using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._29.Carsamba.WF.Model
{
    public class AvEkipman
    {
        public int AvEkipmanID { get; set; }
        public string AvTuru { get; set; }
        public int AvSilahID { get; set; }
        public decimal AvSilahFiyat { get; set; }
        public int AvMalzemeID { get; set; }
        public decimal AvMalzemeFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }


    }
}
