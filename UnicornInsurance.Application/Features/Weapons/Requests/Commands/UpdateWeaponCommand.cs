using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Weapons.Requests.Commands
{
    public class UpdateWeaponCommand : IRequest<BaseCommandResponse>
    {
        public WeaponDTO WeaponDTO { get; set; }
    }
}
