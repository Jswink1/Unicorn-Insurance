using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.MobileSuitCartItem.Validators
{
    public class IMobileSuitCartItemDTOValidator : AbstractValidator<IMobileSuitCartItemDTO>
    {
        public IMobileSuitCartItemDTOValidator()
        {
            RuleFor(p => p.MobileSuitId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
