using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.Weapon.Validators;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Weapons.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Weapons.Handlers.Commands
{
    public class UpdateWeaponHandler : IRequestHandler<UpdateWeaponCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWeaponHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateWeaponCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateWeaponDTOValidator();
            var validationResult = await validator.ValidateAsync(request.WeaponDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Weapon Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var weapon = await _unitOfWork.WeaponRepository.Get(request.WeaponDTO.Id);

                if (weapon is null)
                    throw new NotFoundException(nameof(weapon), request.WeaponDTO.Id);

                if (weapon.IsCustomWeapon == true)
                    throw new UpdateCustomWeaponException();

                _mapper.Map(request.WeaponDTO, weapon);

                await _unitOfWork.WeaponRepository.Update(weapon);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Weapon Update Successful";
                response.Id = weapon.Id;
            }

            return response;
        }
    }
}
