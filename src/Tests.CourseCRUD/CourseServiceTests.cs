using Application.CourseCRUD.Services;
using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Repositories;
using FluentValidation;
using Moq;

namespace Tests.CourseCRUD
{
    public class CourseServiceTests
    {
        private CourseService _courseService;
        private Mock<ICourseRepository> _courseRepository;
        private Mock<IValidator<Course>> _validator;

        [SetUp]
        public void Setup()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _validator = new Mock<IValidator<Course>>();
            _courseService = new CourseService(_courseRepository.Object, _validator.Object);
        }

        [Test]
        public async Task AddCourseAsync_ShouldAddToRepositoryAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "101",
                Description = "Test Description"
            };

            _validator.Setup(x => x.ValidateAsync(course, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            await _courseService.ValidateAndAddCourseAsync(course);

            _courseRepository.Verify(x => x.AddCourseAsync(course), Times.Once);
        }

        [Test]
        public async Task DeleteCourseAsync_ShouldDeleteFromRepositoryAsync()
        {
            var course = new Course
            {
                Id = 1,
                Subject = "Test",
                CourseNumber = "101",
                Description = "Test Description"
            };

            await _courseService.DeleteCourseAsync(course.Id);

            _courseRepository.Verify(x => x.DeleteCourseAsync(course.Id), Times.Once);
        }
    }
}