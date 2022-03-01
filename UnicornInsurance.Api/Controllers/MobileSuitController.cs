using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Commands;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Queries;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileSuitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MobileSuitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<MobileSuitDTO>>> Get()
        {
            var mobileSuits = await _mediator.Send(new GetMobileSuitListRequest());
            return Ok(mobileSuits);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FullMobileSuitDTO>> Get(int id)
        {
            var mobileSuit = await _mediator.Send(new GetMobileSuitDetailsRequest { Id = id });
            return Ok(mobileSuit);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFullMobileSuitDTO mobileSuit)
        {
            var command = new CreateMobileSuitCommand { CreateMobileSuitDTO = mobileSuit };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] FullMobileSuitDTO mobileSuit)
        {
            var command = new UpdateMobileSuitCommand { MobileSuitDTO = mobileSuit };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteMobileSuitCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
