using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Features.Deployments.Requests.Commands
{
    public class DeleteDeploymentCommand : IRequest
    {
        public int DeploymentId { get; set; }
    }
}
