namespace Arqom.Core.Domain.Exceptions;

public sealed record DomainError(string Code, params string?[] Args);
