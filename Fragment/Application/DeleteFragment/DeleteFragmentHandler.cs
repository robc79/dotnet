using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.DeleteFragment;

public class DeleteFragmentHandler : IRequestHandler<DeleteFragmentRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITextFragmentRepository _textFragmentRepository;

    public DeleteFragmentHandler(IUnitOfWork unitOfWork, ITextFragmentRepository textFragmentRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _textFragmentRepository = textFragmentRepository ?? throw new ArgumentNullException(nameof(textFragmentRepository));
    }

    public async Task Handle(DeleteFragmentRequest request, CancellationToken cancellationToken)
    {
        await _textFragmentRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitChangesAsync(cancellationToken);
    }
}
