using Desafio.Loja.Application.Common.Communication.Mediator;
using Desafio.Loja.Domain.Entities.Customer;
using Desafio.Loja.Domain.Entities.Order;
using Desafio.Loja.Domain.Entities.Product;
using Desafio.Loja.Domain.SeedWork;
using Desafio.Loja.Domain.SeedWork.Messages;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Loja.Infra.Data.EF.Write
{
    public class SalesContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Product> Products => Set<Product>();


        public SalesContext(
            DbContextOptions<SalesContext> options,
            IMediatorHandler mediatorHandler) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) 
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("MySequence").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);            
        }

        public async Task<bool> Commit(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries().Where(
                entry => entry.Entity.GetType().GetProperty("OrderDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("OrderDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("OrderDate").IsModified = false;
                }
            }

            var sucess = await base.SaveChangesAsync() > 0;
            if (sucess) await _mediatorHandler.PublishEvents(this);

            return sucess;
        }        
    }
}
