using Desafio.Loja.Application.Common.Communication.Mediator;
using MediatR;

namespace Desafio.Loja.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<DraftOrderInitiatedEvent>,
        INotificationHandler<UpdatedOrderEvent>,
        INotificationHandler<OrderItemAddedEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(DraftOrderInitiatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(UpdatedOrderEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
