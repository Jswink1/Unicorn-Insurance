﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.Deployment;
using UnicornInsurance.Application.Features.Deployments.Requests.Commands;
using UnicornInsurance.Application.Features.Deployments.Requests.Queries;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeploymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeploymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeploymentDTO>>> Get()
        {
            var deployments = await _mediator.Send(new GetDeploymentListRequest());

            return Ok(deployments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeploymentDTO>> Get(int id)
        {
            var deployment = await _mediator.Send(new GetDeploymentDetailsRequest { DeploymentId = id });
            return Ok(deployment);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDeploymentDTO deployment)
        {
            var command = new CreateDeploymentCommand { CreateDeploymentDTO = deployment };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] DeploymentDTO deployment)
        {
            var command = new UpdateDeploymentCommand { DeploymentDTO = deployment };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteDeploymentCommand { DeploymentId = id };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [Route("DeployMobileSuit/{id}")]
        public async Task<ActionResult<DeploymentDTO>> DeployMobileSuit(int id)
        {
            var response = await _mediator.Send(new DeployMobileSuitCommand() { UserMobileSuitId = id });
            return Ok(response);
        }
    }
}
