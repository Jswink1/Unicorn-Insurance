using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.UserWeapon;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.UserItems.Requests.Commands
{
    public class EquipWeaponCommand : IRequest<BaseCommandResponse>
    {
        public EquipWeaponDTO EquipWeaponDTO { get; set; }
    }
}
