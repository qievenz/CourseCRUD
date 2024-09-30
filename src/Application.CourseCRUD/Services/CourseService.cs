using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Repositories;
using Core.CourseCRUD.Services;
using FluentValidation;

namespace Application.CourseCRUD.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IValidator<Course> _validator;

        public CourseService(ICourseRepository courseRepository, IValidator<Course> validator)
        {
            _courseRepository = courseRepository;
            _validator = validator;
        }

        public async Task AddCourseAsync(Course course)
        {
            await _validator.ValidateAndThrowAsync(course);
            await _courseRepository.AddCourseAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
        }

        public async Task<List<Course>> GetCourseByDescription(string description)
        {
            return await _courseRepository.GetCourseByDescription(description);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _courseRepository.GetCoursesAsync();
        }
    }
}
