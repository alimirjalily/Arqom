using Arqom.Core.Domain.Repositories;
using MiniBlog.Core.Domain.People.Entities;

namespace MiniBlog.Core.Domain.People.Repositories;

public interface IPersonCommandRepository : ICommandRepository<Person, int>
{
}
