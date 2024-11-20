using EagleRockService.Features.Common;
using MediatR;

public abstract class Query<T> : IRequest<PaginatedResponse<T>>
{
    protected Query()
    {
        Page = 1;
        PageSize = 20;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
}