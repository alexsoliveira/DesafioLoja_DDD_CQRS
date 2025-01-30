using Desafio.Loja.Application.Common.Messages;
using Desafio.Loja.Domain.SeedWork.Messages;

namespace Desafio.Loja.Application.Common.Communication.Mediator
{
    public interface IMediatorHandler
    {        
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishEvent<T>(T evento) where T : Event;

        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
