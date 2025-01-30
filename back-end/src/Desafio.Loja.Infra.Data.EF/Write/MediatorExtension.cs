using Desafio.Loja.Application.Common.Communication.Mediator;
using Desafio.Loja.Domain.SeedWork;

namespace Desafio.Loja.Infra.Data.EF.Write
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents(this IMediatorHandler mediator, SalesContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
