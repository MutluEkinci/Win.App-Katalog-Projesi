using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._29.Carsamba.WF.Model
{
    public class AvMalzeme
    {
        public int AvMalzemeID { get; set; }
        public string KiyafetTipi { get; set; }
        public string KiyafetRengi { get; set; }
        public string SapkaTipi { get; set; }
        public string SapkaRengi { get; set; }
        public string EkipmanKemerTipi { get; set; }
        public string EkipmanKemerRengi { get; set; }
        public string BotTipi { get; set; }
        public string BotRengi { get; set; }
        public string DurbunBoyutu { get; set; }
        public string CantaBoyutu { get; set; }
        public string CantaRengi { get; set; }
        public decimal MalzemeFiyat { get; set; }
    }
}
