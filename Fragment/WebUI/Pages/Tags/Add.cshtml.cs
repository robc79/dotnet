using System.ComponentModel.DataAnnotations;
using Fragment.Application.AddTag;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fragment.WebUI.Pages.Tags;

public class AddModel : PageModel
{
    public class FormModel
    {
        [Required]
        public string Name { get; set; }
    }

    private readonly IMediator _mediator;

    public AddModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [BindProperty]
    public FormModel Form { get; set; }
    
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new AddTagRequest(Form.Name);
        _ = await _mediator.Send(request);

        return RedirectToPage("/Tags/List");
    }
}
