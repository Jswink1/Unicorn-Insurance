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
using UnicornInsurance.Application.DTOs.OrderDetails.Validators;
using UnicornInsurance.Application.Features.Orders.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Commands
{
    public class CreateOrderDetailsHandler : IRequestHandler<CreateOrderDetailsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateOrderDetailsHandler(IUnitOfWork unitOfWork,
                                           IMapper mapper,
                                           IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(CreateOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new CreateOrderDetailsDTOValidator().ValidateAsync(request.OrderDetailsDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Order Details Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                // TODO: maybe retrieve the OrderHeader details based on request.OrderDetailsDTO.OrderHeaderId
                // and check that it exists and that the userIds are equal
                //var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                //    q => q.Type == SD.Uid)?.Value;

                if (request.OrderDetailsDTO.MobileSuitPurchases.Count > 0)
                {
                    foreach (var mobileSuitPurchaseDTO in request.OrderDetailsDTO.MobileSuitPurchases)
                    {
                        var mobileSuitPurchase = _mapper.Map<MobileSuitPurchase>(mobileSuitPurchaseDTO);
                        mobileSuitPurchase.OrderId = request.OrderDetailsDTO.OrderHeaderId;

                        await _unitOfWork.MobileSuitPurchaseRepository.Add(mobileSuitPurchase);
                    }
                }
                if (request.OrderDetailsDTO.WeaponPurchases.Count > 0)
                {
                    foreach (var weaponPurchaseDTO in request.OrderDetailsDTO.WeaponPurchases)
                    {
                        var weaponPurchase = _mapper.Map<WeaponPurchase>(weaponPurchaseDTO);
                        weaponPurchase.OrderId = request.OrderDetailsDTO.OrderHeaderId;

                        await _unitOfWork.WeaponPurchaseRepository.Add(weaponPurchase);
                    }
                }

                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Order Details Created";
            }

            return response;
        }
    }
}
