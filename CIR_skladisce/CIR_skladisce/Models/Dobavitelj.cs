using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Dobavitelj : Oseba
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public Dobavitelj()
        {

        }

    }
}