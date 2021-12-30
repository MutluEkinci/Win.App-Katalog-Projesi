using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._29.Carsamba.WF.DBManager
{
    class Avcilik
    {
        public AvEkipmanMN AvEkipmanlari { get; set; }
        public AvMalzemeMN AvMalzemeleri { get; set; }
        public AvSilahMN AvSilahlari { get; set; }

        public Avcilik()
        {
            AvEkipmanlari = new AvEkipmanMN();
            AvMalzemeleri = new AvMalzemeMN();
            AvSilahlari = new AvSilahMN();

        }
    }
}
