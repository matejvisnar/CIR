using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Oseba
    {
        public string Ime { get; set; }
        public string Priimek { get; set; }
        [DisplayName("Davčna številka")]
        public string Davcna { get; set; }
        public Naslov Naslov { get; set; }

        public Oseba()
        {

        }
    }
}