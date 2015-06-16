using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CIR_skladisce.Models
{
    public class Izdelek
    {
        public int Id { get; set; }
        [DisplayName("Se prodaja")]
        public bool SeLahkoProdaja { get; set; }
        [DisplayName("Tip izdelka")]
        public string TipIzdelka { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [DisplayName("Prodajna cena")]
        public decimal ProdajnaCena { get; set; }
        [DisplayName("Odkupna cena")]
        public decimal OdkupnaCena { get; set; }
        [DisplayName("Št. mesecev garancije")]
        public int GarancijaMes { get; set; }
        public List<Narocilo> Narocila { get; set; }
        public List<Dobavitelj> Dobavitelji { get; set; }
        public ZalogaIzdelka Zaloga { get; set; }

        public Izdelek()
        {

        }


    }
}