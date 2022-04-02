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
using UnicornInsurance.Application.DTOs.Order.Validators;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Orders.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Commands
{
    public class CompleteOrderHandler : IRequestHandler<CompleteOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompleteOrderHandler(IUnitOfWork unitOfWork,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new OrderHeaderCompletionDTOValidator().ValidateAsync(request.CompleteOrderDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Order Completion Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {                
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

                var orderHeader = await _unitOfWork.OrderHeaderRepository.Get(request.CompleteOrderDTO.OrderId);

                if (orderHeader == null)
                    throw new NotFoundException(nameof(orderHeader), request.CompleteOrderDTO.OrderId);
                if (orderHeader.PaymentStatus == SD.PaymentStatusApproved)
                    throw new PaymentAlreadyApprovedException();
                if (orderHeader.ApplicationUserId != userId)
                    throw new UnauthorizedAccessException();

                if (request.CompleteOrderDTO.TransactionSuccess)
                {
                    orderHeader.PaymentStatus = SD.StatusApproved;
                    orderHeader.PaymentDate = DateTime.Now;
                    orderHeader.TransactionId = request.CompleteOrderDTO.TransactionId;

                    var mobileSuitPurchases = await _unitOfWork.MobileSuitPurchaseRepository.GetMobileSuitPurchasesForOrder(orderHeader.Id);
                    var weaponPurchases = await _unitOfWork.WeaponPurchaseRepository.GetWeaponPurchasesForOrder(orderHeader.Id);

                    if (mobileSuitPurchases is not null &&
                        mobileSuitPurchases.Count > 0)
                    {
                        foreach (var mobileSuit in mobileSuitPurchases)
                        {
                            await _unitOfWork.UserMobileSuitRepository.CreateUserMobileSuit(userId, mobileSuit);
                        }
                    }

                    if (weaponPurchases is not null &&
                        weaponPurchases.Count > 0)
                    {
                        foreach (var weapon in weaponPurchases)
                        {
                            await _unitOfWork.UserWeaponRepository.CreateUserWeapon(userId, weapon);
                        } 
                    }
                }
                else
                {
                    orderHeader.PaymentStatus = SD.PaymentStatusRejected;
                }

                await _unitOfWork.OrderHeaderRepository.Update(orderHeader);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Order Completed";
            }

            return response;
        }
    }
}
