using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Features.MobileSuits.Requests.Commands
{
    public class DeleteMobileSuitCommand : IRequest
    {
        public int MobileSuitId { get; set; }
    }
}
