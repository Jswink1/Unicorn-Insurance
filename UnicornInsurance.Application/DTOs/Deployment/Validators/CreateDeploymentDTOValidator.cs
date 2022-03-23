using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;

namespace UnicornInsurance.Application.DTOs.Deployment.Validators
{
    public class CreateDeploymentDTOValidator : AbstractValidator<CreateDeploymentDTO>
    {
        public CreateDeploymentDTOValidator()
        {
            List<string> ValidResultTypes = new List<string>() { SD.GoodDeploymentResult, SD.BadDeploymentResult };

            RuleFor(p => p.ResultType)
                .Must(x => ValidResultTypes.Contains(x))
                .WithMessage("Deployment Result Type must be " + String.Join(" or ", ValidResultTypes));

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
