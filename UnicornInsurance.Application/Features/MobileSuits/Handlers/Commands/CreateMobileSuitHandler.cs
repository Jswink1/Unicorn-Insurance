using AutoMapper;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.MobileSuit.Validators;
using UnicornInsurance.Application.DTOs.Weapon.Validators;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.MobileSuits.Handlers.Commands
{
    public class CreateMobileSuitHandler : IRequestHandler<CreateMobileSuitCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMobileSuitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateMobileSuitCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new CreateMobileSuitDTOValidator().ValidateAsync(request.CreateMobileSuitDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                return response;
            }

            var mobileSuit = _mapper.Map<MobileSuit>(request.CreateMobileSuitDTO);

            // If a mobile suit with a custom weapon is being inserted
            if (request.CreateMobileSuitDTO.CustomWeapon is not null &&
                string.IsNullOrWhiteSpace(request.CreateMobileSuitDTO.CustomWeapon.Name) == false &&
                string.IsNullOrWhiteSpace(request.CreateMobileSuitDTO.CustomWeapon.Description) == false)
            {
                // Validate the custom weapon values
                validationResult = await new CustomWeaponDTOValidator().ValidateAsync(request.CreateMobileSuitDTO.CustomWeapon);

                if (validationResult.IsValid == false)
                {
                    response.Success = false;
                    response.Message = "Creation Failed";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                    return response;
                }

                mobileSuit.CustomWeapon.IsCustomWeapon = true;
                mobileSuit.CustomWeapon.Price = 1;
            }
            // If a mobile suit without a custom weapon is being inserted
            else
            {
                mobileSuit.CustomWeapon = null;
            }

            mobileSuit = await _unitOfWork.MobileSuitRepository.Add(mobileSuit);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = mobileSuit.Id;

            return response;
        }
    }
}
