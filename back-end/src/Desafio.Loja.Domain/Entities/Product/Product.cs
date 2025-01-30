using Desafio.Loja.Domain.SeedWork;
using Desafio.Loja.Domain.Validation;

namespace Desafio.Loja.Domain.Entities.Product
{
    public class Product : AggregateRoot
    {        
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        protected Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;

            Validate();
        }

        public void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
            DomainValidation.MinLength(Price, 1, nameof(Price));                        
        }
    }
}
