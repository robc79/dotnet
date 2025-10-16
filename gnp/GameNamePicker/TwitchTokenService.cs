using System.Net.Http.Json;
using System.Text.Json;

namespace GameNamePicker;

public class TwitchTokenService : ITokenService
{
    private readonly HttpClient _httpClient;
    private readonly IgdbConfiguration _igdbConfig;

    public TwitchTokenService(HttpClient httpClient, IgdbConfiguration igdbConfig)
    {
        _httpClient = httpClient;
        _igdbConfig = igdbConfig;
        _httpClient.BaseAddress = new Uri(_igdbConfig.TokenEndpoint);
    }


    public async Task<string?> AcquireTokenAsync(CancellationToken ct)
    {
        var query = $"?client_id={_igdbConfig.ClientId}&client_secret={_igdbConfig.Secret}&grant_type=client_credentials";
        var response = await _httpClient.PostAsync(query, null, ct);
        string? token = null;

        if (!response.IsSuccessStatusCode) {
            return token;
        }

        var jsonString = await response.Content.ReadAsStringAsync(ct);
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(jsonString);

        return $"{tokenResponse?.TokenType} {tokenResponse?.AccessToken}";
    }
}