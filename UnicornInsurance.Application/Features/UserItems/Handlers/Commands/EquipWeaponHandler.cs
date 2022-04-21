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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EquipWeaponHandler(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
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
                    throw new NotFoundException("User Mobile Suit", request.EquipWeaponDTO.UserMobileSuitId);
                if (selectedWeapon is null)
                    throw new NotFoundException("User Weapon", request.EquipWeaponDTO.SelectedWeaponId);

                if (userMobileSuit.ApplicationUserId != userId ||
                    selectedWeapon.ApplicationUserId != userId)
                    throw new UnauthorizedAccessException();

                if (selectedWeapon.IsCustomWeapon == true)
                    throw new EquipCustomWeaponException();
                if (selectedWeapon.EquippedMobileSuitId != null)
                    throw new WeaponEquippedException();

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
