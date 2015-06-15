using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Configuration;

namespace DbOperations
{
    public class DbPovezava
    {
        private MySqlConnection connection;
        private string server;
        private string databaseName;
        private string userName;
        private string password;

        public MySqlConnection Connection
        {
            get { return connection; }
        }

        public DbPovezava()
        {
            vzpostaviPovezavo();
        }

        public MySqlConnection vzpostaviPovezavo()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
            connection = new MySqlConnection(connectionString);

            return connection;
        }

        public bool odpriPovezavo()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        // MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //  MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        public bool zapriPovezavo()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
