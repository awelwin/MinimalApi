using Alex.MinimalApi.Service.Presentation;
using FluentValidation;

namespace Alex.MinimalApi.Service.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.Firstname).MaximumLength(255).NotEmpty();
            RuleFor(e => e.Lastname).MaximumLength(255).NotEmpty();
            RuleFor(e => e.Age).InclusiveBetween(1, 150).NotNull();
        }
    }
}
