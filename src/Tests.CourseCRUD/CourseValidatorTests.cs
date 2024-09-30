using Application.CourseCRUD.Validators;
using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Repositories;
using FluentValidation.TestHelper;
using Moq;

namespace Tests.CourseCRUD
{
    public class CourseValidatorTests
    {
        private CourseValidator _validator;
        private Mock<ICourseRepository> _courseRepository;

        [SetUp]
        public void Setup()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _validator = new CourseValidator(_courseRepository.Object);
        }

        [Test]
        public async Task WhenSubjectIsEmpty_ShouldHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "",
                CourseNumber = "101",
                Description = "Test Description"
            };

            var result = await _validator.TestValidateAsync(course);

            result.ShouldHaveValidationErrorFor(x => x.Subject);
        }

        [Test]
        public async Task WhenSubjectIsFull_ShouldNotHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "101",
                Description = "Test Description"
            };

            var result = await _validator.TestValidateAsync(course);

            result.ShouldNotHaveValidationErrorFor(x => x.Subject);
        }

        [Test]
        public async Task WhenCourseNumberIsNot3Digit_ShouldHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "21",
                Description = "Test Description"
            };

            var result = await _validator.TestValidateAsync(course);

            result.ShouldHaveValidationErrorFor(x => x.CourseNumber);
        }

        [Test]
        public async Task WhenCourseNumberIsNotInteger_ShouldHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "qwe",
                Description = "Test Description"
            };

            var result = await _validator.TestValidateAsync(course);

            result.ShouldHaveValidationErrorFor(x => x.CourseNumber);
        }

        [Test]
        public async Task WhenCourseNumberIs3DigitInteger_ShouldNotHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "001",
                Description = "Test Description"
            };

            var result = await _validator.TestValidateAsync(course);

            result.ShouldNotHaveValidationErrorFor(x => x.CourseNumber);
        }

        [Test]
        public async Task WhenDescriptionIsEmpty_ShouldHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "001",
                Description = ""
            };

            var result = await _validator.TestValidateAsync(course);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Test]
        public async Task WhenDescriptionIsFull_ShouldNotHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "001",
                Description = "Test"
            };

            var result = await _validator.TestValidateAsync(course);

            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Test]
        public async Task WhenCourseAlreadyExists_ShouldHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "001",
                Description = "Test"
            };

            _courseRepository.Setup(x => x.FindCourseAsync(course.Subject, course.CourseNumber)).ReturnsAsync(course);

            var result = await _validator.TestValidateAsync(course);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Test]
        public async Task WhenCourseDontExists_ShouldNotHaveErrorAsync()
        {
            var course = new Course
            {
                Subject = "Test",
                CourseNumber = "001",
                Description = "Test"
            };
            var existingCourse = new Course
            {
                Subject = "Test 1",
                CourseNumber = "002",
                Description = "Test"
            };

            _courseRepository.Setup(x => x.FindCourseAsync(course.Subject, course.CourseNumber)).ReturnsAsync(existingCourse);

            var result = await _validator.TestValidateAsync(course);

            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}
