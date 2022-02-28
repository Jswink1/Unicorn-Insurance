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
using UnicornInsurance.Application.DTOs.OrderHeader.Validators;
using UnicornInsurance.Application.Features.Orders.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Commands
{
    public class InItializeOrderHeaderHandler : IRequestHandler<InitializeOrderHeaderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InItializeOrderHeaderHandler(IUnitOfWork unitOfWork,
                                           IMapper mapper,
                                           IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(InitializeOrderHeaderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new InitializeOrderHeaderDTOValidator().ValidateAsync(request.OrderHeaderDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Order Initialization Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

                var orderheader = _mapper.Map<OrderHeader>(request.OrderHeaderDTO);
                orderheader.PaymentStatus = SD.PaymentStatusPending;
                orderheader.OrderStatus = SD.StatusPending;
                orderheader.OrderDate = DateTime.Now;
                orderheader.ApplicationUserId = userId;

                orderheader = await _unitOfWork.OrderHeaderRepository.Add(orderheader);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Order Initialized";
                response.Id = orderheader.Id;
            }

            return response;
        }
    }
}
