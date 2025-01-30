using Desafio.Loja.Domain.Entities.Product;
using Desafio.Loja.Domain.SeedWork;

namespace Desafio.Loja.Domain.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
    }
}
