namespace GameNamePicker;

public class Picker
{
    private readonly ITokenService _tokenService;
    private readonly IGamesService _gameService;

    public Picker(ITokenService tokenService, IGamesService gameService)
    {
        _tokenService = tokenService;
        _gameService = gameService;
    }

    public async Task<string?> PickGameNameAsync(CancellationToken ct)
    {
        var token = await _tokenService.AcquireTokenAsync(ct);
        
        if (token is null) {
            return "<!> Unable to acquire token for API.";
        }

        var gameName = await _gameService.RandomGameNameAsync(token, ct);
        
        return gameName;
    }
}