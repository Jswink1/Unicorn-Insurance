using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models.ViewModels
{
    public class MobileSuitListVM
    {
        public List<MobileSuit> MobileSuits { get; set; }
        public Pagination Pagination { get; set; }        
    }
}
