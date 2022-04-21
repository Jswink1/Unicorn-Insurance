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
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Queries;

namespace UnicornInsurance.Application.Features.MobileSuits.Handlers.Queries
{
    public class GetMobileSuitDetailsHandler : IRequestHandler<GetMobileSuitDetailsRequest, FullMobileSuitDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMobileSuitDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FullMobileSuitDTO> Handle(GetMobileSuitDetailsRequest request, CancellationToken cancellationToken)
        {
            var mobileSuit = await _unitOfWork.MobileSuitRepository.GetFullMobileSuitDetails(request.MobileSuitId);

            if (mobileSuit is null)
                throw new NotFoundException("Mobile Suit", request.MobileSuitId);

            return _mapper.Map<FullMobileSuitDTO>(mobileSuit);
        }
    }
}
