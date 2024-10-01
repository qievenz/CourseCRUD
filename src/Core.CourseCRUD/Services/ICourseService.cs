using Core.CourseCRUD.Entities;

namespace Core.CourseCRUD.Services
{
    public interface ICourseService
    {
        Task ValidateAndAddCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<List<Course>> GetCourseByDescription(string description);
        Task<IEnumerable<Course>> GetCoursesAsync();
    }
}