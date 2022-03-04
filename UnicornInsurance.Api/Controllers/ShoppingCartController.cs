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

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("WeaponCartItem")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateWeaponCartItemDTO weaponCartItem)
        {
            var command = new CreateWeaponCartItemCommand { WeaponCartItem = weaponCartItem };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("MobileSuitCartItem")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateMobileSuitCartItemDTO mobileSuitCartItem)
        {
            var command = new CreateMobileSuitCartItemCommand { MobileSuitCartItem = mobileSuitCartItem };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("WeaponCartItem")]
        public async Task<ActionResult<List<WeaponCartItemDTO>>> GetWeaponCartItems()
        {
            var weaponCartItems = await _mediator.Send(new GetWeaponCartItemListRequest());
            return Ok(weaponCartItems);
        }

        [HttpGet]
        [Route("MobileSuitCartItem")]
        public async Task<ActionResult<List<MobileSuitCartItemDTO>>> GetMobileSuitCartItems()
        {
            var mobileSuitCartItems = await _mediator.Send(new GetMobileSuitCartItemListRequest());
            return Ok(mobileSuitCartItems);
        }

        [HttpGet]
        [Route("ShoppingCartItemCount")]
        public async Task<ActionResult<ShoppingCartItemCountResponse>> GetShoppingCartItemCount()
        {
            var response = await _mediator.Send(new GetShoppingCartItemCountRequest());
            return Ok(response);
        }

        [HttpPut("IncreaseWeaponQuantity/{id}")]
        public async Task<ActionResult> IncreaseWeaponQuantity(int id)
        {
            var command = new IncreaseWeaponCartQuantityCommand { ItemId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("DecreaseWeaponQuantity/{id}")]
        public async Task<ActionResult> DecreaseWeaponQuantity(int id)
        {
            var command = new DecreaseWeaponCartQuantityCommand { ItemId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
   
        [HttpDelete("MobileSuitCartItem/{id}")]
        public async Task<ActionResult> DeleteMobileSuitCartItem(int id)
        {
            var command = new DeleteMobileSuitCartItemCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }
   
        [HttpDelete("WeaponCartItem/{id}")]
        public async Task<ActionResult> DeleteWeaponCartItem(int id)
        {
            var command = new DeleteWeaponCartItemCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("ClearShoppingCart")]
        public async Task<ActionResult> ClearShoppingCart()
        {
            var response = await _mediator.Send(new ClearShoppingCartCommand());
            return Ok(response);
        }
    }
}
