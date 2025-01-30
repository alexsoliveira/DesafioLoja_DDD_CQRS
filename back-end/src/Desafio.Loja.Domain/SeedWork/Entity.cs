using Desafio.Loja.Domain.SeedWork.Messages;

namespace Desafio.Loja.Domain.SeedWork
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        protected Entity() => Id = Guid.NewGuid();

        public void AddEvent(Event evento)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(evento);
        }

        public void RemoverEvento(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }

        public void LimparEventos()
        {
            _notifications?.Clear();
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
