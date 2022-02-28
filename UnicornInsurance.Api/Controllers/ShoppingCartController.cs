using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuitCartItem;
using UnicornInsurance.Application.DTOs.WeaponCartItem;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands;
using UnicornInsurance.Application.Features.ShoppingCart.Requests.Queries;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/<WeaponCart>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("WeaponCart")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateWeaponCartItemDTO weaponCartItem)
        {
            var command = new CreateWeaponCartCommand { WeaponCartItem = weaponCartItem };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // POST api/<MobileSuitCart>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("MobileSuitCart")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateMobileSuitCartItemDTO mobileSuitCartItem)
        {
            var command = new CreateMobileSuitCartCommand { MobileSuitCartItem = mobileSuitCartItem };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // GET: api/<WeaponCart>
        [HttpGet]
        [Route("WeaponCart")]
        public async Task<ActionResult<List<WeaponCartItemDTO>>> GetWeaponCartItems()
        {
            var weaponCartItems = await _mediator.Send(new GetWeaponCartListRequest());
            return Ok(weaponCartItems);
        }

        // GET: api/<MobileSuitCart>
        [HttpGet]
        [Route("MobileSuitCart")]
        public async Task<ActionResult<List<MobileSuitCartItemDTO>>> GetMobileSuitCartItems()
        {
            var mobileSuitCartItems = await _mediator.Send(new GetMobileSuitCartListRequest());
            return Ok(mobileSuitCartItems);
        }

        // GET: api/<ShoppingCartItemCount>
        [HttpGet]
        [Route("ShoppingCartItemCount")]
        public async Task<ActionResult<ShoppingCartItemCountResponse>> GetShoppingCartItemCount()
        {
            var response = await _mediator.Send(new GetShoppingCartItemCountRequest());
            return Ok(response);
        }

        // PUT api/<IncreaseWeaponCartQuantity>
        [HttpPut("IncreaseWeaponCartQuantity/{id}")]
        public async Task<ActionResult> IncreaseWeaponCartQuantity(int id)
        {
            var command = new IncreaseWeaponCartQuantityCommand { ItemId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<DecreaseWeaponCartQuantity>
        [HttpPut("DecreaseWeaponCartQuantity/{id}")]
        public async Task<ActionResult> DecreaseWeaponCartQuantity(int id)
        {
            var command = new DecreaseWeaponCartQuantityCommand { ItemId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // DELETE api/<MobileSuitCart>/5        
        [HttpDelete("MobileSuitCart/{id}")]
        public async Task<ActionResult> DeleteMobileSuitCartItem(int id)
        {
            var command = new DeleteMobileSuitCartCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }

        // DELETE api/<WeaponCart>/5        
        [HttpDelete("WeaponCart/{id}")]
        public async Task<ActionResult> DeleteWeaponCartItem(int id)
        {
            var command = new DeleteWeaponCartCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }

        // GET: api/<ClearShoppingCart>
        [HttpDelete("ClearShoppingCart")]
        public async Task<ActionResult> ClearShoppingCart()
        {
            var response = await _mediator.Send(new ClearShoppingCartCommand());
            return Ok(response);
        }
    }
}
