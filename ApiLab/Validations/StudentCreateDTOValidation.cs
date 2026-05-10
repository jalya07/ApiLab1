using ApiLab.DTOs.Student;
using FluentValidation;

namespace ApiLab.Validations;

public class StudentCreateDTOValidation:AbstractValidator<CreateStudentDto>
{
    public StudentCreateDTOValidation()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName can't be empty")
            .MinimumLength(3).WithMessage("FullName must contain 3 characters");

        RuleFor(x => x.Age)
            .InclusiveBetween(16, 60).WithMessage("Age must be between 16 and 60");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email can't be empty")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.GroupId)
            .GreaterThan(0).WithMessage("GroupId must be greater than 0");
    }
}