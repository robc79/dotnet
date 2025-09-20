using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.EditFragment;

public class EditFragmentHandler : IRequestHandler<EditFragmentRequest, EditFragmentResponse>
{
    public IUnitOfWork _unitOfWork;
    public ITextFragmentRepository _textFragmentRepository;
    public ITagRepository _tagRepository;

    public EditFragmentHandler(
        IUnitOfWork unitOfWork,
        ITextFragmentRepository textFragmentRepository,
        ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _textFragmentRepository = textFragmentRepository ?? throw new ArgumentNullException(nameof(textFragmentRepository));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<EditFragmentResponse> Handle(EditFragmentRequest request, CancellationToken cancellationToken)
    {
        var fragment = await _textFragmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (fragment is null)
        {
            return new EditFragmentResponse("Fragment not found.");
        }

        fragment.Text = request.Text;
        fragment.Tags.Clear();

        foreach (var tagId in request.TagIds)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId, cancellationToken);

            if (tag is not null)
            {
                fragment.Tags.Add(tag);
            }
        }

        await _unitOfWork.CommitChangesAsync(cancellationToken);
        
        return new EditFragmentResponse();
    }
}
