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
namespace _12._29.Carsamba.WF
{
    public partial class Form1 : Form
    {
        Avcilik Avcilik = new Avcilik();
        AvSilah avSilah = new AvSilah();
        AvMalzeme malzeme = new AvMalzeme();
        AvEkipman ekipman = new AvEkipman();
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
                txtSilahTipi.Text = avsilah.SilahTipi;
                txtSilahAdi.Text = avsilah.SilahAdi;
                txtSilahAtisModu.Text = avsilah.SilahAtisModu;
                txtMermitipi.Text = avsilah.MermiTipi;
                txtSarjorKapasitesi.Text = avsilah.SarjorKapasite.ToString();
                txtSilahRengi.Text = avsilah.SilahRengi;
                txtSilahFiyat.Text = avsilah.SilahFiyat.ToString();

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
            avSilah.SilahTipi = txtSilahTipi.Text;
            avSilah.SilahAdi = txtSilahAdi.Text;
            avSilah.SilahAtisModu = txtSilahAtisModu.Text;
            avSilah.MermiTipi = txtMermitipi.Text;
            avSilah.SarjorKapasite = Convert.ToInt32(txtSarjorKapasitesi.Text);
            avSilah.SilahRengi = txtSilahRengi.Text;
            avSilah.SilahFiyat = Convert.ToDecimal(txtSilahFiyat.Text);

            Avcilik.AvSilahlari.SilahEkle(avSilah);

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
            avSilah.SilahTipi = txtSilahTipi.Text;
            avSilah.SilahAdi = txtSilahAdi.Text;
            avSilah.SilahAtisModu = txtSilahAtisModu.Text;
            avSilah.MermiTipi = txtMermitipi.Text;
            avSilah.SarjorKapasite = Convert.ToInt32(txtSarjorKapasitesi.Text);
            avSilah.SilahRengi = txtSilahRengi.Text;
            avSilah.SilahFiyat = Convert.ToDecimal(txtSilahFiyat.Text);

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
            txtSilahTipi.Clear();
            txtSilahAdi.Clear();
            txtSilahAtisModu.Clear();
            txtMermitipi.Clear();
            txtSarjorKapasitesi.Clear();
            txtSilahRengi.Clear();
            txtSilahFiyat.Clear();
        }

        private void btnMalzemeAra_Click(object sender, EventArgs e)
        {
            AvMalzeme malzeme = Avcilik.AvMalzemeleri.MalzemeAra(int.Parse(txtMalzemeID.Text));

            if (malzeme.AvMalzemeID > 0)
            {
                txtMalzemeID.Text = malzeme.AvMalzemeID.ToString();
                txtKiyafetTip.Text = malzeme.KiyafetTipi;
                txtKiyafetRenk.Text= malzeme.KiyafetRengi;
                txtSapkaTip.Text=malzeme.SapkaTipi;
                txtSapkaRenk.Text=malzeme.SapkaRengi;
                txtKemerTip.Text=malzeme.EkipmanKemerTipi;
                txtKemerRenk.Text=malzeme.EkipmanKemerRengi;
                txtBotTip.Text=malzeme.BotTipi;
                txtBotRenk.Text=malzeme.BotRengi;
                txtDurbunBoyut.Text=malzeme.DurbunBoyutu;
                txtCantaBoyut.Text=malzeme.CantaBoyutu;
                txtCantaRenk.Text=malzeme.CantaRengi;
                txtMalzemeFiyat.Text=malzeme.MalzemeFiyat.ToString();

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

            malzeme.KiyafetTipi = txtKiyafetTip.Text;
            malzeme.KiyafetRengi = txtKiyafetRenk.Text;
            malzeme.SapkaTipi = txtSapkaTip.Text;
            malzeme.SapkaRengi = txtSapkaRenk.Text;
            malzeme.EkipmanKemerTipi = txtKemerTip.Text;
            malzeme.EkipmanKemerRengi = txtKemerRenk.Text;
            malzeme.BotTipi = txtBotTip.Text;
            malzeme.BotRengi = txtBotRenk.Text;
            malzeme.DurbunBoyutu = txtDurbunBoyut.Text;
            malzeme.CantaBoyutu = txtCantaBoyut.Text;
            malzeme.CantaRengi = txtCantaRenk.Text;
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
            malzeme.KiyafetTipi = txtKiyafetTip.Text;
            malzeme.KiyafetRengi = txtKiyafetRenk.Text;
            malzeme.SapkaTipi = txtSapkaTip.Text;
            malzeme.SapkaRengi = txtSapkaRenk.Text;
            malzeme.EkipmanKemerTipi = txtKemerTip.Text;
            malzeme.EkipmanKemerRengi = txtKemerRenk.Text;
            malzeme.BotTipi = txtBotTip.Text;
            malzeme.BotRengi = txtBotRenk.Text;
            malzeme.DurbunBoyutu = txtDurbunBoyut.Text;
            malzeme.CantaBoyutu = txtCantaBoyut.Text;
            malzeme.CantaRengi = txtCantaRenk.Text;
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
            txtKiyafetTip.Clear();
            txtKiyafetRenk.Clear();
            txtSapkaTip.Clear();
            txtSapkaRenk.Clear();
            txtKemerTip.Clear();
            txtKemerRenk.Clear();
            txtBotTip.Clear();
            txtBotRenk.Clear();
            txtDurbunBoyut.Clear();
            txtCantaBoyut.Clear();
            txtCantaRenk.Clear();
            txtMalzemeFiyat.Clear();
        }

        private void btnEkipmanAra_Click(object sender, EventArgs e)
        {
            AvSilah avsilah = Avcilik.AvSilahlari.SilahAra(int.Parse(txtSilahID.Text));
            if (avsilah.AvSilahID > 0)
            {
                txtSilahID.Text = avsilah.AvSilahID.ToString();
                txtSilahTipi.Text = avsilah.SilahTipi;
                txtSilahAdi.Text = avsilah.SilahAdi;
                txtSilahAtisModu.Text = avsilah.SilahAtisModu;
                txtMermitipi.Text = avsilah.MermiTipi;
                txtSarjorKapasitesi.Text = avsilah.SarjorKapasite.ToString();
                txtSilahRengi.Text = avsilah.SilahRengi;
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
    }
}
