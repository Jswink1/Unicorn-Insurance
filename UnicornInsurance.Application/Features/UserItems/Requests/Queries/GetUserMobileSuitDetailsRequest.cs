using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.UserMobileSuit;

namespace UnicornInsurance.Application.Features.UserItems.Requests.Queries
{
    public class GetUserMobileSuitDetailsRequest : IRequest<FullUserMobileSuitDTO>
    {
        public int Id { get; set; }
    }
}
