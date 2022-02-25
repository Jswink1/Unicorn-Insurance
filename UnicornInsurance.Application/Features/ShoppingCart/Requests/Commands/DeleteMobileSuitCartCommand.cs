using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Features.ShoppingCart.Requests.Commands
{
    public class DeleteMobileSuitCartCommand : IRequest
    {
        public int Id { get; set; }
    }
}
