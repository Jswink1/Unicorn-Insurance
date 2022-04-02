using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Weapons.Requests.Queries;

namespace UnicornInsurance.Application.Features.Weapons.Handlers.Queries
{
    public class GetWeaponDetailsHandler : IRequestHandler<GetWeaponDetailsRequest, WeaponDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWeaponDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<WeaponDTO> Handle(GetWeaponDetailsRequest request, CancellationToken cancellationToken)
        {
            var weapon = await _unitOfWork.WeaponRepository.Get(request.WeaponId);

            if (weapon is null)
                throw new NotFoundException(nameof(weapon), request.WeaponId);

            return _mapper.Map<WeaponDTO>(weapon);
        }
    }    
}
