using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Arqom.Core.RequestResponse.Commands;
using Arqom.Core.RequestResponse.Common;
using Arqom.Extensions.Logger.Abstractions;
using Arqom.Core.Contracts.ApplicationServices.Commands;

namespace Arqom.Core.ApplicationServices.Commands;

public class CommandDispatcherValidationDecorator : CommandDispatcherDecorator
{
    #region Fields
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CommandDispatcherValidationDecorator> _logger;
    #endregion

    #region Constructors
    public CommandDispatcherValidationDecorator(IServiceProvider serviceProvider,
                                                ILogger<CommandDispatcherValidationDecorator> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public override int Order => 1;
    #endregion

    #region Send Commands
    public override async Task<CommandResult> Send<TCommand>(TCommand command)
    {
        _logger.LogDebug(ArqomEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  start at :{StartDateTime}", command.GetType(), command, DateTime.Now);
        var validationResult = Validate<TCommand>(command);

        if (validationResult != null)
        {
            _logger.LogInformation(ArqomEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}", command.GetType(), command, validationResult.Messages);
            return validationResult;
        }
        _logger.LogDebug(ArqomEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  finished at :{EndDateTime}", command.GetType(), command, DateTime.Now);
        return await _commandDispatcher.Send(command);
    }

    public override async Task<CommandResult<TData>> Send<TCommand, TData>(TCommand command)
    {
        _logger.LogDebug(ArqomEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  start at :{StartDateTime}", command.GetType(), command, DateTime.Now);

        var validationResult = Validate<TCommand>(command);

        if (validationResult != null)
        {
            _logger.LogInformation(ArqomEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}", command.GetType(), command, validationResult.Messages);
            return CommandResult<TData>.From(validationResult);
        }
        _logger.LogDebug(ArqomEventId.CommandValidation, "Validating command of type {CommandType} With value {Command}  finished at :{EndDateTime}", command.GetType(), command, DateTime.Now);
        return await _commandDispatcher.Send<TCommand, TData>(command);
    }
    #endregion

    #region Privaite Methods
    private CommandResult? Validate<TCommand>(TCommand command)
    {
        var validator = _serviceProvider.GetService<IValidator<TCommand>>();

        if (validator is null)
        {
            _logger.LogInformation(
                ArqomEventId.CommandValidation,
                "No validator found for {CommandType}",
                typeof(TCommand)
            );
            return null;
        }

        var result = validator.Validate(command);

        if (result.IsValid)
            return null;

        var commandResult = CommandResult.ValidationFailed();

        foreach (var failure in result.Errors)
        {
            commandResult.AddMessage(
                new ApplicationMessage(
                    code: failure.ErrorCode ?? "validation.error",
                    severity: MessageSeverity.Error,
                    args: new[] { failure.PropertyName }
                )
            );
        }

        _logger.LogWarning(
            ArqomEventId.CommandValidation,
            "Validation failed for {CommandType}. ErrorCodes: {Codes}",
            typeof(TCommand),
            result.Errors.Select(e => e.ErrorCode)
        );

        return commandResult;
    }
    #endregion
}
