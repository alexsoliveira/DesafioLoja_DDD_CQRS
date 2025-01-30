using Desafio.Loja.Domain.SeedWork.Messages;

namespace Desafio.Loja.Application.Events
{
    public class UpdatedOrderEvent : Event
    {        
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalAmount { get; private set; }

        public UpdatedOrderEvent(Guid customerId, Guid orderId, decimal totalAmount)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            TotalAmount = totalAmount;
        }
    }
}
