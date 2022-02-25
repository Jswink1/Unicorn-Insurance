using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.MobileSuit.Validators
{
    public class IMobileSuitDTOValidator : AbstractValidator<IMobileSuitDTO>
    {
        public IMobileSuitDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be at greater than {ComparisonValue}.")
                .LessThan(1000000).WithMessage("{PropertyName} must be less than 1,000,000.");

            RuleFor(p => p.Type).MaximumLength(200).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(p => p.Manufacturer).MaximumLength(100).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(p => p.Height).MaximumLength(100).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(p => p.Weight).MaximumLength(100).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(p => p.PowerOutput).MaximumLength(100).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            RuleFor(p => p.Armor).MaximumLength(100).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
