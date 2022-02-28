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
            Include(new IOrderDetailsDTOValidator());
        }
    }
}
