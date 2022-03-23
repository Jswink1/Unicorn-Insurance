using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class Deployment
    {
        public int Id { get; set; }
        public string ResultType { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
