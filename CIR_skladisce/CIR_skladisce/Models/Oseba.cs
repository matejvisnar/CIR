using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Oseba
    {
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Davcna { get; set; }
        public Naslov Naslov { get; set; }

        public Oseba()
        {

        }
    }
}