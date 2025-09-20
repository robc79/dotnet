using MediatR;

namespace Fragment.Application.EditFragment;

public class EditFragmentRequest : IRequest<EditFragmentResponse>
{
    public int Id { get; }

    public string Text { get; }

    public int[] TagIds { get; }

    public EditFragmentRequest(int id, string text, int[] tagIds)
    {
        Id = id;

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text must be supplied.", nameof(text));
        }

        Text = text;
        TagIds = tagIds;
    }
}