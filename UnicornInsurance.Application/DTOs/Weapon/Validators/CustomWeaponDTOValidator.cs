using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.Weapon.Validators
{
    public class CustomWeaponDTOValidator : AbstractValidator<CustomWeaponDTO>
    {
        public CustomWeaponDTOValidator()
        {
            Include(new IWeaponDTOValidator());
        }
    }
}
