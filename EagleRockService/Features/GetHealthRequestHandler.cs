using EagleRockService.Features.Common;
using EagleRockService.Services;
using System.Reflection;

namespace EagleRockService.Features
{
    public class GetHealthRequestHandler : RequestHandler<GetHealthRequest, GetHealthResponse>
    {
        public GetHealthRequestHandler(ICacheService cacheService) : base(cacheService)
        {
        }

        public override Task<GetHealthResponse> Handle(GetHealthRequest request, CancellationToken cancellationToken)
        {
            LogHandlerExit(new ReturnTypes.Success());
            return Task.FromResult(new GetHealthResponse
            { Version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() });
        }
    }
}
