using Fragment.Application.Dtos;
using MediatR;

namespace Fragment.Application.GetFragment;

public class GetFragmentRequest : IRequest<TextFragmentDto?>
{
    public int Id { get; }

    public GetFragmentRequest(int id)
    {
        Id = id;
    }
}