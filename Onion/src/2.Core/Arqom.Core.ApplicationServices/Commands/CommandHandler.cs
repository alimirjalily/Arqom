using Arqom.Core.Contracts.ApplicationServices.Commands;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Common;
using Arqom.Utilities;

namespace Arqom.Core.ApplicationServices.Commands;

public abstract class CommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
    where TCommand : ICommand<TData>
{

    protected readonly ArqomServices _arqomServices;

    public CommandHandler(ArqomServices arqomServices)
    {
        _arqomServices = arqomServices;
    }

    public abstract Task<CommandResult<TData>> Handle(TCommand command);
    protected virtual Task<CommandResult<TData>> OkAsync(TData data) =>
       Task.FromResult(Ok(data));

    protected virtual CommandResult<TData> Ok(TData data) =>
         CommandResult<TData>.Ok(data);
    protected virtual Task<CommandResult<TData>> ResultAsync(TData data, ApplicationServiceStatus status) =>
        Task.FromResult(Result(data, status));

    protected virtual CommandResult<TData> Result(TData data, ApplicationServiceStatus status) =>
        CommandResult<TData>.Result(data,status );

}

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
where TCommand : ICommand
{
    protected readonly ArqomServices _arqomServices;
   
    public CommandHandler(ArqomServices arqomServices)
    {
        _arqomServices = arqomServices;
    }
    public abstract Task<CommandResult> Handle(TCommand command);

    protected virtual Task<CommandResult> OkAsync() => Task.FromResult(Ok());

    protected virtual CommandResult Ok() => 
        new CommandResult { Status = ApplicationServiceStatus.Ok };

    protected virtual Task<CommandResult> ResultAsync(ApplicationServiceStatus status) =>
        Task.FromResult(Result(status));

    protected virtual CommandResult Result(ApplicationServiceStatus status) =>
        new CommandResult { Status = status };

}

