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
using UnicornInsurance.Application.Features.Orders.Requests.Queries;
using UnicornInsurance.Models;

namespace UnicornInsurance.Application.Features.Orders.Handlers.Queries
{
    public class GetOrdersListHandler : IRequestHandler<GetOrdersListRequest, List<OrderHeaderDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetOrdersListHandler(IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    IHttpContextAccessor httpContextAccessor,
                                    IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<List<OrderHeaderDTO>> Handle(GetOrdersListRequest request, CancellationToken cancellationToken)
        {
            var orders = new List<OrderHeader>();

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;
            var userRoles = await _userService.GetUserRoles(userId);

            if (userRoles.Contains(SD.CustomerRole))
            {
                orders = await _unitOfWork.OrderHeaderRepository.GetUserOrders(userId);                

                if (orders.Count > 0)
                {
                    var ordersListDTO = _mapper.Map<List<OrderHeaderDTO>>(orders);

                    foreach (var orderDTO in ordersListDTO)
                    {
                        orderDTO.UserEmail = await _userService.GetUserEmail(userId);
                    }

                    return ordersListDTO;
                }
            }
            else if (userRoles.Contains(SD.AdminRole))
            {
                orders = await _unitOfWork.OrderHeaderRepository.GetAll();

                if (orders.Count > 0)
                {
                    var ordersListDTO = _mapper.Map<List<OrderHeaderDTO>>(orders);

                    foreach (var orderDTO in ordersListDTO)
                    {
                        orderDTO.UserEmail = await _userService.GetUserEmail(orders.Where(o => o.Id == orderDTO.Id).FirstOrDefault().ApplicationUserId);
                    }

                    return ordersListDTO;
                }
            }

            return new List<OrderHeaderDTO>();
        }
    }
}
