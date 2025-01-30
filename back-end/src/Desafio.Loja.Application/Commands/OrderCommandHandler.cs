using Desafio.Loja.Application.Common.Communication.Mediator;
using Desafio.Loja.Application.Common.Messages;
using Desafio.Loja.Application.Events;
using Desafio.Loja.Domain.Entities;
using Desafio.Loja.Domain.Entities.Order;
using Desafio.Loja.Domain.Repository;
using MediatR;

namespace Desafio.Loja.Application.Commands
{
    public class OrderCommandHandler : 
        IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;        

        public OrderCommandHandler(
            IOrderRepository orderRepository, 
            IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;            
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            var orderItem = new OrderItem(
                message.ProductId, message.ProductName, message.Quantity, message.UnitPrice);

            if (order == null)
            {
                order = Order.PedidoFactory.NewDraftOrder(message.CustomerId);
                order.AddItem(orderItem);

                await _orderRepository.Insert(order, cancellationToken);
                order.AddEvent(new DraftOrderInitiatedEvent(message.CustomerId, message.ProductId));
            }
            else
            {
                var orderItemExisting = order.OrderItemExisting(orderItem);
                order.AddItem(orderItem);

                if (orderItemExisting)
                {
                    _orderRepository.UpdateItem(
                        order.OrderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }

                order.AddEvent(new UpdatedOrderEvent(order.CustomerId, order.Id, order.TotalAmount));
            }

            order.AddEvent(new OrderItemAddedEvent(
                order.CustomerId, 
                order.Id, 
                message.ProductId, 
                message.ProductName, 
                message.UnitPrice, 
                message.Quantity));                        

            return await _orderRepository.UnitOfWork.Commit(cancellationToken);
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(
                    new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
