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
using UnicornInsurance.Application.Features.Weapons.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.Weapons.Handlers.Commands
{
    public class CreateWeaponHandler : IRequestHandler<CreateWeaponCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWeaponHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateWeaponCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateWeaponDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateWeaponDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Weapon Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var weapon = _mapper.Map<Weapon>(request.CreateWeaponDTO);
                weapon.IsCustomWeapon = false;

                weapon = await _unitOfWork.WeaponRepository.Add(weapon);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Weapon Creation Successful";
                response.Id = weapon.Id;
            }

            return response;
        }
    }
}
