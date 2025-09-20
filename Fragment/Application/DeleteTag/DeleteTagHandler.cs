using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.DeleteTag;

public class DeleteTagHandler : IRequestHandler<DeleteTagRequest>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTagHandler(IUnitOfWork unitOfWork, ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task Handle(DeleteTagRequest request, CancellationToken cancellationToken)
    {
        await _tagRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitChangesAsync(cancellationToken);
    }
}
