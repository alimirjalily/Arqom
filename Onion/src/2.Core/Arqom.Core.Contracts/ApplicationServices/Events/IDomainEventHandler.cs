using Arqom.Core.Domain.Events;

namespace Arqom.Core.Contracts.ApplicationServices.Events;

public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent Event);
}