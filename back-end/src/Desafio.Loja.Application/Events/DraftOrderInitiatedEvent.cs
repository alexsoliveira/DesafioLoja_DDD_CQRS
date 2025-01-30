using Desafio.Loja.Domain.SeedWork.Messages;

namespace Desafio.Loja.Application.Events
{
    public class DraftOrderInitiatedEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }

        public DraftOrderInitiatedEvent(Guid customerId, Guid orderId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
