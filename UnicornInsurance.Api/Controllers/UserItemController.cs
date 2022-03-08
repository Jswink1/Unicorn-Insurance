using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.UserMobileSuit;
using UnicornInsurance.Application.Features.UserItems.Requests.Queries;

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
    }
}
