using CIR_skladisce.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Narocilo
    {
        public int Id { get; set; }
        public StanjeNarocila Stanje { get; set; }
        public Prioriteta Prioriteta { get; set; }
        public Izdelek Izdelek { get; set; }
        public Skladisce Skladisce { get; set; }
        [DisplayName("Količina")]
        public int Kolicina { get; set; }
        [DisplayName("Datum naročila")]
        public DateTime DatumNarocila { get; set; }
        public string Opombe { get; set; }

        public Narocilo()
        {

        }

    }
}