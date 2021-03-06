using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Models
{
    public class Deployment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string ResultType { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
