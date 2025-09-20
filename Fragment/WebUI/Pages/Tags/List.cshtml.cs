using Fragment.Application.Dtos;
using Fragment.Application.ListTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fragment.WebUI.Pages.Tags;

public class ListModel : PageModel
{
    private readonly IMediator _mediator;

    public ListModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); 
    }

    public List<TagDto> Tags { get; set; }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        var request = new ListTagsRequest();
        var response = await _mediator.Send(request, ct);
        Tags = response;

        return Page();
    }
}
