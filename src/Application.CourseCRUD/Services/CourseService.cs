using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Repositories;
using Core.CourseCRUD.Services;
using FluentValidation;
using System.Collections;
using System.Text.RegularExpressions;

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

            var existingCourse = await _courseRepository.FindCourseAsync(course.Subject, course.CourseNumber);
            if (existingCourse != null)
            {
                throw new InvalidOperationException("Duplicate course detected");
            }

            await _courseRepository.AddCourseAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteCourseAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _courseRepository.GetCoursesAsync();
        }

        // Otras operaciones como buscar, eliminar, etc.
    }

}
