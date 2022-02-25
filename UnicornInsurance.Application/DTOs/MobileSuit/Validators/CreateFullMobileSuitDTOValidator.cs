using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.MobileSuit.Validators
{
    public class CreateFullMobileSuitDTOValidator : AbstractValidator<CreateFullMobileSuitDTO>
    {
        public CreateFullMobileSuitDTOValidator()
        {
            Include(new IMobileSuitDTOValidator());

            RuleFor(p => p.CustomWeapon.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.CustomWeapon.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.CustomWeapon.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be at greater than {ComparisonValue}.")
                .LessThan(100000).WithMessage("{PropertyName} must be less than 100,000.");
        }
    }
}
