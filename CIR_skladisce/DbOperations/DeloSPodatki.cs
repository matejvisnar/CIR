using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MySql.Data.MySqlClient;

namespace DbOperations
{
    public class DeloSPodatki
    {
        #region SELECT
       
        /// <summary>
        /// PRIMER
        /// </summary>
        /*public DataTable getDogodki()
        {
            DataTable dataTable = new DataTable();

            try
            {
                DbPovezava povezava = new DbPovezava();
                string sql = @"SELECT d.*, t.*, l.*, COUNT(k.ID) AS St_kom
                            FROM dogodek d LEFT JOIN lokacija_dogodka l
                            ON d.LOKACIJA_DOGODKA_ID = l.ID
                            LEFT JOIN tip_dogodka t
                            ON d.TIP_DOGODKA_ID = t.ID
                            LEFT JOIN komentar_ocena_dogodka k
                            ON k.DOGODEK_ID = d.ID AND k.Deleted = 0
                            WHERE d.Deleted = 0
                            GROUP BY d.ID";

                MySqlCommand cmd = new MySqlCommand(sql, povezava.Connection);
                povezava.odpriPovezavo();
                povezava.zapriPovezavo();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dataTable);
                }

                return dataTable;
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }*/

        #endregion

        #region INSERT
        
        /// <summary>
        /// Primer
        /// </summary>
      /*  public void insertEmail(string email)
        {
            try
            {
                DbPovezava povezava = new DbPovezava();
                string sql = @"INSERT INTO obvescanje (Email) VALUES ('" + email + "')";

                MySqlCommand cmd = new MySqlCommand(sql, povezava.Connection);
                povezava.odpriPovezavo();
                cmd.ExecuteNonQuery();
                povezava.zapriPovezavo();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }*/

        #endregion

        #region UPDATE
        /// <summary>
        /// PRIMER
        /// </summary>
        /// <param name="idPrijave"></param>
        /*public void updateOdjavaZDogodka(int idPrijave)
        {
            try
            {
                DbPovezava povezava = new DbPovezava();
                string sql = @"UPDATE prijava SET Deleted = 1 WHERE ID = " + idPrijave;

                MySqlCommand cmd = new MySqlCommand(sql, povezava.Connection);
                povezava.odpriPovezavo();
                cmd.ExecuteNonQuery();
                povezava.zapriPovezavo();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }*/
        #endregion

        #region AVENTIFIKACIJA
        /// <summary>
        /// Avetifikacija
        /// </summary>
        public DataTable callAventifikacija(string up_ime, string geslo)
        {
            DataTable dataTable = new DataTable();

            try
            {
                DbPovezava povezava = new DbPovezava();

                povezava.odpriPovezavo();
                MySqlCommand cmd = povezava.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "avtentifikacija";
                cmd.Parameters.Add(new MySqlParameter("upIme", up_ime));
                cmd.Parameters.Add(new MySqlParameter("upGeslo", geslo));

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dataTable);
                }

                povezava.zapriPovezavo();

                return dataTable;
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }
        #endregion
    }
}
