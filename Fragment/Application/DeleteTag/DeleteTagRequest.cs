using MediatR;

namespace Fragment.Application.DeleteTag;

public class DeleteTagRequest : IRequest
{
    public int Id { get; }

    public DeleteTagRequest(int id)
    {
        Id = id;
    }
}