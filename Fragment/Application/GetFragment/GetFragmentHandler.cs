using Fragment.Application.Dtos;
using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.GetFragment;

public class GetFragmentHandler : IRequestHandler<GetFragmentRequest, TextFragmentDto?>
{
    private readonly ITextFragmentRepository _textFragmentRepository;

    public GetFragmentHandler(ITextFragmentRepository textFragmentRepository)
    {
        _textFragmentRepository =
            textFragmentRepository ?? throw new ArgumentNullException(nameof(textFragmentRepository));
    }

    public async Task<TextFragmentDto?> Handle(GetFragmentRequest request, CancellationToken cancellationToken)
    {
        var fragment = await _textFragmentRepository.GetByIdAsync(request.Id, cancellationToken);

        return fragment is null ? null : new TextFragmentDto
        {
            Id = fragment.Id,
            Text = fragment.Text,
            CreatedOn = fragment.CreatedOn,
            Tags = fragment.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name}).ToArray()
        };
    }
}