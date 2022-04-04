using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.Features.Weapons.Requests.Commands;
using UnicornInsurance.Application.Features.Weapons.Requests.Queries;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeaponController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<WeaponDTO>>> Get()
        {
            var weapons = await _mediator.Send(new GetWeaponListRequest());
            return Ok(weapons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeaponDTO>> Get(int id)
        {
            var weapon = await _mediator.Send(new GetWeaponDetailsRequest { WeaponId = id });
            return Ok(weapon);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateWeaponDTO weapon)
        {
            var command = new CreateWeaponCommand { CreateWeaponDTO = weapon };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] WeaponDTO weapon)
        {
            var command = new UpdateWeaponCommand { WeaponDTO = weapon };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = SD.AdminRole)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteWeaponCommand { WeaponId = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
