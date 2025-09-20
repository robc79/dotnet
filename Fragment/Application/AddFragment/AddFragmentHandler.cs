using Fragment.Domain;
using Fragment.Domain.Repositories;
using MediatR;

namespace Fragment.Application.AddFragment;

public class AddFragmentHandler : IRequestHandler<AddFragmentRequest, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITextFragmentRepository _textFragmentRepository;
    private readonly ITagRepository _tagRepository;

    public AddFragmentHandler(
        IUnitOfWork unitOfWork,
        ITextFragmentRepository textFragmentRepository,
        ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _textFragmentRepository = textFragmentRepository ?? throw new ArgumentNullException(nameof(textFragmentRepository));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
    }

    public async Task<int> Handle(AddFragmentRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request), "Request must be supplied.");
        }

        var fragment = MakeFragment(request.Text);

        foreach (var tagId in request.TagIds)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId, cancellationToken);

            if (tag is not null)
            {
                fragment.Tags.Add(tag);
            }
        }

        await _textFragmentRepository.AddAsync(fragment, cancellationToken);
        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return fragment.Id;
    }
    
    protected virtual TextFragment MakeFragment(string text)
    {
        return new TextFragment(text);
    }
}