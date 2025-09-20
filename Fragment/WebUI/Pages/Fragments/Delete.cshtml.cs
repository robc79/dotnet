using Fragment.Application.DeleteFragment;
using Fragment.Application.Dtos;
using Fragment.Application.GetFragment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fragment.WebUI.Pages.Fragments;

public class DeleteModel : PageModel
{
    private readonly IMediator _mediator;

    public TextFragmentDto Fragment { get; set; }
    
    public DeleteModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<IActionResult> OnGetAsync(int fragmentId, CancellationToken ct)
    {
        var request = new GetFragmentRequest(fragmentId);
        var response = await _mediator.Send(request, ct);

        if (response is null)
        {
            return NotFound();
        }

        Fragment = response;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int fragmentId, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new DeleteFragmentRequest(fragmentId);
        await _mediator.Send(request);

        return RedirectToPage("/Fragments/List", new { skip = 0 });
    }
}
