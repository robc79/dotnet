namespace GameNamePicker;

public interface ITokenService
{
    Task<string?> AcquireTokenAsync(CancellationToken ct);
}