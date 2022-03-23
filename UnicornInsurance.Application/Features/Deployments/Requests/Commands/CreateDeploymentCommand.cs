using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Deployment;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Application.Features.Deployments.Requests.Commands
{
    public class CreateDeploymentCommand : IRequest<BaseCommandResponse>
    {
        public CreateDeploymentDTO CreateDeploymentDTO { get; set; }
    }
}
