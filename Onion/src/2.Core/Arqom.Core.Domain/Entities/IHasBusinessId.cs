using Arqom.Core.Domain.ValueObjects;

namespace Arqom.Core.Domain.Entities;

public interface IHasBusinessId
{
    BusinessId BusinessId { get; }
}
