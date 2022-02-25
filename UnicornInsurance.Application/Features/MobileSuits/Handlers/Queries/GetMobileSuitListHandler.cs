using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Queries;

namespace UnicornInsurance.Application.Features.MobileSuits.Handlers.Queries
{
    public class GetMobileSuitListHandler : IRequestHandler<GetMobileSuitListRequest, List<MobileSuitDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMobileSuitListHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MobileSuitDTO>> Handle(GetMobileSuitListRequest request, CancellationToken cancellationToken)
        {
            var mobileSuits = await _unitOfWork.MobileSuitRepository.GetAll();
            return _mapper.Map<List<MobileSuitDTO>>(mobileSuits);
        }
    }
}
