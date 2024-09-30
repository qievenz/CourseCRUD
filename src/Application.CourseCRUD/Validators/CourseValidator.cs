using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Repositories;
using FluentValidation;

namespace Application.CourseCRUD.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        private readonly ICourseRepository _courseRepository;

        public CourseValidator(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;

            RuleFor(course => course.Subject)
                .NotEmpty()
                .WithMessage("Subject is required.");

            RuleFor(x => x.CourseNumber)
                .Must(x => x.Length == 3)
                .Must(x => int.TryParse(x, out _))
                .WithMessage("CourseNumber must be a three-digit number");

            RuleFor(course => course.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(course => course)
                .MustAsync(async (course, cancellation) =>
                {
                    var existingCourse = await _courseRepository.FindCourseAsync(course.Subject, course.CourseNumber);

                    return existingCourse == null;
                })
                .WithMessage("Duplicate course detected.");
        }
    }
}
