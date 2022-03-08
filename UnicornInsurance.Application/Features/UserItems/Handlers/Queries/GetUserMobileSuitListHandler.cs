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
using UnicornInsurance.Application.DTOs.UserMobileSuit;
using UnicornInsurance.Application.Features.UserItems.Requests.Queries;

namespace UnicornInsurance.Application.Features.UserItems.Handlers.Queries
{
    public class GetUserMobileSuitListHandler : IRequestHandler<GetUserMobileSuitListRequest, List<UserMobileSuitDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserMobileSuitListHandler(IUnitOfWork unitOfWork,
                                            IMapper mapper,
                                            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<UserMobileSuitDTO>> Handle(GetUserMobileSuitListRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == SD.Uid)?.Value;

            var userMobileSuits = await _unitOfWork.UserMobileSuitRepository.GetAllUserMobileSuits(userId);

            return _mapper.Map<List<UserMobileSuitDTO>>(userMobileSuits);
        }
    }
}
