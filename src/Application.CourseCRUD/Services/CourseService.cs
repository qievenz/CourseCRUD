using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Repositories;
using Core.CourseCRUD.Services;
using FluentValidation;
using FluentValidation.Results;

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

        public async Task<ValidationResult> ValidateAndAddCourseAsync(Course course)
        {
            var result = await _validator.ValidateAsync(course);

            if (result.IsValid)
                await _courseRepository.AddCourseAsync(course);

            return result;
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
