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
    class AvSilahMN
    {

        string connstr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn;

        public AvSilah SilahAra(int ID)
        {
            conn = new SqlConnection(connstr);
            conn.Open();
            AvSilah avsilah = new AvSilah();

            string strSQLID = "select*from Avsilah where AvSilahID=@silahid";

            SqlCommand cmd = new SqlCommand(strSQLID, conn);
            cmd.Parameters.AddWithValue("@silahid", ID);
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            if (dr.HasRows)
            {
                avsilah.AvSilahID = dr.GetInt32(0);
                avsilah.SilahTipi = dr[1].ToString();
                avsilah.SilahAdi = dr[2].ToString();
                avsilah.SilahAtisModu = dr[3].ToString();
                avsilah.MermiTipi = dr[4].ToString();
                avsilah.SarjorKapasite = dr.GetInt32(5);
                avsilah.SilahRengi = dr[6].ToString();
                avsilah.SilahFiyat = Convert.ToDecimal(dr[7].ToString());
            }
            else
            {
                avsilah.AvSilahID = -1;
            }
            conn.Close();
            return avsilah;
        }

        public string SilahEkle(AvSilah silah)
        {
            conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("gp_SilahEkle", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@yeniID", SqlDbType.Int);
            cmd.Parameters.AddWithValue("@silahtip", silah.SilahTipi);
            cmd.Parameters.AddWithValue("@Silahad", silah.SilahAdi);
            cmd.Parameters.AddWithValue("@atismod", silah.SilahAtisModu);
            cmd.Parameters.AddWithValue("@mermitipi", silah.MermiTipi);
            cmd.Parameters.AddWithValue("@sarjorkapasite", silah.SarjorKapasite);
            cmd.Parameters.AddWithValue("@silahrenk", silah.SilahRengi);
            cmd.Parameters.AddWithValue("@fiyat", silah.SilahFiyat);

            cmd.Parameters[0].Direction = ParameterDirection.ReturnValue;


            cmd.ExecuteNonQuery();
            conn.Close();

            return cmd.Parameters[0].Value.ToString();

        }

        public void SilahSil(AvSilah silah)
        {
            conn = new SqlConnection(connstr);

            conn.Open();

            string strSQLDelete = "Delete from AvSilah where avsilahıd=@silahID";

            SqlCommand cmd = new SqlCommand(strSQLDelete, conn);

            cmd.Parameters.AddWithValue("@silahID", silah.AvSilahID);

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void SilahGüncelle(AvSilah silah)
        {
            conn = new SqlConnection(connstr);
            conn.Open();

            string strSQLUpdate = "";

            strSQLUpdate = "update avsilah set silahtipi=@tip,silahadi=@ad,silahatismodu=@mod,mermitipi=@mermitip,sarjorkapasite=@sarjor,silahrenk=@renk,fiyat=@fiyat where avsilahID=@ID";
            SqlCommand cmd = new SqlCommand(strSQLUpdate, conn);

            cmd.Parameters.AddWithValue("@ID", silah.AvSilahID);
            cmd.Parameters.AddWithValue("@tip", silah.SilahTipi);
            cmd.Parameters.AddWithValue("@ad", silah.SilahAdi);
            cmd.Parameters.AddWithValue("@mod", silah.SilahAtisModu);
            cmd.Parameters.AddWithValue("@mermitip", silah.MermiTipi);
            cmd.Parameters.AddWithValue("@sarjor", silah.SarjorKapasite);
            cmd.Parameters.AddWithValue("@renk", silah.SilahRengi);
            cmd.Parameters.AddWithValue("@fiyat", silah.SilahFiyat);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SilahListele(DataGridView dgvSilah)
        {
            conn = new SqlConnection(connstr);

            SqlDataAdapter da = new SqlDataAdapter("select*from avsilah", connstr);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvSilah.DataSource = dt;
            
            //conn.Open();


            //SqlCommand cmd = new SqlCommand("select*from avsilah order by avsilahıd", conn);

            //SqlDataReader dr = cmd.ExecuteReader();

            //DataTable dt = new DataTable();
            //;

            //dgvSilah.ColumnCount = dr.FieldCount;

            //object[] veriler = new object[dr.FieldCount];

            //for (int i = 0; i < dr.FieldCount; i++)
            //{
            //    dgvSilah.Columns[i].Name = dr.GetName(i);
            //}



            //while (dr.Read())
            //{
            //    for (int i = 0; i < dr.FieldCount; i++)
            //    {
            //        veriler[i] = dr[i];
            //    }
            //    dgvSilah.Rows.Add(veriler);
            //}
            //conn.Close();


        }
    }
}
