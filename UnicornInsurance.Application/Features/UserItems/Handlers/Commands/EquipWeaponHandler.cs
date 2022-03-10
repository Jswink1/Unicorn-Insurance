using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.UserWeapon.Validators;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.UserItems.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.UserItems.Handlers.Commands
{
    public class EquipWeaponHandler : IRequestHandler<EquipWeaponCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EquipWeaponHandler(IUnitOfWork unitOfWork,
                                  IMapper mapper,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(EquipWeaponCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new EquipWeaponDTOValidator().ValidateAsync(request.EquipWeaponDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Weapon Equip Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

                var userMobileSuit = await _unitOfWork.UserMobileSuitRepository.Get(request.EquipWeaponDTO.UserMobileSuitId);
                var selectedWeapon = await _unitOfWork.UserWeaponRepository.Get(request.EquipWeaponDTO.SelectedWeaponId);

                if (userMobileSuit is null)
                    throw new NotFoundException(nameof(userMobileSuit), request.EquipWeaponDTO.UserMobileSuitId);
                if (selectedWeapon is null)
                    throw new NotFoundException(nameof(selectedWeapon), request.EquipWeaponDTO.SelectedWeaponId);

                if (userMobileSuit.ApplicationUserId != userId ||
                    selectedWeapon.ApplicationUserId != userId)
                    throw new UnauthorizedAccessException();

                var currentlyEquippedWeapon = await _unitOfWork.UserWeaponRepository.GetUserMobileSuitEquippedWeapon(request.EquipWeaponDTO.UserMobileSuitId);

                if (currentlyEquippedWeapon is not null)
                {
                    currentlyEquippedWeapon.EquippedMobileSuitId = null;                   
                }

                await _unitOfWork.UserWeaponRepository.EquipWeapon(request.EquipWeaponDTO.SelectedWeaponId, request.EquipWeaponDTO.UserMobileSuitId);

                response.Success = true;
                response.Message = "Weapon Equipped Successfully";
            }

            return response;
        }
    }
}
