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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Commands
{
    public class AddWeaponCartItemHandler : IRequestHandler<AddWeaponCartItemCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddWeaponCartItemHandler(IUnitOfWork unitOfWork,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(AddWeaponCartItemCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var weapon = await _unitOfWork.WeaponRepository.Get(request.WeaponId);

            if (weapon is null)
                throw new NotFoundException("Weapon", request.WeaponId);
            if (weapon.IsCustomWeapon)
                throw new PurchaseCustomWeaponException();

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var weaponCartItem = await _unitOfWork.WeaponCartRepository.GetCartItem(userId, request.WeaponId);

            if (weaponCartItem is null)
            {
                weaponCartItem = new WeaponCartItem()
                {
                    ApplicationUserId = userId,
                    WeaponId = request.WeaponId,
                    Count = 1,
                    Price = weapon.Price
                };

                weaponCartItem = await _unitOfWork.WeaponCartRepository.Add(weaponCartItem);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Item Added to Cart";
                response.Id = weaponCartItem.Id;
            }
            else
            {
                weaponCartItem.Count++;
                weaponCartItem.Price = weapon.Price * weaponCartItem.Count;

                await _unitOfWork.WeaponCartRepository.Update(weaponCartItem);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Cart Item Quantity Updated";
                response.Id = weaponCartItem.Id;
            }

            return response;
        }
    }
}
