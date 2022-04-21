using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.UserWeapon;

namespace UnicornInsurance.Application.DTOs.UserMobileSuit
{
    public class UserMobileSuitDTO
    {
        public int Id { get; set; }
        public MobileSuitDTO MobileSuit { get; set; }
        public bool IsDamaged { get; set; }
        public DateTime EndOfCoverage { get; set; }
        public string InsurancePlan { get; set; }
    }
}
