using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Features.UserItems.Requests.Commands
{
    public class DeleteUserMobileSuitCommand : IRequest
    {
        public int UserMobileSuitId { get; set; }
    }
}
