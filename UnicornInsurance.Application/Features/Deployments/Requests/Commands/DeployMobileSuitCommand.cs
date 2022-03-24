using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Deployment;

namespace UnicornInsurance.Application.Features.Deployments.Requests.Commands
{
    public class DeployMobileSuitCommand : IRequest<DeploymentDTO>
    {
        public int UserMobileSuitId { get; set; }
    }
}
