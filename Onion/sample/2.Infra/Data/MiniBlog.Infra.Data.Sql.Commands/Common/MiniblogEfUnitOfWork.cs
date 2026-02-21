using Arqom.Extensions.DependencyInjection.Abstractions;
using Arqom.Infra.Data.Sql.Commands;

namespace MiniBlog.Infra.Data.Sql.Commands.Common;

public sealed class MiniblogEfUnitOfWork
    : BaseEntityFrameworkUnitOfWork<MiniblogCommandDbContext>, IScopeLifetime
{
    public MiniblogEfUnitOfWork(MiniblogCommandDbContext dbContext) : base(dbContext)
    {
    }
}
