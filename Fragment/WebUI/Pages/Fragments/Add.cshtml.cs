using System.ComponentModel.DataAnnotations;
using Fragment.Application.AddFragment;
using Fragment.Application.Dtos;
using Fragment.Application.ListTags;
using Fragment.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fragment.WebUI.Pages.Fragments;

public class AddModel : PageModel
{
    public class FormModel
    {
        [Required]
        public string Text { get; set; }

        public int[] SelectedTagIds { get; set; } = [];
    }

    private readonly IMediator _mediator;

    public AddModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [BindProperty]
    public FormModel Form { get; set; }

    public SelectList TagItems { get; set; }

    public async Task<IActionResult> OnGetAsync(CancellationToken ct)
    {
        await PopulateTags(ct);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        await PopulateTags(ct);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new AddFragmentRequest(Form.Text, Form.SelectedTagIds);
        _ = await _mediator.Send(request, ct);

        return RedirectToPage("/Fragments/List", new { skip = 0 });
    }

    private async Task PopulateTags(CancellationToken ct)
    {
        var request = new ListTagsRequest();
        var response = await _mediator.Send(request, ct);
        TagItems = new SelectList(response, nameof(TagDto.Id), nameof(TagDto.Name));
    }
}
