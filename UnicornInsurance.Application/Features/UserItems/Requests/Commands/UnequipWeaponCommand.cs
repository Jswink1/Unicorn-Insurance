using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.UserItems.Requests.Commands
{
    public class UnequipWeaponCommand : IRequest
    {
        public int UserMobileSuitId { get; set; }
    }
}
