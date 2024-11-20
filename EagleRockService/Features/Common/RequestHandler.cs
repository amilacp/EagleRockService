using EagleRockService.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

namespace EagleRockService.Features.Common;

public abstract class RequestHandler<T, TResult> : IRequestHandler<T, TResult> where T : IRequest<TResult>
{
    internal readonly ICacheService CacheService;

    public RequestHandler(ICacheService cacheService)
    {
        CacheService = cacheService;
    }

    protected string HandlerName => GetType().Name;
    public abstract Task<TResult> Handle(T request, CancellationToken cancellationToken);

    protected void LogHandlerEntry<TRequest>(TRequest request)
    {
        Log.Information("Entering MediatR handler {MediatorHandlerName} for request {MediatorRequest}.", HandlerName,
            request);
    }

    protected TResponse LogHandlerExit<TResponse>(TResponse response, string? additionalMessage = null)
    {
        Log.Information(
            "Exiting MediatR handler {MediatorHandlerName} with response return type {MediatorHandlerResponseType}.",
            HandlerName, " " + additionalMessage);
        return response;
    }
}