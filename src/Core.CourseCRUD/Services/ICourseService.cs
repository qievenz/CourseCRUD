using Core.CourseCRUD.Entities;

namespace Core.CourseCRUD.Services
{
    public interface ICourseService
    {
        Task AddCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<List<Course>> GetCourseByDescription(string description);
        Task<IEnumerable<Course>> GetCoursesAsync();
    }
}