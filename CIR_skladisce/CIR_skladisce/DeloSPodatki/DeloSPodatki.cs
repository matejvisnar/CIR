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
                    if (Convert.ToString(rdr[5]) != "")
                    {
                        naslov.Ulica = Convert.ToString(rdr[5]);
                        naslov.HisnaSt = Convert.ToString(rdr[6]);
                        naslov.Mesto = Convert.ToString(rdr[7]);
                        naslov.PostnaSt = Convert.ToInt32(rdr[8]);
                    }

                    uporabnik.Id = Convert.ToInt32(rdr[0]);
                    uporabnik.Ime = Convert.ToString(rdr[1]);
                    uporabnik.Priimek = Convert.ToString(rdr[2]);
                    uporabnik.Davcna = Convert.ToString(rdr[3]);
                    uporabnik.UporabniskoIme = Convert.ToString(rdr[4]);
                }
                rdr.Close();
                povezava.zapriPovezavo();
                return uporabnik;
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
        /// Uredi uporabnika
        /// </summary>
        /// 
        public void updateUporabnik(Uporabnik uporabnik)
        {
            try
            {
                Naslov naslov = uporabnik.Naslov;
                povezava.odpriPovezavo();

                //Pridobi id drzave
                MySqlCommand cmdPridobiIdDrzava = povezava.Connection.CreateCommand();
                cmdPridobiIdDrzava.CommandText = "SELECT id from drzava WHERE naziv= @naziv ";
                cmdPridobiIdDrzava.Parameters.Add("@naziv", naslov.Drzava);

                MySqlDataReader rdrPridobiIdDrzava = cmdPridobiIdDrzava.ExecuteReader();
                int idDrzava = 0;
                while (rdrPridobiIdDrzava.Read())
                {
                    idDrzava = Convert.ToInt32(rdrPridobiIdDrzava[0]);
                }
                rdrPridobiIdDrzava.Close();

                if (idDrzava == 0)
                {
                    MySqlCommand cmdInsertDrzava = povezava.Connection.CreateCommand();
                    cmdInsertDrzava.CommandText = "INSERT INTO drzava(naziv) VALUES (@naziv)";
                    cmdInsertDrzava.Parameters.Add("@naziv", naslov.Drzava);
                    idDrzava = Convert.ToInt32(cmdInsertDrzava.LastInsertedId);
                    cmdInsertDrzava.ExecuteNonQuery();
                }

                //Pridobi id mesto
                MySqlCommand cmdPridobiIdMesto = povezava.Connection.CreateCommand();
                cmdPridobiIdMesto.CommandText = "SELECT id from mesto WHERE kraj= @kraj ";
                cmdPridobiIdMesto.Parameters.Add("@kraj", naslov.Mesto);

                MySqlDataReader rdrPridobidMesto = cmdPridobiIdMesto.ExecuteReader();
                int idMesto = 0;
                while (rdrPridobidMesto.Read())
                {
                    idMesto = Convert.ToInt32(rdrPridobidMesto[0]);
                }
                rdrPridobidMesto.Close();

                if (idMesto == 0)
                {
                    MySqlCommand cmdInsertMesto = povezava.Connection.CreateCommand();
                    cmdInsertMesto.CommandText = "INSERT INTO mesto(kraj, posta) VALUES (@kraj,@posta)";
                    cmdInsertMesto.Parameters.Add("@kraj", naslov.Mesto);
                    cmdInsertMesto.Parameters.Add("@posta", naslov.PostnaSt);
                    idMesto = Convert.ToInt32(cmdInsertMesto.LastInsertedId);
                    cmdInsertMesto.ExecuteNonQuery();
                }

                //Uredi naslov
                //Pridobi id drzave
                MySqlCommand cmdPridobiNaslov = povezava.Connection.CreateCommand();
                cmdPridobiNaslov.CommandText = "SELECT id from naslov WHERE ulica= @ulica AND hisna_st = @hisna_st ";
                cmdPridobiNaslov.Parameters.Add("@ulica", naslov.Ulica);
                cmdPridobiNaslov.Parameters.Add("@hisna_st", naslov.HisnaSt);

                MySqlDataReader rdrPridobiIdNaslov = cmdPridobiIdDrzava.ExecuteReader();
                int idNaslov = 0;

                while (rdrPridobiIdNaslov.Read())
                {
                    idNaslov = Convert.ToInt32(rdrPridobiIdNaslov[0]);
                }
                rdrPridobiIdNaslov.Close();


                if (idNaslov == 0)
                {
                    MySqlCommand cmdInsertNaslov = povezava.Connection.CreateCommand();
                    cmdInsertNaslov.CommandText = "INSERT INTO naslov(mesto_id, Ulica, hisna_st) VALUES (@mesto_id, @ulica,@hisna_st)";
                    cmdInsertNaslov.Parameters.Add("@mesto_id", idMesto);
                    cmdInsertNaslov.Parameters.Add("@Ulica", naslov.Ulica);
                    cmdInsertNaslov.Parameters.Add("@hisna_st", naslov.HisnaSt);
                    idNaslov = Convert.ToInt32(cmdInsertNaslov.LastInsertedId);
                    cmdInsertNaslov.ExecuteNonQuery();
                }

                MySqlCommand cmdUrediNaslov = povezava.Connection.CreateCommand();
                cmdUrediNaslov.CommandText = "UPDATE Naslov SET mesto_id = @mesto_id, ulica = '@ulica', hisna_st = @hisna_st " +
                                      " WHERE id = @id ";
                cmdUrediNaslov.Parameters.Add("@mesto_id", idMesto);
                cmdUrediNaslov.Parameters.Add("@ulica", naslov.Ulica);
                cmdUrediNaslov.Parameters.Add("@hisna_st", naslov.HisnaSt);
                cmdUrediNaslov.Parameters.Add("@id", naslov.Id);
                cmdUrediNaslov.ExecuteNonQuery();

                MySqlCommand cmd = povezava.Connection.CreateCommand();
                cmd.CommandText = "UPDATE Uporabnik SET naslov_id = @naslov_id, ime = @ime, priimek = '@priimek', uporabnisko_ime = @uporabnisko_ime, davcna_st = @davcna_st " +
                                  " WHERE u.id = @id ";
                cmd.Parameters.Add("@naslov_id", idNaslov);
                cmd.Parameters.Add("@ime", uporabnik.Ime);
                cmd.Parameters.Add("@priimek", uporabnik.Priimek);
                cmd.Parameters.Add("@uporabnisko_ime", uporabnik.UporabniskoIme);
                cmd.Parameters.Add("@davcna_st", uporabnik);
                cmd.Parameters.Add("@id", uporabnik.Id);
                cmd.ExecuteNonQuery();

                povezava.zapriPovezavo();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        public void spremeniGeslo(int userId, string novoGeslo)
        {
            try
            {
                string sql = @"UPDATE uporabnik SET geslo = SHA1(@geslo) WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, povezava.Connection);
                povezava.odpriPovezavo();
                cmd.Parameters.Add("@geslo", novoGeslo);
                cmd.Parameters.Add("@id", userId);
                cmd.ExecuteNonQuery();
                povezava.zapriPovezavo();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }
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
