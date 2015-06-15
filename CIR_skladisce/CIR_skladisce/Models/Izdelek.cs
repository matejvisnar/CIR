using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Izdelek
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal ProdajnaCena { get; set; }
        public decimal OdkupnaCena { get; set; }
        public int GarancijaMes { get; set; }

        public Izdelek()
        {

        }


    }
}