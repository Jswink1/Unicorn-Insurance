using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.UserMobileSuit.Validators
{
    public class UpdateUserMobileSuitDTOValidator : AbstractValidator<UserInsurancePlanDTO>
    {
        public UpdateUserMobileSuitDTOValidator()
        {
            RuleFor(p => p.UserMobileSuitId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.InsurancePlan)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
