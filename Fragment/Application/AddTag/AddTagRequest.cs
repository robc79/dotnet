using MediatR;

namespace Fragment.Application.AddTag;

public class AddTagRequest : IRequest<int>
{
    public string Name { get; }

    public AddTagRequest(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name must be supplied.");
        }

        Name = name;
    }
}