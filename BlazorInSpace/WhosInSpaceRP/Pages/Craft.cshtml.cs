using Microsoft.AspNetCore.Mvc.RazorPages;
using WhosInSpaceRP.Models;
using WhosInSpaceRP.Services;

namespace WhosInSpaceRP.Pages
{
    public class CraftModel : PageModel
    {
        private readonly IAstrosService _astrosService;

        public List<Astronaut>? Crew { get; set; }

        public CraftModel(IAstrosService astrosService)
        {
            _astrosService = astrosService ?? throw new ArgumentNullException(nameof(astrosService));
        }

        public async Task OnGetAsync(string craft)
        {
            var data = await ((craft == "Red Dwarf")
                ? _astrosService.GetEasterEggAsync()
                : _astrosService.GetAstrosAsync());

            if (data != null)
            {
                Crew = data.People.Where(p => p.Craft == craft).OrderBy(p => p.Name).ToList();
            }
        }
    }
}