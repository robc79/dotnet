using Microsoft.EntityFrameworkCore;
using Fragment.Infrastructure.Sql;
using Fragment.Domain.Repositories;
using Fragment.Infrastructure.Sql.Repositories;
using Fragment.Application.ListTags;
using Fragment.Application.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(path: "Logs\\log.json", rollingInterval: RollingInterval.Day, formatter: new JsonFormatter())
    .CreateLogger();

Log.Information("Atomic batteries to power, turbines to speed.");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    
    // Add services to the container.
    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<FragmentDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("Fragment");
        options.UseSqlite(connectionString);
    });

    var searchPageConfiguration = new SearchPageConfiguration();
    builder.Configuration.GetSection(SearchPageConfiguration.SectionName).Bind(searchPageConfiguration);
    builder.Services.AddSingleton(searchPageConfiguration);

    builder.Services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<FragmentDbContext>());
    builder.Services.AddScoped<ITagRepository, TagRepository>();
    builder.Services.AddScoped<ITextFragmentRepository, TextFragmentRepository>();
    builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<ListTagsHandler>());

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

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start.");
}
finally
{
    Log.Information("Application shutdown complete.");
    Log.CloseAndFlush();
}