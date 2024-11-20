using EagleRockService.Features.Common;
using EagleRockService.Models;
using MediatR;
using OneOf;

namespace EagleRockService.Features
{
    public class PostDataCommand : IRequest<OneOf<ReturnTypes.Success, ReturnTypes.InternalError, ReturnTypes.BadRequest>>
    {
        public string EagleBotId { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; }
        public string RoadName { get; set; }
        public TrafficFlowDirection Direction { get; set; }
        public int TrafficFlowRate { get; set; }
        public double AverageSpeed { get; set; }
    }
}