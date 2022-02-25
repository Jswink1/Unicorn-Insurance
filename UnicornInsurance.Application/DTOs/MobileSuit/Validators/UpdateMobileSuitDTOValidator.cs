using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.MobileSuit.Validators
{
    public class UpdateMobileSuitDTOValidator : AbstractValidator<FullMobileSuitDTO>
    {
        public UpdateMobileSuitDTOValidator()
        {
            Include(new IMobileSuitDTOValidator());

            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
