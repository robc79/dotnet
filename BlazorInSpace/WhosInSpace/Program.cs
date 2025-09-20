using WhosInSpace.Services;

namespace WhosInSpace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<IAstrosService, HttpAstrosService>();

            builder.Services.AddHttpClient<IAstrosService, HttpAstrosService>(
                (provider, client) =>
                {
                    var config = provider.GetRequiredService<IConfiguration>();
                    var baseUrl = config.GetValue<string>("Astros:BaseUrl");

                    if (string.IsNullOrWhiteSpace(baseUrl))
                    {
                        throw new InvalidOperationException("Astros:BaseUrl not set.");
                    }

                    client.BaseAddress = new Uri(baseUrl!);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}