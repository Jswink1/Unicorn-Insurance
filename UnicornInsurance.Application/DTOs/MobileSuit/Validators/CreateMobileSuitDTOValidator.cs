using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.MobileSuit.Validators
{
    public class CreateMobileSuitDTOValidator : AbstractValidator<CreateFullMobileSuitDTO>
    {
        public CreateMobileSuitDTOValidator()
        {
            Include(new IMobileSuitDTOValidator());
        }
    }
}
