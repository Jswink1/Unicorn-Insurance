using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.Order.Validators
{
    public class OrderHeaderCompletionDTOValidator : AbstractValidator<CompleteOrderDTO>
    {
        public OrderHeaderCompletionDTOValidator()
        {
            RuleFor(p => p.OrderId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.TransactionSuccess)
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
