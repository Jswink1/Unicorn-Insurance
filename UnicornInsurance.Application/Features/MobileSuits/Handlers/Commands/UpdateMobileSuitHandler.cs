using AutoMapper;
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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.MobileSuits.Handlers.Commands
{
    public class UpdateMobileSuitHandler : IRequestHandler<UpdateMobileSuitCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMobileSuitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateMobileSuitCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validationResult = await new UpdateMobileSuitDTOValidator().ValidateAsync(request.MobileSuitDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Mobile Suit Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                return response;
            }

            if (request.MobileSuitDTO.CustomWeapon is not null)
            {
                if (string.IsNullOrWhiteSpace(request.MobileSuitDTO.CustomWeapon.Name) ||
                    string.IsNullOrWhiteSpace(request.MobileSuitDTO.CustomWeapon.Description))
                {
                    request.MobileSuitDTO.CustomWeapon = null;
                }
            }

            var mobileSuit = await _unitOfWork.MobileSuitRepository.GetFullMobileSuitDetails(request.MobileSuitDTO.Id);

            if (mobileSuit is null)
                throw new NotFoundException(nameof(mobileSuit), request.MobileSuitDTO.Id);

            // If the mobile suit previously had a custom weapon, and the updated mobile suit has no custom weapon
            if (mobileSuit.CustomWeapon is not null &&
                request.MobileSuitDTO.CustomWeapon is null)
            {
                // Delete the previous custom weapon
                await _unitOfWork.WeaponRepository.Delete(mobileSuit.CustomWeapon);

                _mapper.Map(request.MobileSuitDTO, mobileSuit);
            }

            // if the updated mobile suit does have a custom weapon
            else if (request.MobileSuitDTO.CustomWeapon is not null)
            {
                // Validate the custom weapon values
                validationResult = await new CustomWeaponDTOValidator().ValidateAsync(request.MobileSuitDTO.CustomWeapon);

                if (validationResult.IsValid == false)
                {
                    response.Success = false;
                    response.Message = "Mobile Suit Update Failed";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                    return response;
                }

                _mapper.Map(request.MobileSuitDTO, mobileSuit);
                mobileSuit.CustomWeapon.IsCustomWeapon = true;
                mobileSuit.CustomWeapon.Price = 1;
            }

            // If the mobile suit previously did not have a custom weapon, and the update mobile suit still has no custom weapon
            else
            {
                _mapper.Map(request.MobileSuitDTO, mobileSuit);
            }

            await _unitOfWork.MobileSuitRepository.Update(mobileSuit);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Mobile Suit Update Successful";
            response.Id = mobileSuit.Id;
            
            return response;
        }
    }
}
