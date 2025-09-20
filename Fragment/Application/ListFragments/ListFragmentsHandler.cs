using System.Net.Http.Headers;
using Fragment.Application.Dtos;
using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.ListFragments;

public class ListFragmentsHandler : IRequestHandler<ListFragmentsRequest, List<TextFragmentDto>>
{
    private readonly ITextFragmentRepository _textFragmentRepository;

    public ListFragmentsHandler(ITextFragmentRepository textFragmentRepository)
    {
        _textFragmentRepository = textFragmentRepository ?? throw new ArgumentNullException(nameof(textFragmentRepository));
    }

    public async Task<List<TextFragmentDto>> Handle(ListFragmentsRequest request, CancellationToken cancellationToken)
    {
        var fragments = await _textFragmentRepository.GetPageAsync(request.Skip, request.Take, cancellationToken);

        return fragments.Select(f => new TextFragmentDto
        {
            Id = f.Id,
            Text = f.Text,
            CreatedOn = f.CreatedOn,
            Tags = f.Tags.Select(t => new TagDto { Id = t.Id, Name = t.Name}).ToArray()
        }).ToList();
    }
}