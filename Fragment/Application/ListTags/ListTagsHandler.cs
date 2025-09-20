using Fragment.Application.Dtos;
using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.ListTags;

public class ListTagsHandler : IRequestHandler<ListTagsRequest, List<TagDto>>
{
    private readonly ITagRepository _tagRepository;

    public ListTagsHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<List<TagDto>> Handle(ListTagsRequest request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetAllAsync(cancellationToken);

        return tags.Select(t => new TagDto { Id = t.Id, Name = t.Name}).ToList();
    }
}
