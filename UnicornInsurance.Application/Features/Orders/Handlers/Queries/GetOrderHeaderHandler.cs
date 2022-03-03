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
using UnicornInsurance.Application.DTOs.OrderHeader;
using UnicornInsurance.Application.Features.Orders.Requests.Queries;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Queries
{
    public class GetOrderHeaderHandler : IRequestHandler<GetOrderHeaderRequest, OrderHeaderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetOrderHeaderHandler(IUnitOfWork unitOfWork,
                                     IMapper mapper,
                                     IHttpContextAccessor httpContextAccessor,
                                     IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<OrderHeaderDTO> Handle(GetOrderHeaderRequest request, CancellationToken cancellationToken)
        {
            var orderHeader = await _unitOfWork.OrderHeaderRepository.Get(request.OrderId);

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            if (orderHeader.ApplicationUserId != userId)
                throw new UnauthorizedAccessException();

            var orderHeaderDTO = _mapper.Map<OrderHeaderDTO>(orderHeader);

            return orderHeaderDTO;
        }
    }
}
