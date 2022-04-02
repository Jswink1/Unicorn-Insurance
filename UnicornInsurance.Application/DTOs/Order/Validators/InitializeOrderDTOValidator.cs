using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.Order.Validators
{
    public class InitializeOrderDTOValidator : AbstractValidator<InitializeOrderDTO>
    {
        public InitializeOrderDTOValidator()
        {
            RuleForEach(p => p.MobileSuitPurchases).ChildRules(MobileSuitPurchases =>
            {
                MobileSuitPurchases.RuleFor(p => p.MobileSuitId)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .NotNull();
            });

            RuleForEach(p => p.WeaponPurchases).ChildRules(WeaponPurchases =>
            {
                WeaponPurchases.RuleFor(p => p.WeaponId)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .NotNull();

                WeaponPurchases.RuleFor(p => p.Count)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .NotNull()
                    .GreaterThan(0);
            });
        }
    }
}
