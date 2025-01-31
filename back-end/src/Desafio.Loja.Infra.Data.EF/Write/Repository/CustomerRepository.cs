using Desafio.Loja.Domain.Entities.Customer;
using Desafio.Loja.Domain.Repository;
using Desafio.Loja.Domain.SeedWork;

namespace Desafio.Loja.Infra.Data.EF.Write.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SalesContext _context;

        public CustomerRepository(SalesContext context)
            => _context = context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Customer> Get(Guid id, CancellationToken cancellationToken)
            => await _context.Customers.FindAsync(id, cancellationToken);
        
        public async Task Insert(Customer aggregate, CancellationToken cancellationToken)
            => await _context.Customers.AddAsync(aggregate, cancellationToken);

        public async Task Update(Customer aggregate, CancellationToken cancellationToken)
            => await Task.FromResult(_context.Customers.Update(aggregate));

        public async Task Delete(Customer aggregate, CancellationToken cancellationToken)
            => await Task.FromResult(_context.Customers.Remove(aggregate));

        public void Dispose()
            => _context.Dispose();
    }
}
