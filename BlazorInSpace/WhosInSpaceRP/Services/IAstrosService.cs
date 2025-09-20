using WhosInSpaceRP.Models;

namespace WhosInSpaceRP.Services
{
    public interface IAstrosService
    {
        Task<AstrosData?> GetAstrosAsync();

        Task<AstrosData?> GetEasterEggAsync();
    }
}
