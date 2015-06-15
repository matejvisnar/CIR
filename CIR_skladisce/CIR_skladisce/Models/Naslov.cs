using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Naslov
    {
        public int Id { get; set; }
        public string Ulica { get; set; }
        public string Hisna_st { get; set; }
        public int Postna_st { get; set; }
        public string Mesto { get; set; }
        public string Drzava { get; set; }
    }
}