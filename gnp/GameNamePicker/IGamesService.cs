namespace GameNamePicker;

public interface IGamesService
{
    Task<string?> RandomGameNameAsync(string accessToken, CancellationToken ct);
}