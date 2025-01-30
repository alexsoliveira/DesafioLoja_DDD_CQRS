using Desafio.Loja.Domain.Exceptions;
using Desafio.Loja.Domain.SeedWork;

namespace Desafio.Loja.Domain.Entities.Order
{
    public class Order : AggregateRoot
    {
        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(Guid customerId, decimal totalAmount)
        {
            CustomerId = customerId;
            TotalAmount = totalAmount;
            _orderItems = new List<OrderItem>();
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public void CalculateOrderValue()
        {
            TotalAmount = OrderItems.Sum(p => p.CalculateValue());
        }

        public bool OrderItemExisting(OrderItem item)
        {
            return _orderItems.Any(p => p.ProductId == item.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if (OrderItemExisting(item))
            {
                var itemExisting = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);
                itemExisting.AddUnits(item.Quantity);
                item = itemExisting;

                _orderItems.Remove(itemExisting);
            }

            item.CalculateValue();
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            var itemExisting = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (itemExisting == null) throw new EntityValidationException("O item não pertence ao pedido");
            _orderItems.Remove(itemExisting);

            CalculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var itemExisting = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (itemExisting == null) throw new EntityValidationException("O item não pertence ao pedido");

            _orderItems.Remove(itemExisting);
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void UpdateUnits(OrderItem item, int units)
        {
            item.UpdateUnits(units);
            UpdateItem(item);
        }

        public void MakeDraft()
        {
            Status = OrderStatus.Draft;
        }

        public void StartOrder()
        {
            Status = OrderStatus.Started;
        }

        public void FinalizeOrder()
        {
            Status = OrderStatus.Paid;
        }

        public void CancelOrder()
        {
            Status = OrderStatus.Canceled;
        }

        public static class PedidoFactory
        {
            public static Order NewDraftOrder(Guid customerId)
            {
                var order = new Order
                {
                    CustomerId = customerId,
                };

                order.MakeDraft();
                return order;
            }
        }
    }
}
