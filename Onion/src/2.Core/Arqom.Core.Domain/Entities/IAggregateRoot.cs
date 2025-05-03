using Arqom.Core.Domain.Events;

namespace Arqom.Core.Domain.Entities
{
    public interface IAggregateRoot
    {
        void ClearEvents();
        IEnumerable<IDomainEvent> GetEvents();
    }
}