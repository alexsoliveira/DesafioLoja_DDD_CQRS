using Desafio.Loja.Domain.SeedWork;
using Desafio.Loja.Domain.Validation;

namespace Desafio.Loja.Domain.Entities.Customer
{
    public class Customer : AggregateRoot
    {        
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }

        protected Customer() { }

        public Customer(string name, string email, string phone)
        {
            Name = name;
            Email = new Email(email);
            Phone = new Phone(phone);
        }

        public void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
            DomainValidation.MinLength(Name, 3, nameof(Name));
            DomainValidation.MaxLength(Name, 50, nameof(Name));
        }
    }
}
