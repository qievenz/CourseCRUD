using Core.CourseCRUD.Entities;
using FluentValidation.Results;

namespace Core.CourseCRUD.Services
{
    public interface ICourseService
    {
        Task<ValidationResult> ValidateAndAddCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<List<Course>> GetCourseByDescription(string description);
        Task<IEnumerable<Course>> GetCoursesAsync();
    }
}