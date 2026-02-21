using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Arqom.Core.Domain.Exceptions;
using Arqom.Core.RequestResponse.Common;
using Arqom.Core.RequestResponse.Queries;
using Arqom.Extensions.Logger.Abstractions;
using Arqom.Extensions.Translations.Abstractions;

namespace Arqom.Core.ApplicationServices.Queries;

public class QueryDispatcherDomainExceptionHandlerDecorator : QueryDispatcherDecorator
{
    #region Fields
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<QueryDispatcherDomainExceptionHandlerDecorator> _logger;
    public override int Order => 2;
    #endregion

    #region Constructors
    public QueryDispatcherDomainExceptionHandlerDecorator(IServiceProvider serviceProvider,
                                                          ILogger<QueryDispatcherDomainExceptionHandlerDecorator> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    #endregion

    #region Query Dispatcher
    public override async Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query)
    {
        try
        {
            var result = await _queryDispatcher.Execute<TQuery, TData>(query);
            return result;

        }
        catch (DomainStateException ex)
        {
            _logger.LogError(ArqomEventId.DomainValidationException, ex, "Processing of {QueryType} With value {Query} failed at {StartDateTime} because there are domain exceptions.", query.GetType(), query, DateTime.Now);
            return DomainExceptionHandlingWithReturnValue<TQuery, TData>(ex);
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is DomainStateException domainStateException)
            {
                _logger.LogError(ArqomEventId.DomainValidationException, ex, "Processing of {QueryType} With value {Query} failed at {StartDateTime} because there are domain exceptions.", query.GetType(), query, DateTime.Now);
                return DomainExceptionHandlingWithReturnValue<TQuery, TData>(domainStateException);
            }
            throw ex;
        }
    }
    #endregion

    #region Privaite Methods
    private QueryResult<TData> DomainExceptionHandlingWithReturnValue<TQuery, TData>(DomainStateException ex)
    {
        var queryResult = new QueryResult<TData>()
        {
            Status = ApplicationServiceStatus.InvalidDomainState
        };

        queryResult.AddMessages(ex.Errors.Select(e =>
            new ApplicationMessage(
                e.Code,
                MessageSeverity.Error,
                e.Args)));

        return queryResult;
    }

    #endregion
}