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
using UnicornInsurance.Application.DTOs.WeaponCartItem;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Queries;

namespace UnicornInsurance.Application.Features.ShoppingCart.Handlers.Queries
{
    public class GetWeaponCartListHandler : IRequestHandler<GetWeaponCartListRequest, List<WeaponCartItemDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetWeaponCartListHandler(IUnitOfWork unitOfWork,
                                        IMapper mapper,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<WeaponCartItemDTO>> Handle(GetWeaponCartListRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var weaponCartItems = await _unitOfWork.WeaponCartRepository.GetAllCartItems(userId);
            return _mapper.Map<List<WeaponCartItemDTO>>(weaponCartItems);
        }
    }
}
