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
    public class GetWeaponDetailsHandler : IRequestHandler<GetWeaponDetailsRequest, FullWeaponDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWeaponDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FullWeaponDTO> Handle(GetWeaponDetailsRequest request, CancellationToken cancellationToken)
        {
            var weapon = await _unitOfWork.WeaponRepository.Get(request.Id);

            if (weapon is null)
                throw new NotFoundException(nameof(weapon), request.Id);

            return _mapper.Map<FullWeaponDTO>(weapon);
        }
    }    
}
