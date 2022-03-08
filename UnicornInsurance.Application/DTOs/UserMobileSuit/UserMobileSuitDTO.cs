using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;

namespace UnicornInsurance.Application.DTOs.UserMobileSuit
{
    public class UserMobileSuitDTO
    {
        public int Id { get; set; }
        public MobileSuitDTO MobileSuit { get; set; }
    }
}
