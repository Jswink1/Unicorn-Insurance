using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Deployment;

namespace UnicornInsurance.Application.Features.Deployments.Requests.Queries
{
    public class GetDeploymentListRequest : IRequest<List<DeploymentDTO>>
    {
    }
}
