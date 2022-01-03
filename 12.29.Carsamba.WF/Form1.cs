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
            picboxSilah.ImageLocation = "";
            if (!(txtSilahID.Text == ""))
            {
                avSilah = Avcilik.AvSilahlari.SilahAra(int.Parse(txtSilahID.Text));

                if (avSilah.AvSilahID > 0)
                {
                    txtSilahID.Text = avSilah.AvSilahID.ToString();
                    cmbBoxSilahTipi.Text = avSilah.SilahTipi;
                    txtSilahAdi.Text = avSilah.SilahAdi;
                    cmbBoxSilahAtisModu.Text = avSilah.SilahAtisModu;
                    cmbBoxMermitipi.Text = avSilah.MermiTipi;
                    txtSarjorKapasitesi.Text = avSilah.SarjorKapasite.ToString();
                    cmbBoxSilahRengi.Text = avSilah.SilahRengi;
                    txtSilahFiyat.Text = avSilah.SilahFiyat.ToString();

                    pnlAv.Enabled = true;


                    StreamReader sr = new StreamReader("" + avSilah.AvSilahID + ".bak");
                    picboxSilah.ImageLocation = sr.ReadLine();

                    if (picboxSilah.ImageLocation == "openFileDialog1")
                    {
                        MessageBox.Show("Bu silahın resmini güncelleyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }

                }
                else
                {
                    MessageBox.Show("Aradığınız ID değeri bulunamadı!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SilahTemizle();
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ID değeri giriniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            MessageBox.Show("Silahın ID'si:" + " " + mesaj, "ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SilahResimEkle();

            MessageBox.Show("Silah başarıyla eklendi.", "BAŞARILI", MessageBoxButtons.OK);

            SilahTemizle();
        }

        private void btnSilahSil_Click(object sender, EventArgs e)
        {
            dr = MessageBox.Show("Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

            Avcilik.AvSilahlari.SilahGüncelle(avSilah);

            dr = MessageBox.Show("Silahın resmini güncellemek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                SilahResimEkle();
            }

            MessageBox.Show("Silah başarıyla güncellenmiştir.", "Başarılı", MessageBoxButtons.OK);

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

            StreamWriter sw = new StreamWriter("" + avSilah.AvSilahID + ".bak", false);

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
            picBoxKiyafet.ImageLocation = "";
            picBoxSapka.ImageLocation = "";
            picBoxKemer.ImageLocation = "";
            picBoxBot.ImageLocation = "";
            picBoxDurbun.ImageLocation = "";
            picBoxCanta.ImageLocation = "";

            if (!(txtMalzemeID.Text == ""))
            {
                malzeme = Avcilik.AvMalzemeleri.MalzemeAra(int.Parse(txtMalzemeID.Text));

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


                    StreamReader sr = new StreamReader("" + malzeme.AvMalzemeID + ".bak");
                    picBoxKiyafet.ImageLocation = sr.ReadLine();
                    picBoxSapka.ImageLocation = sr.ReadLine();
                    picBoxKemer.ImageLocation = sr.ReadLine();
                    picBoxBot.ImageLocation = sr.ReadLine();
                    picBoxDurbun.ImageLocation = sr.ReadLine();
                    picBoxCanta.ImageLocation = sr.ReadLine();

                    if (picBoxKiyafet.ImageLocation == "openFileDialog1" || picBoxSapka.ImageLocation == "openFileDialog1" || picBoxKemer.ImageLocation == "openFileDialog1" || picBoxBot.ImageLocation == "openFileDialog1" || picBoxDurbun.ImageLocation == "openFileDialog1" || picBoxCanta.ImageLocation == "openFileDialog1")
                    {
                        MessageBox.Show("Bu malzemenin resimlerini güncelleyiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Aradığınız ID değeri bulunamadı!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    MalzemeTemizle();
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir ID değeri giriniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            MessageBox.Show("Malzemenin ID'si:" + " " + mesaj, "ID", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MalzemeResimEkle();

            MessageBox.Show("Malzemeler başarıyla eklenmiştir.", "Başarılı", MessageBoxButtons.OK);
            MalzemeTemizle();
        }

        private void btnMalzemeSil_Click(object sender, EventArgs e)
        {
            dr = MessageBox.Show("Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

            dr = MessageBox.Show("Malzemenin resmini güncellemek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Resim sırasını veri girişine uygun olarak seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MalzemeResimEkle();
            }

            MessageBox.Show("Malzemeler başarıyla güncellenmiştir.", "Başarılı", MessageBoxButtons.OK);

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

            StreamWriter sw = new StreamWriter("" + malzeme.AvMalzemeID + ".bak", false);

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
            if (!(txtEkipmanID.Text == ""))
            {
                ekipman = Avcilik.AvEkipmanlari.EkipmanAra(int.Parse(txtEkipmanID.Text));

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
                    MessageBox.Show("Aradığınız ID değeri bulunamadı!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EkipmanTemizle();

                }
            }
            else
            {
                MessageBox.Show("Litfen bir ID değeri giriniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            MessageBox.Show("Ekipmanlar başarıyla eklenmiştir.", "Başarılı", MessageBoxButtons.OK);

            EkipmanTemizle();
        }

        private void btnEkipmanSil_Click(object sender, EventArgs e)
        {
            dr = MessageBox.Show("Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
            ekipman.AvMalzemeID = int.Parse(txtEkipmanMalzemeID.Text);
            ekipman.AvMalzemeFiyat = Convert.ToDecimal(txtEkipmanMalzemeFiyat.Text);
            ekipman.ToplamFiyat = Convert.ToDecimal(txtEkipmanToplamFiyat.Text);

            Avcilik.AvEkipmanlari.EkipmanGüncelle(ekipman);

            MessageBox.Show("Ekipmanlar başarıyla güncellenmiştir.", "Başarılı", MessageBoxButtons.OK);

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

        private void txtSilahID_Enter(object sender, EventArgs e)
        {
            toolTipID.Show("Silah eklerken ID girmenize gerek yoktur.", (IWin32Window)sender);
        }

        private void txtMalzemeID_Enter(object sender, EventArgs e)
        {
            toolTipID.Show("Malzeme eklerken ID girmenize gerek yoktur.", (IWin32Window)sender);
        }

        private void txtEkipmanID_Enter(object sender, EventArgs e)
        {
            toolTipID.Show("Ekipman eklerken ID girmenize gerek yoktur.", (IWin32Window)sender);
        }




        /*List<bolum> bolumler = new List<bolum>() {new bolum{ BolumID=11 , BolumAdi="Muhasebe" }; 
         combobox.datasource=bolumler;
        combobox.displaymember = "BolumAdi";
        combobox.Valuemember = "BolumID" ;

        */
    }
}
