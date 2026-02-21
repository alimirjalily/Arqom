using Arqom.Core.Domain.Entities;
using Arqom.Core.Domain.Events;
using Arqom.Core.Domain.ValueObjects;

namespace TestDomain
{
    public class Class1 : IAggregateRoot

    {
        public BusinessId BusinessId => throw new NotImplementedException();

        public void ClearEvents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDomainEvent> GetEvents()
        {
            throw new NotImplementedException();
        }
    }
}
