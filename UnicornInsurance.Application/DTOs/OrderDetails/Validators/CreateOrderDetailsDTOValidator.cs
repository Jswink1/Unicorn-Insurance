using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderDetails.Validators
{
    public class CreateOrderDetailsDTOValidator : AbstractValidator<CreateOrderDetailsDTO>
    {
        public CreateOrderDetailsDTOValidator()
        {
            RuleFor(p => p.OrderHeaderId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
