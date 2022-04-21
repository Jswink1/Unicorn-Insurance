using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.DTOs.Deployment.Validators;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Deployments.Requests.Commands;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Deployments.Handlers.Commands
{
    public class UpdateDeploymentHandler : IRequestHandler<UpdateDeploymentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDeploymentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDeploymentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateDeploymentDTOValidator();
            var validationResult = await validator.ValidateAsync(request.DeploymentDTO);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Deployment Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var deployment = await _unitOfWork.DeploymentRepository.Get(request.DeploymentDTO.Id);

                if (deployment is null)
                    throw new NotFoundException("Deployment", request.DeploymentDTO.Id);

                _mapper.Map(request.DeploymentDTO, deployment);

                await _unitOfWork.DeploymentRepository.Update(deployment);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Deployment Update Successful";
                response.Id = deployment.Id;
            }

            return response;
        }
    }
}
