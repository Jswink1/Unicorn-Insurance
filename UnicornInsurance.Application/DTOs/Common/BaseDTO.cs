using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.DTOs.Common
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        //public DateTime DateCreated { get; set; }
        //public DateTime LastModifiedDate { get; set; }
    }
}
