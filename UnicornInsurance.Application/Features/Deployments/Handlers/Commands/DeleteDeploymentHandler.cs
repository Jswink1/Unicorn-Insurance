using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Deployments.Requests.Commands;

namespace UnicornInsurance.Application.Features.Deployments.Handlers.Commands
{
    public class DeleteDeploymentHandler : IRequestHandler<DeleteDeploymentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeploymentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteDeploymentCommand request, CancellationToken cancellationToken)
        {
            var deployment = await _unitOfWork.DeploymentRepository.Get(request.DeploymentId);

            if (deployment is null)
                throw new NotFoundException(nameof(deployment), request.DeploymentId);

            await _unitOfWork.DeploymentRepository.Delete(deployment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
