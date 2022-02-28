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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Orders.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Commands
{
    public class CompleteOrderHeaderHandler : IRequestHandler<CompleteOrderHeaderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompleteOrderHeaderHandler(IUnitOfWork unitOfWork,
                                          IMapper mapper,
                                          IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(CompleteOrderHeaderCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            ValidationResult validationResult = await new OrderHeaderCompletionDTOValidator().ValidateAsync(request.OrderHeaderCompletionDTO);

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

                var orderHeader = await _unitOfWork.OrderHeaderRepository.Get(request.OrderHeaderCompletionDTO.OrderId);

                if (orderHeader == null)
                    throw new NotFoundException(nameof(orderHeader), request.OrderHeaderCompletionDTO.OrderId);

                if (request.OrderHeaderCompletionDTO.Success)
                {
                    orderHeader.OrderStatus = SD.PaymentStatusApproved;
                    orderHeader.PaymentStatus = SD.StatusApproved;
                    orderHeader.PaymentDate = DateTime.Now;
                    orderHeader.TransactionId = request.OrderHeaderCompletionDTO.TransactionId;
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
