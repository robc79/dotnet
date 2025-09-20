using MediatR;

namespace Fragment.Application.AddFragment;

public class AddFragmentRequest : IRequest<int>
{
    public string Text { get; }

    public int[] TagIds { get; }

    public AddFragmentRequest(string text, int[] tagIds)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text must be supplied.", nameof(text));
        }

        Text = text;
        TagIds = tagIds;
    }
}