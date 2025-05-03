using Arqom.Core.Domain.Events;

namespace MiniBlog.Core.Domain.Blogs.Events;

public record BlogDeleted(Guid BusinessId) : IDomainEvent;
