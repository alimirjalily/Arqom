using Arqom.Core.Domain.Events;

namespace Arqom.Core.Contracts.ApplicationServices.Events;

public interface IEventDispatcher
{
    Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;
}