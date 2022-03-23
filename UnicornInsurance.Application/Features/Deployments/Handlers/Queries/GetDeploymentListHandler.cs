using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.Deployment;
using UnicornInsurance.Application.Features.Deployments.Requests.Queries;

namespace UnicornInsurance.Application.Features.Deployments.Handlers.Queries
{
    public class GetDeploymentListHandler : IRequestHandler<GetDeploymentListRequest, List<DeploymentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetDeploymentListHandler(IUnitOfWork unitOfWork,
                                        IMapper mapper,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DeploymentDTO>> Handle(GetDeploymentListRequest request, CancellationToken cancellationToken)
        {
            var deployments = await _unitOfWork.DeploymentRepository.GetAll();

            return _mapper.Map<List<DeploymentDTO>>(deployments);
        }
    }
}
