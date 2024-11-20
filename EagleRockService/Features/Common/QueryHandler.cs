using EagleRockService.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Serilog;
using System.Linq;

namespace EagleRockService.Features.Common;

public abstract class QueryHandler<T, TResult> : IRequestHandler<T, PaginatedResponse<TResult>>
    where T : IRequest<PaginatedResponse<TResult>>
{
    protected readonly ICacheService CacheService;

    protected QueryHandler(ICacheService cache)
    {
        CacheService = cache;
    }

    protected string HandlerName => GetType().Name;

    public abstract Task<PaginatedResponse<TResult>> Handle(T request,
        CancellationToken cancellationToken);

    protected Task<PaginatedResponse<T>> BuildResultPage<T>(
        Query<T> requestParams,
        CancellationToken cancellationToken,
        IList<T> queryable)
    {
        var total = queryable.Count();
        var totalPages = (int)Math.Ceiling(total / (double)requestParams.PageSize);
        var skipRows = requestParams.PageSize * (requestParams.Page - 1);
        var results = queryable.Skip(skipRows).Take(requestParams.PageSize);
        var page = new PaginatedResponse<T>(results, total, totalPages, requestParams.Page);
        return Task.FromResult(page);
    }

    protected void LogHandlerEntry<TRequest>(TRequest request)
    {
        Log.Information("Entering MediatR handler {MediatorHandlerName} for request {MediatorRequest}.", HandlerName,
            JsonConvert.SerializeObject(request));
    }

    protected TResponse LogHandlerExit<TResponse>(TResponse response, string? additionalMessage = null)
    {
        Log.Information(
            "Exiting MediatR handler {MediatorHandlerName} with response return type {MediatorHandlerResponseType}.",
            HandlerName, typeof(TResponse).Name + (additionalMessage == null ? "" : " " + additionalMessage));
        return response;
    }
}
