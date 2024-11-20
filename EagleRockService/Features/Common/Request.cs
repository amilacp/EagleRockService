using MediatR;

namespace EagleRockService.Features.Common
{
    public abstract class Request<T> : IRequest<T>
    {
    }
}