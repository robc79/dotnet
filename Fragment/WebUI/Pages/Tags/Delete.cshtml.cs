using Fragment.Application.DeleteTag;
using Fragment.Application.Dtos;
using Fragment.Application.GetTag;
using Fragment.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fragment.WebUI.Pages.Tags;

public class DeleteModel : PageModel
{
    private readonly IMediator _mediator;

    public TagDto Tag { get; set; }

    public DeleteModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int tagId, CancellationToken ct)
    {
        var request = new GetTagRequest(tagId);
        var response = await _mediator.Send(request, ct);

        if (response is null)
        {
            return NotFound();
        }

        Tag = response;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int tagId, CancellationToken ct)
    {
        var request = new DeleteTagRequest(tagId);
        await _mediator.Send(request);

        return RedirectToPage("/Tags/List");
    }
}