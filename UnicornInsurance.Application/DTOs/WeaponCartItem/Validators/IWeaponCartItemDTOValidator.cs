using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.WeaponCartItem.Validators
{
    public class IWeaponCartItemDTOValidator : AbstractValidator<IWeaponCartItemDTO>
    {
        public IWeaponCartItemDTOValidator()
        {
            RuleFor(p => p.WeaponId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
