using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class MobileSuitDamagedException : ApplicationException
    {
        public MobileSuitDamagedException() : base($"Your Mobile Suit is damaged")
        {

        }
    }
}
