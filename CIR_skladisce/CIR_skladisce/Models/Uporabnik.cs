using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Uporabnik : Oseba
    {
        public int Id { get; set; }
        
        [DisplayName("Uporabniško ime")]
        public string UporabniskoIme { get; set; }

        public Uporabnik()
        {

        }

    }

    public class UporabnikLogin
    {
        [Required]
        [DisplayName("Uporabniško ime")]
        public string UporabniskoIme { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Geslo { get; set; }
    }

    public class UporabnikRegistartion
    {
        public string Ime { get; set; }

        public string Priimek { get; set; }
        
        [Required]
        [DisplayName("Uporabniško ime")]
        public string UporabniskoIme { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Geslo { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Ponovi geslo")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PonovnoGeslo { get; set; }
    }
}