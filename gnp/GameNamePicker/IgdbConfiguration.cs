namespace GameNamePicker;

public class IgdbConfiguration
{
    public static string SectionName = "Igdb";

    public string ClientId { get; set; }

    public string Secret { get; set; }

    public string TokenEndpoint { get; set; }

    public string ApiEndpoint { get; set; }

    public int MaxGameId { get; set; }
}