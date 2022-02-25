using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Weapon;

namespace UnicornInsurance.Application.Features.Weapons.Requests.Queries
{
    public class GetWeaponListRequest : IRequest<List<WeaponDTO>>
    {        
    }
}
