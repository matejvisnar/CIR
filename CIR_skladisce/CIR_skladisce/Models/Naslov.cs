using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Naslov
    {
        public int Id { get; set; }
        [Required]
        public string Ulica { get; set; }
        [Required]
        [DisplayName("Hišna številka")]
        public string HisnaSt { get; set; }
        [Required]
        [DisplayName("Poštna številka")]
        public int PostnaSt { get; set; }
        [Required]
        public string Mesto { get; set; }
        [Required]
        [DisplayName("Država")]
        public string Drzava { get; set; }

        public Naslov()
        {

        }

    }
}