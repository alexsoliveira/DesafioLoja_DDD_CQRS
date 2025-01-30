namespace Desafio.Loja.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        public Task<bool> Commit(CancellationToken cancellationToken);        
    }
}
