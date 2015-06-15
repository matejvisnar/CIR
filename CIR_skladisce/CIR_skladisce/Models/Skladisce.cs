using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Skladisce
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public Naslov Naslov { get; set; }
        public List<Narocilo> Narocila { get; set; }

        public Skladisce()
        {

        }

    }
}