using Desafio.Loja.Application.Common.Messages;
using FluentValidation;

namespace Desafio.Loja.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public AddOrderItemCommand(Guid customerId, Guid productId, string productName, int quantity, decimal unitPrice)
        {
            CustomerId = customerId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderItemCommandValidation : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.ProductName)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");

            RuleFor(c => c.UnitPrice)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}
