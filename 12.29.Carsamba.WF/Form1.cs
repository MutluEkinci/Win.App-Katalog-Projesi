using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using _12._29.Carsamba.WF.Model;
using _12._29.Carsamba.WF.DBManager;
using System.Collections;
using System.IO;

namespace _12._29.Carsamba.WF
{
    //DESKTOP-TUMHS1A\MS_SQL_2019
    public partial class Form1 : Form
    {
        Avcilik Avcilik = new Avcilik();
        AvSilah avSilah = new AvSilah();
        AvMalzeme malzeme = new AvMalzeme();
        AvEkipman ekipman = new AvEkipman();
        List<string> list = new List<string>();
        
        DialogResult dr;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            


        }
        private void btnSilahAra_Click(object sender, EventArgs e)
        {
            AvSilah avsilah = Avcilik.AvSilahlari.SilahAra(int.Parse(txtSilahID.Text));
            if (avsilah.AvSilahID > 0)
            {
                txtSilahID.Text = avsilah.AvSilahID.ToString();
                cmbBoxSilahTipi.Text = avsilah.SilahTipi;
                txtSilahAdi.Text = avsilah.SilahAdi;
                cmbBoxSilahAtisModu.Text = avsilah.SilahAtisModu;
                cmbBoxMermitipi.Text = avsilah.MermiTipi;
                txtSarjorKapasitesi.Text = avsilah.SarjorKapasite.ToString();
                cmbBoxSilahRengi.Text = avsilah.SilahRengi;
                txtSilahFiyat.Text = avsilah.SilahFiyat.ToString();

                foreach (var item in list)
                {
                    item.Contains(avsilah.AvSilahID.ToString());
                    picboxSilah.ImageLocation = item;
                }
                pnlAv.Enabled = true;
               

            }
            else
            {
                MessageBox.Show("Aradığınız kayıt bulunamadı.");

                SilahTemizle();
            }
        }

        private void btnSilahEkle_Click(object sender, EventArgs e)
        {
            avSilah.SilahTipi = cmbBoxSilahTipi.Text;
            avSilah.SilahAdi = txtSilahAdi.Text;
            avSilah.SilahAtisModu = cmbBoxSilahAtisModu.Text;
            avSilah.MermiTipi = cmbBoxMermitipi.Text;
            avSilah.SarjorKapasite = Convert.ToInt32(txtSarjorKapasitesi.Text);
            avSilah.SilahRengi = cmbBoxSilahRengi.Text;
            avSilah.SilahFiyat = Convert.ToDecimal(txtSilahFiyat.Text);

            string Mesaj = Avcilik.AvSilahlari.SilahEkle(avSilah);

            SilahTemizle();

            MessageBox.Show("Lütfen dosya ismini silahın ID'si olacak şekilde ayarlayınız.");

            MessageBox.Show("Silahın ID'si:" + " " + Mesaj);
            ResimEkle();
            
        }

        private void btnSilahSil_Click(object sender, EventArgs e)
        {
            dr = MessageBox.Show("Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                avSilah.AvSilahID = int.Parse(txtSilahID.Text);
                Avcilik.AvSilahlari.SilahSil(avSilah);
                pnlAv.Enabled = false;
            }
        }
        private void btnSilahGuncelle_Click(object sender, EventArgs e)
        {
            avSilah.AvSilahID = int.Parse(txtSilahID.Text);
            avSilah.SilahTipi = cmbBoxSilahTipi.Text;
            avSilah.SilahAdi = txtSilahAdi.Text;
            avSilah.SilahAtisModu = cmbBoxSilahAtisModu.Text;
            avSilah.MermiTipi = cmbBoxMermitipi.Text;
            avSilah.SarjorKapasite = Convert.ToInt32(txtSarjorKapasitesi.Text);
            avSilah.SilahRengi = cmbBoxSilahRengi.Text;
            avSilah.SilahFiyat = Convert.ToDecimal(txtSilahFiyat.Text);

            dr = MessageBox.Show("Silahın resmini güncellemek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Lütfen dosya ismini silahın ID'si olacak şekilde ayarlayınız.");
                ResimEkle();
            }


            Avcilik.AvSilahlari.SilahGüncelle(avSilah);


            SilahTemizle();
            pnlAv.Enabled = false;
        }

        private void btnSilahListele_Click(object sender, EventArgs e)
        {

            Avcilik.AvSilahlari.SilahListele(dgvSilahListe);

        }
        private void SilahTemizle()
        {
            cmbBoxSilahTipi.Text="";
            txtSilahAdi.Clear();
            cmbBoxSilahAtisModu.Text="";
            cmbBoxMermitipi.Text="";
            txtSarjorKapasitesi.Clear();
            cmbBoxSilahRengi.Text="";
            txtSilahFiyat.Clear();
        }

        private void btnMalzemeAra_Click(object sender, EventArgs e)
        {
            AvMalzeme malzeme = Avcilik.AvMalzemeleri.MalzemeAra(int.Parse(txtMalzemeID.Text));

            if (malzeme.AvMalzemeID > 0)
            {
                txtMalzemeID.Text = malzeme.AvMalzemeID.ToString();
                cmbBoxKiyafetTip.Text = malzeme.KiyafetTipi;
                cmbBoxKiyafetRenk.Text = malzeme.KiyafetRengi;
                cmbBoxSapkaTip.Text = malzeme.SapkaTipi;
                cmbBoxSapkaRenk.Text = malzeme.SapkaRengi;
                cmbBoxKemerTip.Text = malzeme.EkipmanKemerTipi;
                cmbBoxKemerRenk.Text = malzeme.EkipmanKemerRengi;
                cmbBoxBotTip.Text = malzeme.BotTipi;
                cmbBoxBotRenk.Text = malzeme.BotRengi;
                cmbBoxDurbunBoyut.Text = malzeme.DurbunBoyutu;
                cmbBoxCantaBoyut.Text = malzeme.CantaBoyutu;
                cmbBoxCantaRenk.Text = malzeme.CantaRengi;
                txtMalzemeFiyat.Text = malzeme.MalzemeFiyat.ToString();



                pnlMalzeme.Enabled = true;
            }
            else
            {
                MessageBox.Show("Aradığınız kayıt bulunamadı.");

                MalzemeTemizle();
            }
        }



        private void btnMalzemeEkle_Click(object sender, EventArgs e)
        {

            malzeme.KiyafetTipi = cmbBoxKiyafetTip.Text;
            malzeme.KiyafetRengi = cmbBoxKiyafetRenk.Text;
            malzeme.SapkaTipi = cmbBoxSapkaTip.Text;
            malzeme.SapkaRengi = cmbBoxSapkaRenk.Text;
            malzeme.EkipmanKemerTipi = cmbBoxKemerTip.Text;
            malzeme.EkipmanKemerRengi = cmbBoxKemerRenk.Text;
            malzeme.BotTipi = cmbBoxBotTip.Text;
            malzeme.BotRengi = cmbBoxBotRenk.Text;
            malzeme.DurbunBoyutu = cmbBoxDurbunBoyut.Text;
            malzeme.CantaBoyutu = cmbBoxCantaBoyut.Text;
            malzeme.CantaRengi = cmbBoxCantaRenk.Text;
            malzeme.MalzemeFiyat = Convert.ToDecimal(txtMalzemeFiyat.Text);

            Avcilik.AvMalzemeleri.MalzemeEkle(malzeme);

            MalzemeTemizle();


        }

        private void btnMalzemeSil_Click(object sender, EventArgs e)
        {
            dr = MessageBox.Show("Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                malzeme.AvMalzemeID = int.Parse(txtMalzemeID.Text);
                Avcilik.AvMalzemeleri.MalzemeSil(malzeme);
                pnlMalzeme.Enabled = false;
            }
        }

        private void btnMalzemeGuncelle_Click(object sender, EventArgs e)
        {

            malzeme.AvMalzemeID = int.Parse(txtMalzemeID.Text);
            malzeme.KiyafetTipi = cmbBoxKiyafetTip.Text;
            malzeme.KiyafetRengi = cmbBoxKiyafetRenk.Text;
            malzeme.SapkaTipi = cmbBoxSapkaTip.Text;
            malzeme.SapkaRengi = cmbBoxSapkaRenk.Text;
            malzeme.EkipmanKemerTipi = cmbBoxKemerTip.Text;
            malzeme.EkipmanKemerRengi = cmbBoxKemerRenk.Text;
            malzeme.BotTipi = cmbBoxBotTip.Text;
            malzeme.BotRengi = cmbBoxBotRenk.Text;
            malzeme.DurbunBoyutu = cmbBoxDurbunBoyut.Text;
            malzeme.CantaBoyutu = cmbBoxCantaBoyut.Text;
            malzeme.CantaRengi = cmbBoxCantaRenk.Text;
            malzeme.MalzemeFiyat = Convert.ToDecimal(txtMalzemeFiyat.Text);

            Avcilik.AvMalzemeleri.MalzemeGüncelle(malzeme);
            MalzemeTemizle();
            pnlMalzeme.Enabled = true;
        }

        private void btnMalzemeListele_Click(object sender, EventArgs e)
        {
            Avcilik.AvMalzemeleri.MalzemeListele(dgvMalzemeListe);
        }

        private void MalzemeTemizle()
        {
            cmbBoxKiyafetTip.Text="";
            cmbBoxKiyafetRenk.Text = "";
            cmbBoxSapkaTip.Text="";
            cmbBoxSapkaRenk.Text="";
            cmbBoxKemerTip.Text="";
            cmbBoxKemerRenk.Text="";
            cmbBoxBotTip.Text="";
            cmbBoxBotRenk.Text="";
            cmbBoxDurbunBoyut.Text="";
            cmbBoxCantaBoyut.Text="";
            cmbBoxCantaRenk.Text="";
            txtMalzemeFiyat.Clear();
        }

        private void btnEkipmanAra_Click(object sender, EventArgs e)
        {
            AvSilah avsilah = Avcilik.AvSilahlari.SilahAra(int.Parse(txtSilahID.Text));
            if (avsilah.AvSilahID > 0)
            {
                txtSilahID.Text = avsilah.AvSilahID.ToString();
                cmbBoxSilahTipi.Text = avsilah.SilahTipi;
                txtSilahAdi.Text = avsilah.SilahAdi;
                cmbBoxSilahAtisModu.Text = avsilah.SilahAtisModu;
                cmbBoxMermitipi.Text = avsilah.MermiTipi;
                txtSarjorKapasitesi.Text = avsilah.SarjorKapasite.ToString();
                cmbBoxSilahRengi.Text = avsilah.SilahRengi;
                txtSilahFiyat.Text = avsilah.SilahFiyat.ToString();

                pnlAv.Enabled = true;
            }
            else
            {
                MessageBox.Show("Aradığınız kayıt bulunamadı.");

                SilahTemizle();
            }

            AvEkipman ekipman = Avcilik.AvEkipmanlari.EkipmanAra(int.Parse(txtEkipmanID.Text));
            if (ekipman.AvEkipmanID > 0)
            {
                txtEkipmanID.Text = ekipman.AvEkipmanID.ToString();
                txtAvTuru.Text = ekipman.AvTuru;
                txtEkipmanSilahID.Text = ekipman.AvSilahID.ToString();
                txtEkipmanSilahFiyat.Text = ekipman.AvSilahFiyat.ToString();
                txtEkipmanMalzemeID.Text = ekipman.AvMalzemeFiyat.ToString();

            }
        }

        private void btnEkipmanEkle_Click(object sender, EventArgs e)
        {

        }

        private void btnEkipmanSil_Click(object sender, EventArgs e)
        {

        }

        private void btnEkipmanGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void btnEkipmanListe_Click(object sender, EventArgs e)
        {

        }
        private void ResimEkle()
        {
            ofdResimEkle.ShowDialog();

            list.Add(ofdResimEkle.FileName);
        }
    }
}
