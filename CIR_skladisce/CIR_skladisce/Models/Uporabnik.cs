using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Uporabnik : Oseba
    {
        public int Id { get; set; }
        public string Uporabniski_ime { get; set; }
        public string Geslo { get; set; }

        public Uporabnik()
        {

        }

    }
}