using System;
using System.Collections.Generic;
using System.Configuration;
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

        }
        public void EkipmanSil(AvEkipman ekipman)
        {

        }
        public void EkipmanGüncelle(AvEkipman ekipman)
        {

        }
        public List<AvEkipman> EkipmanListele()
        {
            return new List<AvEkipman>();
        }

    }
}
