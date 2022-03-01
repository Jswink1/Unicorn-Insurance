using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.OrderDetails;
using UnicornInsurance.Application.DTOs.OrderHeader;
using UnicornInsurance.Application.Features.Orders.Requests.Commands;
using UnicornInsurance.Application.Features.Orders.Requests.Queries;
using UnicornInsurance.Application.Responses;

namespace UnicornInsurance.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Order")]
        public async Task<ActionResult<List<OrderHeaderDTO>>> Get()
        {
            var orders = await _mediator.Send(new GetOrdersListRequest());
            return Ok(orders);
        }

        [HttpGet("Order/{id}")]
        public async Task<ActionResult<OrderHeaderDTO>> Get(int id)
        {
            
            return Ok(new OrderHeaderDTO());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("api/InitializeOrder")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] InitializeOrderHeaderDTO orderHeader)
        {
            var command = new InitializeOrderHeaderCommand { OrderHeaderDTO = orderHeader };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("api/CreateOrderDetails")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOrderDetailsDTO orderDetails)
        {
            var command = new CreateOrderDetailsCommand { OrderDetailsDTO = orderDetails };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("api/CompleteOrder")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CompleteOrderHeaderDTO orderHeaderCompletion)
        {
            var command = new CompleteOrderHeaderCommand { OrderHeaderCompletionDTO = orderHeaderCompletion };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
