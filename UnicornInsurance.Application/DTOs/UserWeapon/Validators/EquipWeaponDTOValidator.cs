using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.UserWeapon.Validators
{
    public class EquipWeaponDTOValidator : AbstractValidator<EquipWeaponDTO>
    {
        public EquipWeaponDTOValidator()
        {
            RuleFor(p => p.UserMobileSuitId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SelectedWeaponId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
