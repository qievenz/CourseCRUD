using Core.CourseCRUD.Entities;
using FluentValidation;

namespace Application.CourseCRUD.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(course => course.Subject)
                .NotEmpty()
                .WithMessage("Subject is required.");

            RuleFor(x => x.CourseNumber)
                .Must(x => x.Length == 3)
                .WithMessage("CourseNumber must be a three-digit number");
            
            RuleFor(course => course.Description)
                .NotEmpty()
                .WithMessage("Description is required.");
        }
    }
}
