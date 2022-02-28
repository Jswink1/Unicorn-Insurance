using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderHeader.Validators
{
    public class IOrderHeaderDTOValidator : AbstractValidator<IOrderHeaderDTO>
    {
        public IOrderHeaderDTOValidator()
        {
            RuleFor(p => p.OrderTotal)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0);
        }
    }
}
