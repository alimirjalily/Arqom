using Arqom.Core.ApplicationServices.Queries;
using Arqom.Core.Contracts.ApplicationServices.Queries;
using Arqom.Core.RequestResponse.Queries;

namespace MiniBlog.Endpoints.API.CustomDecorators;

public class CustomQueryDecorator : QueryDispatcherDecorator
{
    public override int Order => 0;

    public override async Task<QueryResult<TData>> Execute<TQuery, TData>(TQuery query)
    {
        return await _queryDispatcher.Execute<TQuery, TData>(query);
    }
}