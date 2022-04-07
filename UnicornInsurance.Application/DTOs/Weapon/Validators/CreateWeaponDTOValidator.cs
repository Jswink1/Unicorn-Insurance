using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.Weapon.Validators
{
    public class CreateWeaponDTOValidator : AbstractValidator<CreateWeaponDTO>
    {
        public CreateWeaponDTOValidator()
        {
            Include(new IWeaponDTOValidator());

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be at greater than {ComparisonValue}.")
                .LessThan(100000).WithMessage("{PropertyName} must be less than 100,000.");
        }
    }
}
