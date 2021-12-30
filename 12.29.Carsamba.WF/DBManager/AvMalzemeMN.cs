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
    class AvMalzemeMN
    {
        string connstr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn;
        public AvMalzeme MalzemeAra(int ID)
        {
            conn = new SqlConnection(connstr);
            conn.Open();
            AvMalzeme malzeme = new AvMalzeme();

            string strSQLID = "select*from avmalzeme where avmalzemeID=@id";

            SqlCommand cmd = new SqlCommand(strSQLID, conn);
            cmd.Parameters.AddWithValue("@id", ID);
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            if (dr.HasRows)
            {
                malzeme.AvMalzemeID = dr.GetInt32(0);
                malzeme.KiyafetTipi = dr[1].ToString();
                malzeme.KiyafetRengi = dr[2].ToString();
                malzeme.SapkaTipi = dr[3].ToString();
                malzeme.SapkaRengi = dr[4].ToString();
                malzeme.EkipmanKemerTipi = dr[5].ToString();
                malzeme.EkipmanKemerRengi = dr[6].ToString();
                malzeme.BotTipi = dr[7].ToString();
                malzeme.BotRengi = dr[8].ToString();
                malzeme.DurbunBoyutu = dr[9].ToString();
                malzeme.CantaBoyutu = dr[10].ToString();
                malzeme.CantaRengi = dr[11].ToString();
                malzeme.MalzemeFiyat = dr.GetDecimal(12);
            }
            else
            {
                malzeme.AvMalzemeID = -1;
            }
            conn.Close();
            return malzeme;
        }
        public void MalzemeEkle(AvMalzeme malzeme)
        {
            conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("gp_MalzemeEkle", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@kiyafettipi",malzeme.KiyafetTipi);
            cmd.Parameters.AddWithValue("@kiyafetrengi",malzeme.KiyafetRengi);
            cmd.Parameters.AddWithValue("@sapkatipi",malzeme.SapkaTipi);
            cmd.Parameters.AddWithValue("@sapkarengi",malzeme.SapkaRengi);
            cmd.Parameters.AddWithValue("@kemertipi",malzeme.EkipmanKemerTipi);
            cmd.Parameters.AddWithValue("@kemerrengi",malzeme.EkipmanKemerRengi);
            cmd.Parameters.AddWithValue("@bottipi",malzeme.BotTipi);
            cmd.Parameters.AddWithValue("@botrengi",malzeme.BotRengi);
            cmd.Parameters.AddWithValue("@durbunboyutu",malzeme.DurbunBoyutu);
            cmd.Parameters.AddWithValue("@cantaboyutu",malzeme.CantaBoyutu);
            cmd.Parameters.AddWithValue("@cantarengi",malzeme.CantaRengi);
            cmd.Parameters.AddWithValue("@fiyat",malzeme.MalzemeFiyat);

            cmd.ExecuteNonQuery();
            conn.Close();

        }
        public void MalzemeSil(AvMalzeme malzeme)
        {
            conn = new SqlConnection(connstr);

            conn.Open();

            string strSQLDelete = "Delete from avmalzeme where avmalzemeID=@ID";

            SqlCommand cmd = new SqlCommand(strSQLDelete, conn);

            cmd.Parameters.AddWithValue("@ID", malzeme.AvMalzemeID);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void MalzemeGüncelle(AvMalzeme malzeme)
        {
            conn = new SqlConnection(connstr);
            conn.Open();

            string strSQLUpdate = "";

            strSQLUpdate = "update avmalzeme set kiyafettipi=@kiyafettipi,kiyafetrengi=@kiyafetrengi,sapkatipi=@sapkatipi,sapkarengi=@sapkarengi,ekipmankemertipi=@ekipmankemertipi,ekipmankemerrengi=@ekipmankemerrengi,bottipi=@bottipi,botrengi=@botrengi,durbunboyutu=@durbunboyutu,cantaboyutu=@cantaboyutu,cantarengi=@cantarengi,fiyat=@fiyat";
            SqlCommand cmd = new SqlCommand(strSQLUpdate, conn);

            cmd.Parameters.AddWithValue("@avmalzemeID", malzeme.AvMalzemeID);
            cmd.Parameters.AddWithValue("@kiyafettipi", malzeme.KiyafetTipi);
            cmd.Parameters.AddWithValue("@kiyafetrengi", malzeme.KiyafetRengi);
            cmd.Parameters.AddWithValue("@sapkatipi", malzeme.SapkaTipi);
            cmd.Parameters.AddWithValue("@sapkarengi", malzeme.SapkaRengi);
            cmd.Parameters.AddWithValue("@ekipmankemertipi", malzeme.EkipmanKemerTipi);
            cmd.Parameters.AddWithValue("@ekipmankemerrengi", malzeme.EkipmanKemerRengi);
            cmd.Parameters.AddWithValue("@bottipi", malzeme.BotTipi);
            cmd.Parameters.AddWithValue("@botrengi", malzeme.BotRengi);
            cmd.Parameters.AddWithValue("@durbunboyutu", malzeme.DurbunBoyutu);
            cmd.Parameters.AddWithValue("@cantaboyutu", malzeme.CantaBoyutu);
            cmd.Parameters.AddWithValue("@cantarengi", malzeme.CantaRengi);
            cmd.Parameters.AddWithValue("@fiyat", malzeme.MalzemeFiyat);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void MalzemeListele(DataGridView dgvMalzeme)
        {
            AvMalzeme m = new AvMalzeme();

            conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select*from avmalzeme order by avmalzemeID", conn);

            SqlDataReader dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dgvMalzeme.ColumnCount = dr.FieldCount;

            object[] veriler = new object[dr.FieldCount];

            for (int i = 0; i < dr.FieldCount; i++)
            {
                dgvMalzeme.Columns[i].Name = dr.GetName(i);
            }



            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    veriler[i] = dr[i];
                }
                dgvMalzeme.Rows.Add(veriler);
            }
            conn.Close();


        }
    }
}
