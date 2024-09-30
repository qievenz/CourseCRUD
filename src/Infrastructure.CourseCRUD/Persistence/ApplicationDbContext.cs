using Core.CourseCRUD.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CourseCRUD.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}
