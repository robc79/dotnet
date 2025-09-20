using Fragment.Application.Dtos;
using MediatR;

namespace Fragment.Application.GetTag;

public class GetTagRequest : IRequest<TagDto?>
{
    public int Id { get; }

    public GetTagRequest(int id)
    {
        Id = id;
    }
}