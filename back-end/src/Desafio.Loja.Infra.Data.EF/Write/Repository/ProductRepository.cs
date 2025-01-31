using Desafio.Loja.Domain.Entities.Product;
using Desafio.Loja.Domain.Repository;
using Desafio.Loja.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Loja.Infra.Data.EF.Write.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SalesContext _context;

        public ProductRepository(SalesContext context)
            => _context = context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Product> Get(Guid id, CancellationToken cancellationToken)
            => await _context.Products.FindAsync(id, cancellationToken);
        public async Task<IEnumerable<Product>> GetAll()
            => await _context.Products.ToListAsync();

        public async Task Insert(Product aggregate, CancellationToken cancellationToken)
            => await _context.Products.AddAsync(aggregate, cancellationToken);

        public async Task Update(Product aggregate, CancellationToken cancellationToken)
            => await Task.FromResult(_context.Products.Update(aggregate));

        public async Task Delete(Product aggregate, CancellationToken cancellationToken)
            => await Task.FromResult(_context.Products.Remove(aggregate));

        public void Dispose()
            => _context.Dispose();
    }
}
