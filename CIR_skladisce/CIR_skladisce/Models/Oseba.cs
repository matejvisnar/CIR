using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Oseba
    {
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Priimek { get; set; }
        [DisplayName("Davčna številka")]
        [Required]
        public string Davcna { get; set; }
        [Required]
        public Naslov Naslov { get; set; }

        public Oseba()
        {

        }
    }
}