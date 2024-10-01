﻿using Infrastructure.CourseCRUD.Persistence;
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
                var connected = false;

                while (!connected)
                {
                    try
                    {
                        context.Database.Migrate();
                        connected = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Database is not ready yet: {ex.Message}. Retrying in 5 seconds...");
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                    }
                }
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
