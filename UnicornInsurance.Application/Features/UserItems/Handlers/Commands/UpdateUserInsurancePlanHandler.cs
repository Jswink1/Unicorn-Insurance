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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserInsurancePlanHandler(IUnitOfWork unitOfWork,
                                              IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(UpdateUserInsurancePlanCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new UpdateUserMobileSuitDTOValidator().ValidateAsync(request.UserInsurancePlanDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Insurance Plan Update Failure";
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
                if (userMobileSuit.IsDamaged == true)
                    throw new MobileSuitDamagedException();

                userMobileSuit.InsurancePlan = request.UserInsurancePlanDTO.InsurancePlan;
                userMobileSuit.EndOfCoverage = DateTime.UtcNow.AddDays(1);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Insurance Plan Update Success";
            }
            return response;
        }
    }
}
