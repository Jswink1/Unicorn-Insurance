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
    public class AddMobileSuitCartItemHandler : IRequestHandler<AddMobileSuitCartItemCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddMobileSuitCartItemHandler(IUnitOfWork unitOfWork,
                                            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(AddMobileSuitCartItemCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var mobileSuit = await _unitOfWork.MobileSuitRepository.GetFullMobileSuitDetails(request.MobileSuitId);

            if (mobileSuit is null)
                throw new NotFoundException("Mobile Suit", request.MobileSuitId);

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var userMobileSuits = await _unitOfWork.UserMobileSuitRepository.GetAllUserMobileSuits(userId);
            var alreadyOwnedMobileSuit = userMobileSuits.Where(m => m.MobileSuitId == mobileSuit.Id).FirstOrDefault();

            if (alreadyOwnedMobileSuit is not null)
                throw new SingleMobileSuitException();                      

            var cartItemExists = await _unitOfWork.MobileSuitCartRepository.CartItemExists(userId, request.MobileSuitId);

            if (cartItemExists)
            {
                response.Success = false;
                response.Message = "User can only have one of each type of mobile suit";
            }
            else
            {
                var mobileSuitCartItem = new MobileSuitCartItem()
                {
                    ApplicationUserId = userId,
                    MobileSuitId = request.MobileSuitId,
                    Price = mobileSuit.Price
                };

                mobileSuitCartItem = await _unitOfWork.MobileSuitCartRepository.Add(mobileSuitCartItem);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Item Added to Cart";
                response.Id = mobileSuitCartItem.Id;
            }

            return response;
        }
    }
}
