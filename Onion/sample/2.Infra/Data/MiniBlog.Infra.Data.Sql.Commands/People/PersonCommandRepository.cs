using Arqom.Infra.Data.Sql.Commands;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.Domain.People.Repositories;
using MiniBlog.Infra.Data.Sql.Commands.Common;

namespace MiniBlog.Infra.Data.Sql.Commands.People;

public class PersonCommandRepository :
        BaseCommandRepository<Person, MiniblogCommandDbContext, int>,
        IPersonCommandRepository
{
    public PersonCommandRepository(MiniblogCommandDbContext dbContext) : base(dbContext)
    {
    }
}
