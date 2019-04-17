using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace openbankapi.Models
{
    public class ProcessTransaction
    {
        [Required]
        public string To { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
