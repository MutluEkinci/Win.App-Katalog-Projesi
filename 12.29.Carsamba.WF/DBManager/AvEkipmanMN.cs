using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using _12._29.Carsamba.WF.Model;

namespace _12._29.Carsamba.WF.DBManager
{
    class AvEkipmanMN
    {
        string connstr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn;
        public AvEkipman EkipmanAra(int ID)
        {
            conn = new SqlConnection(connstr);
            conn.Open();
            AvEkipman ekipman = new AvEkipman();

            string strSQLID = "select*from avekipman where avekipmanID=@id";

            SqlCommand cmd = new SqlCommand(strSQLID, conn);

            cmd.Parameters.AddWithValue("@id", ID);

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            if (dr.HasRows)
            {
                ekipman.AvEkipmanID = dr.GetInt32(0);
                ekipman.AvTuru = dr[1].ToString();
                ekipman.AvSilahID = dr.GetInt32(2);
                ekipman.AvSilahFiyat = dr.GetDecimal(3);
                ekipman.AvMalzemeID = dr.GetInt32(4);
                ekipman.AvMalzemeFiyat = dr.GetDecimal(5);
                ekipman.ToplamFiyat = dr.GetDecimal(6);
            }
            else
            {
                ekipman.AvEkipmanID = -1;
            }
            conn.Close();
            return ekipman;
        }
        public void EkipmanEkle(AvEkipman ekipman)
        {
            conn = new SqlConnection(connstr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("insert into avekipman Values(@avturu,@avsilahID,@avsilahfiyat,@avmalzemeıd,@avmalzemefiyat)", conn);

            cmd.Parameters.AddWithValue("@avturu", ekipman.AvTuru);
            cmd.Parameters.AddWithValue("@avsilahıd", ekipman.AvSilahID);
            cmd.Parameters.AddWithValue("@avsilahfiyat", ekipman.AvSilahFiyat);
            cmd.Parameters.AddWithValue("@avmalzemeıd", ekipman.AvMalzemeID);
            cmd.Parameters.AddWithValue("@avmalzemefiyat", ekipman.AvMalzemeFiyat);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void EkipmanSil(AvEkipman ekipman)
        {
            conn = new SqlConnection(connstr);
            conn.Open();

            string strSQLDelete = "Delete from avekipman where avekipmanID=@ekipmanID";

            SqlCommand cmd = new SqlCommand(strSQLDelete, conn);

            cmd.Parameters.AddWithValue("@ekipmanID", ekipman.AvEkipmanID);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void EkipmanGüncelle(AvEkipman ekipman)
        {
            conn = new SqlConnection(connstr);
            conn.Open();

            string strSQLUpdate = "update avekipman set AvTuru=@tur,AvsilahID=@silahıd,avsilahfiyat=@silahfiyat,AvmalzemeID=@malzemeID,avmalzemefiyat=@malzemefiyat where avekipmanID=@ID";

            SqlCommand cmd = new SqlCommand(strSQLUpdate, conn);

            cmd.Parameters.AddWithValue("@ıd", ekipman.AvEkipmanID);
            cmd.Parameters.AddWithValue("@tur", ekipman.AvTuru);
            cmd.Parameters.AddWithValue("@silahıd", ekipman.AvSilahID);
            cmd.Parameters.AddWithValue("@silahfiyat", ekipman.AvSilahFiyat);
            cmd.Parameters.AddWithValue("@malzemeID", ekipman.AvMalzemeID);
            cmd.Parameters.AddWithValue("@malzemefiyat", ekipman.AvMalzemeFiyat);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void EkipmanListele(DataGridView dgvEkipman)
        {
            conn = new SqlConnection(connstr);

            SqlDataAdapter da = new SqlDataAdapter("select AvEkipmanID,AvTuru,silah.AvSilahID,SilahAdi,AvSilahFiyat,malzeme.AvMalzemeID,KiyafetTipi,SapkaTipi,EkipmanKemerTipi,BotTipi,DurbunBoyutu,CantaBoyutu,AvMalzemeFiyat,ToplamFiyat from AvSilah silah,AvEkipman ekipman,AvMalzeme malzeme where silah.AvSilahID=ekipman.AvSilahID and ekipman.AvMalzemeID=malzeme.AvMalzemeID", connstr);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvEkipman.DataSource = dt;
        }

    }
}
