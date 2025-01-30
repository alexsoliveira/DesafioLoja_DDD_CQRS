using Desafio.Loja.Domain.SeedWork.Messages;

namespace Desafio.Loja.Application.Events
{
    public class OrderItemAddedEvent : Event
    {        
        public Guid CostumerId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public OrderItemAddedEvent(
            Guid costumerId, 
            Guid orderId, 
            Guid productId, 
            string productName, 
            decimal unitPrice, 
            int quantity)
        {
            AggregateId = orderId;
            CostumerId = costumerId;
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
