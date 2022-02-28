using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.OrderHeader.Validators
{
    public class InitializeOrderHeaderDTOValidator : AbstractValidator<InitializeOrderHeaderDTO>
    {
        public InitializeOrderHeaderDTOValidator()
        {
            Include(new IOrderHeaderDTOValidator());
        }
    }
}
