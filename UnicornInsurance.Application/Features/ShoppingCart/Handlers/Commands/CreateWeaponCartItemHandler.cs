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
using UnicornInsurance.Application.DTOs.WeaponCartItem.Validators;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class CreateWeaponCartItemHandler : IRequestHandler<CreateWeaponCartItemCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateWeaponCartItemHandler(IUnitOfWork unitOfWork,
                                       IMapper mapper,
                                       IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(CreateWeaponCartItemCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new CreateWeaponCartItemDTOValidator().ValidateAsync(request.WeaponCartItem);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Add to Cart Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

                var weaponCartItem = await _unitOfWork.WeaponCartRepository.GetCartItem(userId, request.WeaponCartItem.WeaponId);

                if (weaponCartItem is null)
                {
                    weaponCartItem = _mapper.Map<WeaponCartItem>(request.WeaponCartItem);
                    weaponCartItem.ApplicationUserId = userId;

                    weaponCartItem = await _unitOfWork.WeaponCartRepository.Add(weaponCartItem);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Item Added to Cart";
                    response.Id = weaponCartItem.Id;
                }
                else
                {
                    weaponCartItem.Count++;
                    weaponCartItem.Price = request.WeaponCartItem.Price * weaponCartItem.Count;

                    await _unitOfWork.WeaponCartRepository.Update(weaponCartItem);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Cart Item Quantity Updated";
                    response.Id = weaponCartItem.Id;
                }
            }

            return response;
        }
    }
}
