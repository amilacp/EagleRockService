using AutoMapper;
using EagleRockService.Features;
using EagleRockService.Services;
using EagleRockService.Test.Common;
using EagleRockService.Test.Helpers;
using Moq;
using OneOf;

namespace EagleRockServiceTest
{
    public class PostDataCommandHandlerHappyPathTests
    {
        public class WhenDataExist : SpecificationForAsync<PostDataCommandHandler>
        {
            private ICacheService? _cacheService;
            private OneOf<ReturnTypes.Success, ReturnTypes.InternalError, ReturnTypes.BadRequest>? _response;

            protected override PostDataCommandHandler Given()
            {
                _cacheService = new FakeCacheService();
                var mapperMock = new Mock<IMapper>();
                var postDataCommandHandler = new PostDataCommandHandler(_cacheService, new FakeTimeStampProvider(), mapperMock.Object);
                return postDataCommandHandler;
            }

            protected override async void When()
            {
                //I would move these data to Json files
                var request = new PostDataCommand
                {
                    EagleBotId = "1",
                    Location = "Brisbane",
                    RoadName = "Änn St",
                    AverageSpeed = 55.25,
                    Direction = EagleRockService.Models.TrafficFlowDirection.Eastbound,
                    Timestamp = new FakeTimeStampProvider().GetCurrentDateTimeStampUtc(),
                    TrafficFlowRate = 10
                };
                _response = await Subject.Handle(request, new CancellationToken());
            }

            [Fact]
            public void TheResponseShouldNotBeNull()
            {
                Assert.NotNull(_response);
            }

            [Fact]
            public void TheResponseTypeIsCorrect()
            {
                Assert.IsType<OneOf<ReturnTypes.Success, ReturnTypes.InternalError, ReturnTypes.BadRequest>>(_response);
            }

            [Fact]
            public void TheResponseShouldTypeofReturnTypesSuccess()
            {
                Assert.IsType<ReturnTypes.Success>(_response?.Value);
            }
        }
    }
}
