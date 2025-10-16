using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameNamePicker;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder();
        var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

        configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables();

        var configuration = configurationBuilder.Build();
            
        var services = new ServiceCollection();

        var igdbConfiguration = new IgdbConfiguration();
        configuration.GetRequiredSection(IgdbConfiguration.SectionName).Bind(igdbConfiguration);
        services.AddSingleton(igdbConfiguration);
        
        services.AddSingleton<Picker>();
        services.AddHttpClient<ITokenService, TwitchTokenService>();
        services.AddHttpClient<IGamesService, InternetGamesDatabaseService>();
        
        var serviceProvider = services.BuildServiceProvider();
        var ep = serviceProvider.GetRequiredService<Picker>();
        var cts = new CancellationTokenSource();
        var ct = cts.Token;
        var name = await ep.PickGameNameAsync(ct) ?? "<!> Unable to get a game name.";

        Console.WriteLine(name);
    }
}