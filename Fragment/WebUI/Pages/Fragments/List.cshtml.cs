using Fragment.Application.Configuration;
using Fragment.Application.Dtos;
using Fragment.Application.ListFragments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fragment.WebUI.Pages.Fragments;

public class ListModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly SearchPageConfiguration _configuration;

    public List<TextFragmentDto> Fragments { get; set; } = [];

    public int CurrentSkip { get; set; }

    public int PrevSkip { get; set; }

    public int NextSkip { get; set; }

    public ListModel(IMediator mediator, SearchPageConfiguration configuration)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<IActionResult> OnGetAsync([FromQuery] int skip, CancellationToken ct)
    {
        skip = skip < 0 ? 0 : skip;
        var request = new ListFragmentsRequest(skip, _configuration.EntriesPerPage + 1);
        var response = await _mediator.Send(request, ct);
        Fragments = response.Take(_configuration.EntriesPerPage).ToList();

        CurrentSkip = skip;

        if (response.Count > _configuration.EntriesPerPage)
        {
            NextSkip = CurrentSkip + _configuration.EntriesPerPage;
        }
        else
        {
            NextSkip = CurrentSkip;
        }

        PrevSkip = CurrentSkip - _configuration.EntriesPerPage;
        PrevSkip = PrevSkip < 0 ? 0 : PrevSkip;
        
        return Page();
    }
}
