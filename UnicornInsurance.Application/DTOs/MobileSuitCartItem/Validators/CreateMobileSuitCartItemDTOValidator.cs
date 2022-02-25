using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;

namespace UnicornInsurance.Application.DTOs.MobileSuitCartItem.Validators
{
    public class CreateMobileSuitCartItemDTOValidator : AbstractValidator<CreateMobileSuitCartItemDTO>
    {
        public CreateMobileSuitCartItemDTOValidator()
        {
            Include(new IMobileSuitCartItemDTOValidator());
        }
    }
}
