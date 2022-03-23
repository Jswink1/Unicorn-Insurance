using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;

namespace UnicornInsurance.Application.DTOs.Deployment.Validators
{
    public class UpdateDeploymentDTOValidator : AbstractValidator<DeploymentDTO>
    {
        public UpdateDeploymentDTOValidator()
        {
            List<string> ValidResultTypes = new List<string>() { SD.GoodDeploymentResult, SD.BadDeploymentResult };

            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} must be present");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.ResultType)
                .Must(x => ValidResultTypes.Contains(x))
                .WithMessage("Deployment Result Type must be " + String.Join(" or ", ValidResultTypes));
        }
    }
}
