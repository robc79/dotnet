using Fragment.Domain;
using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.AddTag;

public class AddTagHandler : IRequestHandler<AddTagRequest, int>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTagHandler(IUnitOfWork unitOfWork, ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<int> Handle(AddTagRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request), "Request must be supplied.");
        }
        
        var tag = MakeTag(request.Name);
        await _tagRepository.AddAsync(tag, cancellationToken);
        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return tag.Id;
    }

    protected virtual Tag MakeTag(string name)
    {
        return new Tag(name);
    }
}
