using Desafio.Loja.Domain.Entities.Order;
using Desafio.Loja.Domain.SeedWork;

namespace Desafio.Loja.Domain.Repository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {        
        Task<IEnumerable<Order>> GetListByCustomerId(Guid customerId);
        Task<Order> GetDraftOrderByCustomerId(Guid customerId);                

        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);

        void AddItem(OrderItem orderItem);
        void UpdateItem(OrderItem orderItem);
        void RemoveItem(OrderItem orderItem);        
    }
}
