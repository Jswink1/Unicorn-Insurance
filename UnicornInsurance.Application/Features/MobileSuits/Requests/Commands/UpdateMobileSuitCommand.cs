using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.MobileSuits.Requests.Commands
{
    public class UpdateMobileSuitCommand : IRequest<BaseCommandResponse>
    {
        public FullMobileSuitDTO MobileSuitDTO { get; set; }
    }
}
