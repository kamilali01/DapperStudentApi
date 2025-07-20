using DapperStudentApi.Models;
using FluentValidation;

namespace DapperStudentApi.Validators
{
    public class StudentValidator: AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 100).WithMessage("Age must be between 1 and 100.");

        }
    }
}
