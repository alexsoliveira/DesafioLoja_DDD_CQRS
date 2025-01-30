using Desafio.Loja.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Desafio.Loja.Domain.Entities.Customer
{
    public class Email
    {
        public const int AddressMaxLength = 254;
        public const int AddressMinLength = 5;
        public string Address { get; private set; }
        
        protected Email() { }

        public Email(string address)
        {
            if (!Validar(address)) throw new EntityValidationException("E-mail inválido");
            Address = address;
        }

        public static bool Validar(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}