using EagleRockService.Models;
using MediatR;
using OneOf;

namespace EagleRockService.Features
{
    public class GetDataRequest : IRequest<OneOf<IList<EagleBot>, ReturnTypes.InternalError, ReturnTypes.BadRequest>>
    {
        public required IList<string> EagleBotKeys { get; set; }
    }
}