using System.Text.Json;

namespace GameNamePicker;

public class InternetGamesDatabaseService : IGamesService
{
    private readonly HttpClient _httpClient;
    private readonly IgdbConfiguration _igdbConfig;

    public InternetGamesDatabaseService(HttpClient httpClient, IgdbConfiguration igdbConfig)
    {
        _httpClient = httpClient;
        _igdbConfig = igdbConfig;
        _httpClient.BaseAddress = new Uri(_igdbConfig.ApiEndpoint);
        _httpClient.DefaultRequestHeaders.Add("Client-ID", _igdbConfig.ClientId);
    }

    public async Task<string?> RandomGameNameAsync(string accessToken, CancellationToken ct)
    {
        var rnd = new Random();
        var rndGameId = rnd.Next(1, _igdbConfig.MaxGameId);

        var request = new HttpRequestMessage {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{_httpClient.BaseAddress?.ToString()}/games"),
            Content = new StringContent($"fields name; where id = {rndGameId};")
        };

        request.Headers.Add("Authorization", accessToken);
        var response = await _httpClient.SendAsync(request);
        var jsonString = await response.Content.ReadAsStringAsync(ct);
        var gameResponse = JsonSerializer.Deserialize<List<GameResponse>>(jsonString);

        return gameResponse?.Count > 0 ? gameResponse[0].Name : null;
    }
}