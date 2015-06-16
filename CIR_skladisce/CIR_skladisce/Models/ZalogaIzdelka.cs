using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class ZalogaIzdelka
    {
        [DisplayName("Minimalna zaloga")]
        public int MinZaloga { get; set; }
        [DisplayName("Dejanska zaloga")]
        public int DejanskaZaloga { get; set; }
        [DisplayName("Naročena zaloga")]
        public int NarocenaZaloga { get; set; }
        [DisplayName("Datum spremebe")]
        public DateTime DatumSpremembe { get; set; }
    }
}