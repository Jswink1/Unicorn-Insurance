using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models.ViewModels
{
    public class MobileSuitUpsertVM
    {
        public MobileSuit MobileSuit { get; set; } = new();
        public ICollection<string> Errors { get; set; }
    }
}
