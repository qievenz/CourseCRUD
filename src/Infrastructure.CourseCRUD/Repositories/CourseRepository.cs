using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Repositories;
using Infrastructure.CourseCRUD.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CourseCRUD.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Course> FindCourseAsync(string subject, string courseNumber)
        {
            return await _context.Courses
                                 .FirstOrDefaultAsync(c => c.Subject == subject && c.CourseNumber == courseNumber);
        }
    }

}
