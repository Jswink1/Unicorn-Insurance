using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;

namespace UnicornInsurance.Application.DTOs.UserMobileSuit.Validators
{
    public class UpdateUserMobileSuitDTOValidator : AbstractValidator<UserInsurancePlanDTO>
    {
        public UpdateUserMobileSuitDTOValidator()
        {
            List<string> ValidInsuranceTypes = new List<string>() 
            { 
                SD.StandardInsurancePlan,
                SD.SuperInsurancePlan,
                SD.UltraInsurancePlan
            };

            RuleFor(p => p.InsurancePlan)
                .Must(x => ValidInsuranceTypes.Contains(x))
                .WithMessage("Deployment Result Type must be " + String.Join(" or ", ValidInsuranceTypes));

            RuleFor(p => p.UserMobileSuitId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
