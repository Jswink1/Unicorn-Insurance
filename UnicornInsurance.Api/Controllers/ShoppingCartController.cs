using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Route("AddWeaponToCart")]
        [Authorize]
        public async Task<ActionResult<BaseCommandResponse>> AddWeaponToCart([FromBody] int WeaponId)
        {
            var command = new AddWeaponCartItemCommand { WeaponId = WeaponId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("AddMobileSuitToCart")]
        [Authorize]
        public async Task<ActionResult<BaseCommandResponse>> AddMobileSuitToCart([FromBody] int MobileSuitId)
        {
            var command = new AddMobileSuitCartItemCommand { MobileSuitId = MobileSuitId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("WeaponCartItemsList")]
        [Authorize]
        public async Task<ActionResult<List<WeaponCartItemDTO>>> GetWeaponCartItems()
        {
            var weaponCartItems = await _mediator.Send(new GetWeaponCartItemListRequest());
            return Ok(weaponCartItems);
        }

        [HttpGet]
        [Route("MobileSuitCartItemsList")]
        [Authorize]
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

        [HttpPut("WeaponCartItemQuantityIncrease")]
        [Authorize]
        public async Task<ActionResult> IncreaseWeaponQuantity([FromBody] int WeaponId)
        {
            var command = new WeaponCartItemQuantityIncreaseCommand { WeaponId = WeaponId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("WeaponCartItemQuantityDecrease")]
        [Authorize]
        public async Task<ActionResult> DecreaseWeaponQuantity([FromBody] int WeaponId)
        {
            var command = new WeaponCartItemQuantityDecreaseCommand { WeaponId = WeaponId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
   
        [HttpDelete("DeleteMobileSuitCartItem/{CartItemId}")]
        [Authorize]
        public async Task<ActionResult> DeleteMobileSuitCartItem(int CartItemId)
        {
            var command = new DeleteMobileSuitCartItemCommand { MobileSuitCartItemId = CartItemId };
            await _mediator.Send(command);
            return Ok();
        }
   
        [HttpDelete("DeleteWeaponCartItem/{CartItemId}")]
        [Authorize]
        public async Task<ActionResult> DeleteWeaponCartItem(int CartItemId)
        {
            var command = new DeleteWeaponCartItemCommand { WeaponCartItemId = CartItemId };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("ClearShoppingCart")]
        [Authorize]
        public async Task<ActionResult> ClearShoppingCart()
        {
            var response = await _mediator.Send(new ClearShoppingCartCommand());
            return Ok(response);
        }
    }
}
