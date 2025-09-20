using Fragment.Application.Dtos;
using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.GetTag;

public class GetTagHandler : IRequestHandler<GetTagRequest, TagDto?>
{
    private readonly ITagRepository _tagRepository;

    public GetTagHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<TagDto?> Handle(GetTagRequest request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetByIdAsync(request.Id, cancellationToken);

        return tag is null ? null : new TagDto { Id = tag.Id, Name = tag.Name };
    }
}
