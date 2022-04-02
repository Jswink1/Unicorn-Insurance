using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.UserMobileSuit;
using UnicornInsurance.Application.DTOs.UserWeapon;
using UnicornInsurance.Application.Features.UserItems.Requests.Commands;
using UnicornInsurance.Application.Features.UserItems.Requests.Queries;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("UserMobileSuit")]
        public async Task<ActionResult<List<UserMobileSuitDTO>>> GetUserMobileSuits()
        {
            var userMobileSuits = await _mediator.Send(new GetUserMobileSuitListRequest());
            return Ok(userMobileSuits);
        }

        [HttpGet]
        [Route("UserMobileSuit/{id}")]
        public async Task<ActionResult<FullUserMobileSuitDTO>> GetUserMobileSuitDetails(int id)
        {
            var userMobileSuit = await _mediator.Send(new GetUserMobileSuitDetailsRequest() { UserMobileSuitId = id });
            return Ok(userMobileSuit);
        }

        [HttpPut]
        [Route("EquipWeapon")]
        public async Task<ActionResult<BaseCommandResponse>> EquipWeapon([FromBody] EquipWeaponDTO equipWeaponDTO)
        {
            var response = await _mediator.Send(new EquipWeaponCommand() { EquipWeaponDTO = equipWeaponDTO });
            return Ok(response);
        }

        [HttpPut]
        [Route("UnequipWeapon")]
        public async Task<ActionResult> UnequipWeapon(int userMobileSuitId)
        {
            await _mediator.Send(new UnequipWeaponCommand() { UserMobileSuitId = userMobileSuitId });
            return Ok();
        }

        [HttpPut]
        [Route("UserInsurancePlan")]
        public async Task<ActionResult<BaseCommandResponse>> UserInsurancePlan([FromBody] UserInsurancePlanDTO userInsurancePlanDTO)
        {
            var response = await _mediator.Send(new UpdateUserInsurancePlanCommand() { UserInsurancePlanDTO = userInsurancePlanDTO });
            return Ok(response);
        }

        [HttpDelete]
        [Route("UserMobileSuit/{id}")]
        public async Task<ActionResult> DeleteUserMobileSuit(int id)
        {
            await _mediator.Send(new DeleteUserMobileSuitCommand() { UserMobileSuitId = id });
            return Ok();
        }
    }
}
