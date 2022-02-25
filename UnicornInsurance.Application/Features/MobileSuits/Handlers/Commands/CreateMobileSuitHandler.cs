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
            ValidationResult validationResult = new();

            if (request.CreateMobileSuitDTO.CustomWeapon is null)
                validationResult = await new CreateMobileSuitDTOValidator().ValidateAsync(request.CreateMobileSuitDTO);

            else if (request.CreateMobileSuitDTO.CustomWeapon is not null)
                validationResult = await new CreateFullMobileSuitDTOValidator().ValidateAsync(request.CreateMobileSuitDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var mobileSuit = _mapper.Map<MobileSuit>(request.CreateMobileSuitDTO);

                if (request.CreateMobileSuitDTO.CustomWeapon is not null)
                    mobileSuit.CustomWeapon.IsCustomWeapon = true;

                mobileSuit = await _unitOfWork.MobileSuitRepository.Add(mobileSuit);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = mobileSuit.Id;
            }

            return response;
        }
    }
}
