using MediatR;

namespace Fragment.Application.DeleteFragment;

public class DeleteFragmentRequest : IRequest
{
    public int Id { get; }

    public DeleteFragmentRequest(int id)
    {
        Id = id;
    }
}