using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;

namespace UnicornInsurance.Application.Features.MobileSuits.Requests.Queries
{
    public class GetMobileSuitDetailsRequest : IRequest<FullMobileSuitDTO>
    {
        public int Id { get; set; }
    }
}
