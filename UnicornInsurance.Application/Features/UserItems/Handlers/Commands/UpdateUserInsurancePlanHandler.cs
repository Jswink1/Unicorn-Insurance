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
using UnicornInsurance.Application.DTOs.UserMobileSuit.Validators;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.UserItems.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.UserItems.Handlers.Commands
{
    public class UpdateUserInsurancePlanHandler : IRequestHandler<UpdateUserInsurancePlanCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserInsurancePlanHandler(IUnitOfWork unitOfWork,
                                              IMapper mapper,
                                              IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(UpdateUserInsurancePlanCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new UpdateUserMobileSuitDTOValidator().ValidateAsync(request.UserInsurancePlanDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "User Mobile Suit Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

                var userMobileSuit = await _unitOfWork.UserMobileSuitRepository.Get(request.UserInsurancePlanDTO.UserMobileSuitId);

                if (userMobileSuit is null)
                    throw new NotFoundException(nameof(userMobileSuit), request.UserInsurancePlanDTO.UserMobileSuitId);
                if (userMobileSuit.ApplicationUserId != userId)
                    throw new UnauthorizedAccessException();

                if (request.UserInsurancePlanDTO.InsurancePlan.ToLower() == SD.StandardInsurancePlan ||
                    request.UserInsurancePlanDTO.InsurancePlan.ToLower() == SD.SuperInsurancePlan ||
                    request.UserInsurancePlanDTO.InsurancePlan.ToLower() == SD.UltraInsurancePlan)
                {
                    userMobileSuit.InsurancePlan = request.UserInsurancePlanDTO.InsurancePlan;
                    userMobileSuit.EndOfCoverage = DateTime.Now.AddDays(1);
                    await _unitOfWork.Save();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid Insurance Plan Name";
                    return response;
                }

                response.Success = true;
                response.Message = "Weapon Equipped Successfully";
            }
            return response;
        }
    }
}
