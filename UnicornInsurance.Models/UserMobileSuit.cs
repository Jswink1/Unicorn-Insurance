using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Models
{
    public class UserMobileSuit
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int MobileSuitId { get; set; }
        [ForeignKey("MobileSuitId")]
        public MobileSuit MobileSuit { get; set; }
        public string InsurancePlan { get; set; }
        public DateTime EndOfCoverage { get; set; }
        public bool IsDamaged { get; set; }

        //public int? CustomWeaponId { get; set; }
        //public virtual UserWeapon CustomWeapon { get; set; }
    }
}
