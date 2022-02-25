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
using UnicornInsurance.Application.DTOs.MobileSuitCartItem.Validators;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class CreateMobileSuitCartHandler : IRequestHandler<CreateMobileSuitCartCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateMobileSuitCartHandler(IUnitOfWork unitOfWork,
                                           IMapper mapper,
                                           IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(CreateMobileSuitCartCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new CreateMobileSuitCartItemDTOValidator().ValidateAsync(request.MobileSuitCartItem);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Add to Cart Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;

                var exists = await _unitOfWork.MobileSuitCartRepository.CartItemExists(userId, request.MobileSuitCartItem.MobileSuitId);

                if (exists == true)
                {
                    response.Success = false;
                    response.Message = "User can only have one of each type of mobile suit";
                }
                else
                {
                    var mobileSuitCartItem = _mapper.Map<MobileSuitCartItem>(request.MobileSuitCartItem);
                    mobileSuitCartItem.ApplicationUserId = userId;

                    mobileSuitCartItem = await _unitOfWork.MobileSuitCartRepository.Add(mobileSuitCartItem);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Item Added to Cart";
                    response.Id = mobileSuitCartItem.Id;
                }
            }

            return response;
        }
    }
}
