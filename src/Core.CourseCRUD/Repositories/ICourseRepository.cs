using Core.CourseCRUD.Entities;

namespace Core.CourseCRUD.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<Course> FindCourseAsync(string subject, string courseNumber);
    }
}
