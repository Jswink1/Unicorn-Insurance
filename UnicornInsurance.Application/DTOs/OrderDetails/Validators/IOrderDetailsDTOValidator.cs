using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderDetails.Validators
{
    public class IOrderDetailsDTOValidator : AbstractValidator<IOrderDetailsDTO>
    {
        public IOrderDetailsDTOValidator()
        {
            RuleFor(p => p.OrderHeaderId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
