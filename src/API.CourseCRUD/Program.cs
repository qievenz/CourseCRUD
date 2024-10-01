using API.CourseCRUD.Extensions;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;
IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

builder.Services.AddServices(configuration);

var app = builder.Build();

app.AddUsings();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
