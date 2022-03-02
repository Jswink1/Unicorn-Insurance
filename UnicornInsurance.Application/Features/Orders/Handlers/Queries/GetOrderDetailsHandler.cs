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
using UnicornInsurance.Application.DTOs.OrderDetails;
using UnicornInsurance.Application.DTOs.OrderHeader;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Orders.Requests.Queries;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Queries
{
    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsRequest, OrderDetailsDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetOrderDetailsHandler(IUnitOfWork unitOfWork,
                                      IMapper mapper,
                                      IHttpContextAccessor httpContextAccessor,
                                      IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<OrderDetailsDTO> Handle(GetOrderDetailsRequest request, CancellationToken cancellationToken)
        {
            var orderDetails = new OrderDetailsDTO();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;
            var userRoles = await _userService.GetUserRoles(userId);

            var orderHeader = await _unitOfWork.OrderHeaderRepository.Get(request.OrderId);

            if (orderHeader is null)
                throw new NotFoundException(nameof(orderHeader), request.OrderId);
            if (orderHeader.ApplicationUserId != userId &&
                userRoles.Contains(SD.AdminRole) == false)
                throw new UnauthorizedAccessException();

            orderDetails.OrderHeader = _mapper.Map<OrderHeaderDTO>(orderHeader);
            orderDetails.OrderHeader.UserEmail = await _userService.GetUserEmail(userId);

            var mobileSuitPurchases = await _unitOfWork.MobileSuitPurchaseRepository.GetMobileSuitPurchasesForOrder(request.OrderId);
            var weaponPurchases = await _unitOfWork.WeaponPurchaseRepository.GetWeaponPurchasesForOrder(request.OrderId);

            if (mobileSuitPurchases.Count > 0)
                orderDetails.MobileSuitPurchases = _mapper.Map<List<MobileSuitPurchaseDTO>>(mobileSuitPurchases);

            if (weaponPurchases.Count > 0)
                orderDetails.WeaponPurchases = _mapper.Map<List<WeaponPurchaseDTO>>(weaponPurchases);

            return orderDetails;
        }
    }
}
