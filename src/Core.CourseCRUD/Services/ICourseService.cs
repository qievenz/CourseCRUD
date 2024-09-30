using Core.CourseCRUD.Entities;

namespace Core.CourseCRUD.Services
{
    public interface ICourseService
    {
        Task AddCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<IEnumerable<Course>> GetCoursesAsync();
    }
}