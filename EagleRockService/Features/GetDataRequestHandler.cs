using EagleRockService.Features.Common;
using EagleRockService.Models;
using EagleRockService.Services;
using OneOf;

namespace EagleRockService.Features
{
    public class GetDataRequestHandler : RequestHandler<GetDataRequest, OneOf<IList<EagleBot>, ReturnTypes.InternalError, ReturnTypes.BadRequest>>
    {
        public GetDataRequestHandler(ICacheService cacheService) : base(cacheService)
        {

        }

        public override async Task<OneOf<IList<EagleBot>, ReturnTypes.InternalError, ReturnTypes.BadRequest>> Handle(GetDataRequest request,
            CancellationToken cancellationToken)
        {
            LogHandlerEntry(request);

            var eagleBotList = new List<EagleBot>();
            foreach (var botKey in request.EagleBotKeys)
            {
                var bot = await CacheService.GetCacheValueAsync<EagleBot>(botKey);
                if (bot == null)
                {
                    return new ReturnTypes.BadRequest();
                }
                eagleBotList.Add(bot);
            }
            LogHandlerExit(new ReturnTypes.Success());

            return eagleBotList;
        }
    }

}