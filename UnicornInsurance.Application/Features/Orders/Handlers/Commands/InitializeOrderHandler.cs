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
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Commands
{
    public class InitializeOrderHandler : IRequestHandler<InitializeOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InitializeOrderHandler(IUnitOfWork unitOfWork,
                                      IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(InitializeOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new InitializeOrderDTOValidator().ValidateAsync(request.InitializeOrderDTO);

            if (validationResult.IsValid == false ||
                request.InitializeOrderDTO.MobileSuitPurchases.Count == 0 &&
                request.InitializeOrderDTO.WeaponPurchases.Count == 0)
            {
                response.Success = false;
                response.Message = "Order Initialization Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

                // Check if user is trying to purchase a mobile suit that they already own
                foreach (var mobileSuitPurchase in request.InitializeOrderDTO.MobileSuitPurchases)
                {
                    var userMobileSuits = await _unitOfWork.UserMobileSuitRepository.GetAllUserMobileSuits(userId);
                    var alreadyOwnedMobileSuit = userMobileSuits.Where(m => m.MobileSuitId == mobileSuitPurchase.MobileSuitId).FirstOrDefault();

                    if (alreadyOwnedMobileSuit is not null)
                        throw new SingleMobileSuitException();
                }

                decimal total = 0;

                OrderHeader orderheader = new();
                orderheader.PaymentStatus = SD.PaymentStatusPending;
                orderheader.OrderDate = DateTime.Now;
                orderheader.ApplicationUserId = userId;

                orderheader = await _unitOfWork.OrderHeaderRepository.Add(orderheader);
                await _unitOfWork.Save();

                try
                {
                    if (request.InitializeOrderDTO.MobileSuitPurchases.Count > 0)
                    {
                        foreach (var mobileSuitPurchaseDTO in request.InitializeOrderDTO.MobileSuitPurchases)
                        {
                            var mobileSuit = await _unitOfWork.MobileSuitRepository.Get(mobileSuitPurchaseDTO.MobileSuitId);

                            if (mobileSuit is null)
                                throw new NotFoundException("Mobile Suit", mobileSuitPurchaseDTO.MobileSuitId);

                            MobileSuitPurchase mobileSuitPurchase = new()
                            {
                                MobileSuitId = mobileSuitPurchaseDTO.MobileSuitId,
                                OrderId = orderheader.Id,
                                Price = mobileSuit.Price
                            };

                            total += mobileSuit.Price;

                            await _unitOfWork.MobileSuitPurchaseRepository.Add(mobileSuitPurchase);
                        }
                    }
                    if (request.InitializeOrderDTO.WeaponPurchases.Count > 0)
                    {
                        foreach (var weaponPurchaseDTO in request.InitializeOrderDTO.WeaponPurchases)
                        {
                            var weapon = await _unitOfWork.WeaponRepository.Get(weaponPurchaseDTO.WeaponId);

                            if (weapon is null)
                                throw new NotFoundException("Weapon", weaponPurchaseDTO.WeaponId);
                            if (weapon.IsCustomWeapon)
                                throw new PurchaseCustomWeaponException();

                            WeaponPurchase weaponPurchase = new()
                            {
                                WeaponId = weaponPurchaseDTO.WeaponId,
                                OrderId = orderheader.Id,
                                Count = weaponPurchaseDTO.Count,
                                Price = weapon.Price * weaponPurchaseDTO.Count
                            };

                            total += weapon.Price * weaponPurchaseDTO.Count;

                            await _unitOfWork.WeaponPurchaseRepository.Add(weaponPurchase);
                        }
                    }
                }
                catch (Exception)
                {
                    await _unitOfWork.OrderHeaderRepository.Delete(orderheader);
                    await _unitOfWork.Save();
                    throw;
                }

                orderheader.OrderTotal = total;

                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Order Initialized";
                response.Id = orderheader.Id;
            }

            return response;
        }
    }
}
