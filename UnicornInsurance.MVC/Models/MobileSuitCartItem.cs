using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class MobileSuitCartItem
    {
        public int Id { get; set; }
        public int MobileSuitId { get; set; }
        public MobileSuitVM MobileSuit { get; set; }
        public decimal Price { get; set; }
    }
}
