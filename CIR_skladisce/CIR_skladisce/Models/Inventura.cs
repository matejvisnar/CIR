using CIR_skladisce.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Inventura
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumInventure { get; set; }
        public TipInventure TipInventure { get; set; }
        public List<Izdelek> Izdelki { get; set; }
        public Skladisce Skladisce { get; set; }

        public Inventura()
        {

        }

    }
}