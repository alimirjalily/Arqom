using Arqom.Core.Contracts.Data.Commands;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Common;
using Microsoft.Extensions.Logging;

namespace Arqom.Core.ApplicationServices.Commands.Transactions;

public class TransactionCommandDispatcherDecorator : CommandDispatcherDecorator
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionCommandDispatcherDecorator> _logger;

    // Order را مثلاً 3 می‌گذاریم تا لایه‌های دیگر (مثل لاگ یا ولیدیشن) اولویت‌بندی شوند
    public override int Order => 3;

    public TransactionCommandDispatcherDecorator(IUnitOfWork unitOfWork,
                                                ILogger<TransactionCommandDispatcherDecorator> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public override async Task<CommandResult> Send<TCommand>(TCommand command)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // هدایت دستور به دیسپچر بعدی (یا دیسپچر اصلی)
            var result = await _commandDispatcher.Send(command);

            if (result.Status == ApplicationServiceStatus.Ok)
            {
                // اینجاست که Outbox Interceptor در حین ذخیره اجرا می‌شود
                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            else
            {
                await _unitOfWork.RollbackTransactionAsync();
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed for command {CommandType}", typeof(TCommand).Name);
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public override async Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var result = await _commandDispatcher.Send<TCommand, TData>(command);

            if (result.Status == ApplicationServiceStatus.Ok)
            {
                // ذخیره تغییرات دامین + رکوردهای Outbox در یک تراکنش واحد
                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            else
            {
                await _unitOfWork.RollbackTransactionAsync();
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Transaction failed for command {CommandType}", typeof(TCommand).Name);
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
