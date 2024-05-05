using FluentValidation;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<Pres.Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.Firstname).MaximumLength(255).NotEmpty();
            RuleFor(e => e.Lastname).MaximumLength(255).NotEmpty();
            RuleFor(e => e.Age).InclusiveBetween(1, 150).NotNull();
        }
    }
}
