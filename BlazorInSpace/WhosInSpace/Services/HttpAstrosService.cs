using WhosInSpace.Models;

namespace WhosInSpace.Services
{
    public class HttpAstrosService : IAstrosService
    {
        private readonly HttpClient _httpClient;
        private AstrosData? _cachedData = null;

        public HttpAstrosService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<AstrosData?> GetAstrosAsync()
        {
            if (_cachedData == null)
            {
                _cachedData = await _httpClient.GetFromJsonAsync<AstrosData>("astros.json");
            }

            return _cachedData;
        }

        public Task<AstrosData?> GetEasterEggAsync()
        {
            return Task.FromResult<AstrosData?>(new AstrosData
            {
                Message = "success",
                Number = 4,
                People = new List<Astronaut>
                {
                    new Astronaut { Craft = "Red Dwarf", Name = "Dave Lister" },
                    new Astronaut { Craft = "Red Dwarf", Name = "Arnold Rimmer" },
                    new Astronaut { Craft = "Red Dwarf", Name = "Kryten" },
                    new Astronaut { Craft = "Red Dwarf", Name = "Cat" }
                }
            });
        }
    }
}
