using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Naslov
    {
        public int Id { get; set; }
        public string Ulica { get; set; }
        [DisplayName("Hišna številka")]
        public string HisnaSt { get; set; }
        [DisplayName("Poštna številka")]
        public int PostnaSt { get; set; }
        public string Mesto { get; set; }
        [DisplayName("Država")]
        public string Drzava { get; set; }

        public Naslov()
        {

        }

    }
}