using AutoMapper;
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
using UnicornInsurance.Application.Contracts.Identity;
using UnicornInsurance.Application.DTOs.Order;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Orders.Requests.Queries;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Queries
{
    public class GetOrderHeaderHandler : IRequestHandler<GetOrderHeaderRequest, OrderHeaderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetOrderHeaderHandler(IUnitOfWork unitOfWork,
                                     IMapper mapper,
                                     IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrderHeaderDTO> Handle(GetOrderHeaderRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var orderHeader = await _unitOfWork.OrderHeaderRepository.Get(request.OrderId);

            if (orderHeader is null)
                throw new NotFoundException("Order", request.OrderId);
            if (orderHeader.ApplicationUserId != userId)
                throw new UnauthorizedAccessException();

            var orderHeaderDTO = _mapper.Map<OrderHeaderDTO>(orderHeader);

            return orderHeaderDTO;
        }
    }
}
