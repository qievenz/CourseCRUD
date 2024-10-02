using API.CourseCRUD.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;
IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddServices(configuration);

var app = builder.Build();

app.AddUsings();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
