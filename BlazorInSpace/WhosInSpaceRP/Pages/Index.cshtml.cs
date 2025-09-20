using Microsoft.AspNetCore.Mvc.RazorPages;
using WhosInSpaceRP.Services;

namespace WhosInSpaceRP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAstrosService _astrosService;

        public List<string?> Spacecraft { get; set; } = new List<string?>();

        public IndexModel(
            ILogger<IndexModel> logger,
            IAstrosService astrosService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _astrosService = astrosService ?? throw new ArgumentNullException(nameof(astrosService));
        }

        public async Task OnGetAsync()
        {
            var astrosData = await _astrosService.GetAstrosAsync();

            if (astrosData != null)
            {
                Spacecraft = astrosData.People.Select(p => p.Craft).Distinct().OrderBy(c => c).ToList();
            }
        }
    }
}