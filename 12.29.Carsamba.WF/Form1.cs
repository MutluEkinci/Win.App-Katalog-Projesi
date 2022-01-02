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

                pnlAv.Enabled = true;

                try
                {
                    StreamReader sr = new StreamReader("" + avsilah.AvSilahID + ".bak");
                    picboxSilah.ImageLocation = sr.ReadLine();
                }
                catch
                {
                    MessageBox.Show("Bu silahın resmini güncelleyiniz.");
                }

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


            string mesaj = Avcilik.AvSilahlari.SilahEkle(avSilah);

            avSilah.AvSilahID = int.Parse(mesaj);

            MessageBox.Show("Lütfen dosya ismini silahın ID'si olacak şekilde ayarlayınız.");

            MessageBox.Show("Silahın ID'si:" + " " + mesaj);
            SilahResimEkle();

            SilahTemizle();
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
                SilahResimEkle();
            }


            Avcilik.AvSilahlari.SilahGüncelle(avSilah);


            SilahTemizle();
            pnlAv.Enabled = false;
        }

        private void btnSilahListele_Click(object sender, EventArgs e)
        {

            Avcilik.AvSilahlari.SilahListele(dgvSilahListe);

        }
        private void SilahResimEkle()
        {
            ofdResimEkle.ShowDialog();

            StreamWriter sw = new StreamWriter("" + avSilah.AvSilahID + ".bak");

            sw.Write(ofdResimEkle.FileName);
            sw.Close();

        }
        private void SilahTemizle()
        {
            cmbBoxSilahTipi.Text = "";
            txtSilahAdi.Clear();
            cmbBoxSilahAtisModu.Text = "";
            cmbBoxMermitipi.Text = "";
            txtSarjorKapasitesi.Clear();
            cmbBoxSilahRengi.Text = "";
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

                try
                {
                    StreamReader sr = new StreamReader("" + malzeme.AvMalzemeID + ".bak");
                    picBoxKiyafet.ImageLocation = sr.ReadLine();
                    picBoxSapka.ImageLocation = sr.ReadLine();
                    picBoxKemer.ImageLocation = sr.ReadLine();
                    picBoxBot.ImageLocation = sr.ReadLine();
                    picBoxDurbun.ImageLocation = sr.ReadLine();
                    picBoxCanta.ImageLocation = sr.ReadLine();

                }
                catch
                {
                    MessageBox.Show("Bu malzemenin resmini güncelleyiniz.");
                }
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

            string mesaj = Avcilik.AvMalzemeleri.MalzemeEkle(malzeme);

            malzeme.AvMalzemeID = int.Parse(mesaj);

            MessageBox.Show("Lütfen dosya ismini malzemenin ID'si olacak şekilde ayarlayınız.");

            MessageBox.Show("Malzemenin ID'si:" + " " + mesaj);

            MalzemeResimEkle();
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

            dr = MessageBox.Show("Malzemenin resmini güncellemek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Lütfen dosya ismini malzemenin ID'si olacak şekilde ayarlayınız.");
                MessageBox.Show("Resim sırasını veri girişine uygun olarak seçiniz.");
                MalzemeResimEkle();

            }

            Avcilik.AvMalzemeleri.MalzemeGüncelle(malzeme);

            MalzemeTemizle();
            pnlMalzeme.Enabled = true;
        }

        private void btnMalzemeListele_Click(object sender, EventArgs e)
        {
            Avcilik.AvMalzemeleri.MalzemeListele(dgvMalzemeListe);
        }
        private void MalzemeResimEkle()
        {
            MessageBox.Show("Kıyafet için resim seçiniz...");
            ofdKiyafet.ShowDialog(lblKiyafetTip);
            MessageBox.Show("Şapka için resim seçiniz...");
            ofdSapka.ShowDialog();
            MessageBox.Show("Kemer için resim seçiniz...");
            ofdKemer.ShowDialog();
            MessageBox.Show("Bot için resim seçiniz...");
            ofdBot.ShowDialog();
            MessageBox.Show("Dürbün için resim seçiniz...");
            ofdDurbun.ShowDialog();
            MessageBox.Show("Çanta için resim seçiniz...");
            ofdCanta.ShowDialog();

            StreamWriter sw = new StreamWriter("" + malzeme.AvMalzemeID + ".bak");

            sw.WriteLine(ofdKiyafet.FileName);
            sw.WriteLine(ofdSapka.FileName);
            sw.WriteLine(ofdKemer.FileName);
            sw.WriteLine(ofdBot.FileName);
            sw.WriteLine(ofdDurbun.FileName);
            sw.WriteLine(ofdCanta.FileName);

            sw.Close();

        }

        private void MalzemeTemizle()
        {
            cmbBoxKiyafetTip.Text = "";
            cmbBoxKiyafetRenk.Text = "";
            cmbBoxSapkaTip.Text = "";
            cmbBoxSapkaRenk.Text = "";
            cmbBoxKemerTip.Text = "";
            cmbBoxKemerRenk.Text = "";
            cmbBoxBotTip.Text = "";
            cmbBoxBotRenk.Text = "";
            cmbBoxDurbunBoyut.Text = "";
            cmbBoxCantaBoyut.Text = "";
            cmbBoxCantaRenk.Text = "";
            txtMalzemeFiyat.Clear();
        }

        private void btnEkipmanAra_Click(object sender, EventArgs e)
        {
            AvEkipman ekipman = Avcilik.AvEkipmanlari.EkipmanAra(int.Parse(txtEkipmanID.Text));
            if (ekipman.AvEkipmanID > 0)
            {
                txtEkipmanID.Text = ekipman.AvEkipmanID.ToString();
                cmbBoxAvTuru.Text = ekipman.AvTuru;
                txtEkipmanSilahID.Text = ekipman.AvSilahID.ToString();
                txtEkipmanSilahFiyat.Text = ekipman.AvSilahFiyat.ToString();
                txtEkipmanMalzemeID.Text = ekipman.AvMalzemeID.ToString();
                txtEkipmanMalzemeFiyat.Text = ekipman.AvMalzemeFiyat.ToString();
                txtEkipmanToplamFiyat.Text = ekipman.ToplamFiyat.ToString();

                pnlEkipman.Enabled = true;
            }
            else
            {
                MessageBox.Show("Aradığınız kayıt bulunamadı.");
                EkipmanTemizle();

            }
        }

        private void btnEkipmanEkle_Click(object sender, EventArgs e)
        {
            ekipman.AvTuru = cmbBoxAvTuru.Text;
            ekipman.AvSilahID = int.Parse(txtEkipmanSilahID.Text);
            ekipman.AvSilahFiyat = Convert.ToDecimal(txtEkipmanSilahFiyat.Text);
            ekipman.AvMalzemeID = int.Parse(txtEkipmanMalzemeID.Text);
            ekipman.AvMalzemeFiyat = Convert.ToDecimal(txtEkipmanMalzemeFiyat.Text);

            Avcilik.AvEkipmanlari.EkipmanEkle(ekipman);

            EkipmanTemizle();
        }

        private void btnEkipmanSil_Click(object sender, EventArgs e)
        {
            dr = MessageBox.Show("Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                ekipman.AvEkipmanID = int.Parse(txtEkipmanID.Text);
                Avcilik.AvEkipmanlari.EkipmanSil(ekipman);

                pnlEkipman.Enabled = true;
            }
        }

        private void btnEkipmanGuncelle_Click(object sender, EventArgs e)
        {
            ekipman.AvEkipmanID = int.Parse(txtEkipmanID.Text);
            ekipman.AvTuru = cmbBoxAvTuru.Text;
            ekipman.AvSilahID = int.Parse(txtEkipmanSilahID.Text);
            ekipman.AvSilahFiyat = Convert.ToDecimal(txtEkipmanSilahFiyat.Text);
            ekipman.AvMalzemeID = int.Parse(txtEkipmanSilahID.Text);
            ekipman.AvMalzemeFiyat = Convert.ToDecimal(txtEkipmanMalzemeFiyat.Text);
            ekipman.ToplamFiyat = Convert.ToDecimal(txtEkipmanToplamFiyat.Text);

            Avcilik.AvEkipmanlari.EkipmanGüncelle(ekipman);

            EkipmanTemizle();
            pnlEkipman.Enabled = false;

        }

        private void btnEkipmanListe_Click(object sender, EventArgs e)
        {
            Avcilik.AvEkipmanlari.EkipmanListele(dgvEkipmanListe);
        }
        private void EkipmanTemizle()
        {
            cmbBoxAvTuru.Text = "";
            txtEkipmanSilahID.Clear();
            txtEkipmanSilahFiyat.Clear();
            txtEkipmanMalzemeID.Clear();
            txtEkipmanMalzemeFiyat.Clear();
            txtEkipmanToplamFiyat.Clear();
        }

        /*List<bolum> bolumler = new List<bolum>() {new bolum{ BolumID=11 , BolumAdi="Muhasebe" }; 
         combobox.datasource=bolumler;
        combobox.displaymember = "BolumAdi";
        combobox.Valuemember = "BolumID" ;

        */
    }
}
