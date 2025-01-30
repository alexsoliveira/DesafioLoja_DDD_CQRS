using Desafio.Loja.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Desafio.Loja.Domain.Entities.Customer
{
    public class Phone
    {
        public const int NumberMaxLength = 15;
        public const int NumberMinLength = 10;
        public string Number { get; private set; }

        protected Phone() { }

        public Phone(string number)
        {
            if (!Validate(number)) throw new EntityValidationException("Telefone inválido");
            Number = number;
        }

        public static bool Validate(string phone)
        {
            var regexPhone = new Regex(@"^\(?\d{2}\)?\s?(?:9\d{4}|\d{4})-?\d{4}$");
            return regexPhone.IsMatch(phone);
        }
    }
}