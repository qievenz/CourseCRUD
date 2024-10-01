using Infrastructure.CourseCRUD.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.CourseCRUD.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication AddUsings(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                Task.Delay(30000);
                context.Database.Migrate();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();

            return app;
        }
    }
}
