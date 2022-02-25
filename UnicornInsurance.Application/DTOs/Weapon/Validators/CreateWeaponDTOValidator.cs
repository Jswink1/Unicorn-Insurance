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
        }
    }
}
