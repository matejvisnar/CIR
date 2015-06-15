using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using CIR_skladisce.Models;

namespace DbOperations
{
    public class DeloSPodatki
    {
        private DbPovezava povezava;

        public DeloSPodatki()
        {
            povezava = new DbPovezava();
        }

        #region SELECT
       
        /// <summary>
        /// Pridobi uporabnika uporabnika po ID
        /// </summary>
        public Uporabnik getUporabnikaID(int ID)
        {
            try
            {
                povezava.odpriPovezavo();
                MySqlCommand cmd = povezava.Connection.CreateCommand();
                cmd.CommandText = "SELECT u.id,u.Ime,u.priimek,u.davcna_st,u.uporabnisko_ime,n.ulica, n.hisna_st, m.kraj, m.posta " +
                               " FROM UPORABNIK u LEFT JOIN NASLOV n ON u.Naslov_id = n.id "+
                               " LEFT JOIN MESTO m "+
                               " ON n.mesto_id = m.id "+
                               " WHERE u.id = @id ";
                cmd.Parameters.Add("@id", ID);
                MySqlDataReader rdr = cmd.ExecuteReader();

                Uporabnik uporabnik = new Uporabnik();
                while (rdr.Read())
                {
                    Naslov naslov = new Naslov();
                    naslov.Ulica =Convert.ToString(rdr[5]);
                    naslov.HisnaSt = Convert.ToString(rdr[6]);
                    naslov.Mesto = Convert.ToString(rdr[7]);
                    naslov.PostnaSt =Convert.ToInt32(rdr[8]);

                    uporabnik.Id = Convert.ToInt32(rdr[0]);
                    uporabnik.Ime = Convert.ToString(rdr[1]);
                    uporabnik.Priimek = Convert.ToString(rdr[2]);
                    uporabnik.Davcna = Convert.ToString(rdr[3]);
                    uporabnik.UporabniskoIme = Convert.ToString(rdr[4]);
                }
                rdr.Close();
                return uporabnik;
                povezava.zapriPovezavo();
            }
            catch (MySqlException ex)
            {
                throw;
            }

        }

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
        /// Avetifikacija uporabnika
        /// </summary>
        public DataTable Avtentifikacija(string up_ime, string geslo)
        {
            DataTable dataTable = new DataTable();

            try
            {
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

        #region REGISTRACIJA
        /// <summary>
        /// Registracija uporabnika
        /// </summary>
        /// <param name="novUporabnik"></param>
        public void Registracija(UporabnikRegistartion novUporabnik)
        {
            try
            {
                povezava.odpriPovezavo();
                MySqlCommand cmd = povezava.Connection.CreateCommand();
                cmd.CommandText = "INSERT INTO uporabnik(ime, priimek, uporabnisko_ime, geslo) VALUES (@ime, @priimek, @upIme, SHA1(@geslo))";
                cmd.Parameters.Add("@ime", novUporabnik.Ime);
                cmd.Parameters.Add("@priimek", novUporabnik.Priimek);
                cmd.Parameters.Add("@upIme", novUporabnik.UporabniskoIme);
                cmd.Parameters.Add("@geslo", novUporabnik.Geslo);
                cmd.ExecuteNonQuery();
                povezava.zapriPovezavo();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        #endregion
    }
}
