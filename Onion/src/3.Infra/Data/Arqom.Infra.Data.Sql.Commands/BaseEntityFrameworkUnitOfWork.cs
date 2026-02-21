using Arqom.Core.Contracts.Data.Commands;
using Microsoft.EntityFrameworkCore.Storage;

namespace Arqom.Infra.Data.Sql.Commands;
public abstract class BaseEntityFrameworkUnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : BaseCommandDbContext
{
    protected readonly TDbContext _dbContext;
    private IDbContextTransaction _currentTransaction;

    public BaseEntityFrameworkUnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null) return;
        _currentTransaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public int Commit()
    {
        var result = _dbContext.SaveChanges();
        return result;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken= default)
    {
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _currentTransaction?.CommitAsync()!;
        }
        finally
        {
            await ReleaseCurrentTransaction();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            await _currentTransaction?.RollbackAsync()!;
        }
        finally
        {
            await ReleaseCurrentTransaction();
        }
    }

    private async Task ReleaseCurrentTransaction()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null!;
        }
    }
}

