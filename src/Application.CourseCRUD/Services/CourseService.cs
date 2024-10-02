using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Repositories;
using Core.CourseCRUD.Services;
using FluentValidation;
using FluentValidation.Results;
using Serilog;
using System.Text.Json;

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
            try
            {
                var result = await _validator.ValidateAsync(course);

                if (result.IsValid)
                    await _courseRepository.AddCourseAsync(course);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error adding course: {JsonSerializer.Serialize(course)}");
                throw;
            }
        }

        public async Task DeleteCourseAsync(int id)
        {
            try
            {
                await _courseRepository.DeleteCourseAsync(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error deleting course id: {id}");
                throw;
            }
        }

        public async Task<List<Course>> GetCourseByDescription(string description)
        {
            try
            {
                return await _courseRepository.GetCourseByDescription(description);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error searching course description: {description}");
                throw;
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            Log.Information("Getting courses");
            try
            {
                return await _courseRepository.GetCoursesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error getting courses");
                throw;
            }
        }
    }
}
