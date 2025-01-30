using Desafio.Loja.Domain.Entities.Order;
using Desafio.Loja.Domain.Repository;
using Desafio.Loja.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Loja.Infra.Data.EF.Write.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesContext _context;       

        public OrderRepository(SalesContext context) 
            => _context = context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Order> Get(Guid id, CancellationToken cancellationToken) 
            => await _context.Orders.FindAsync(id, cancellationToken);

        public async Task<IEnumerable<Order>> GetListByCustomerId(Guid customerId)
            => await _context.Orders.Where(p => p.CustomerId == customerId).ToListAsync();

        public async Task<Order> GetDraftOrderByCustomerId(Guid customerId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(
                p => p.CustomerId == customerId && p.Status == OrderStatus.Draft);

            if (order == null) return null;

            await _context.Entry(order)
                .Collection(i => i.OrderItems).LoadAsync();            

            return order;
        }        

        public async Task Insert(Order aggregate, CancellationToken cancellationToken)
            => await _context.Orders.AddAsync(aggregate, cancellationToken);
        
        public async Task Update(Order aggregate, CancellationToken _)
            => await Task.FromResult(_context.Orders.Update(aggregate));
        
        public async Task Delete(Order aggregate, CancellationToken _)
            => await Task.FromResult(_context.Orders.Remove(aggregate));


        public async Task<OrderItem> GetItemById(Guid id) 
            => await _context.OrderItems.FindAsync(id);

        public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId) 
            => await _context.OrderItems.FirstOrDefaultAsync(p => p.OrderId == orderId && p.ProductId == productId);

        public void AddItem(OrderItem orderItem) 
            => _context.OrderItems.Add(orderItem);

        public void UpdateItem(OrderItem orderItem) 
            => _context.OrderItems.Update(orderItem);

        public void RemoveItem(OrderItem orderItem) 
            => _context.OrderItems.Remove(orderItem);

        public void Dispose() 
            => _context.Dispose();
    }
}
