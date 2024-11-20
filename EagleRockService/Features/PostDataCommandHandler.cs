using AutoMapper;
using EagleRockService.Features.Common;
using EagleRockService.Helpers;
using EagleRockService.Models;
using EagleRockService.Services;
using Newtonsoft.Json;
using OneOf;

namespace EagleRockService.Features
{
    public class PostDataCommandHandler : RequestHandler<PostDataRequest, OneOf<ReturnTypes.Success, ReturnTypes.InternalError, ReturnTypes.BadRequest>>
    {
        private readonly ITimeStampProvider _timeStampProvider;
        private readonly IMapper _mapper;
        public PostDataCommandHandler(ICacheService cacheService, ITimeStampProvider timeStampProvider, IMapper mapper) : base(cacheService)
        {
            _timeStampProvider = timeStampProvider ?? throw new ArgumentNullException(nameof(timeStampProvider), "TimeStampProvider cannot be null.");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<OneOf<ReturnTypes.Success, ReturnTypes.InternalError, ReturnTypes.BadRequest>> Handle(PostDataRequest request, CancellationToken cancellationToken)
        {
            if (CacheService == null)
            {
                return new ReturnTypes.InternalError();
            }
            if (request == null || string.IsNullOrWhiteSpace(request.EagleBotId))
            {
                return new ReturnTypes.BadRequest();
            }
            string cacheKey = request.EagleBotId;
            if (string.IsNullOrWhiteSpace(cacheKey))
            {
                return new ReturnTypes.BadRequest();
            }
            request.Timestamp = _timeStampProvider.GetCurrentDateTimeStampUtc();

            var eagleBot = _mapper.Map<EagleBot>(request);
            string serializedData = JsonConvert.SerializeObject(eagleBot);
            await CacheService.SetCacheValueAsync(cacheKey, serializedData, TimeSpan.FromHours(1));
            return new ReturnTypes.Success();
        }
    }
}
