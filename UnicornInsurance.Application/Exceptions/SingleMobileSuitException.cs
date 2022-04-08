using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Exceptions
{
    public class SingleMobileSuitException : ApplicationException
    {
        public SingleMobileSuitException() : base($"User can only own one each type of mobile suit")
        {

        }
    }

}
