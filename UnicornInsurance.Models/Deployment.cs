using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Models.Common;

namespace UnicornInsurance.Models
{
    public class Deployment : BaseModel
    {
        [Required]
        [MaxLength(10)]
        public string ResultType { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(2000)]
        public string ImageUrl { get; set; }
    }
}
