using WhosInSpace.Models;

namespace WhosInSpace.Services
{
    public interface IAstrosService
    {
        Task<AstrosData?> GetAstrosAsync();

        Task<AstrosData?> GetEasterEggAsync();
    }
}
