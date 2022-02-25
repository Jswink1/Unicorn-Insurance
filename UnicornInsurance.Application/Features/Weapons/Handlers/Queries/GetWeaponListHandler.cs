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
using UnicornInsurance.Application.Features.Weapons.Requests.Queries;

namespace UnicornInsurance.Application.Features.Weapons.Handlers.Queries
{
    public class GetWeaponListHandler : IRequestHandler<GetWeaponListRequest, List<WeaponDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWeaponListHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<WeaponDTO>> Handle(GetWeaponListRequest request, CancellationToken cancellationToken)
        {
            var weapons = await _unitOfWork.WeaponRepository.GetStandardWeaponsList();
            return _mapper.Map<List<WeaponDTO>>(weapons);
        }
    }
}
