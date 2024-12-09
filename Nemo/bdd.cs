using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Nemo
{
    internal class bdd
    {
        #region BDD
        public static MySqlConnection connection;
        public static string server;
        public static string database;
        public static string uid;
        public static string password;
        #region Initialize
        public static void Initialze()
        {
            server = "127.0.0.1";
            database = "Nemo";
            uid = "root";
            password = "";
            string connectionBDD;
            connectionBDD = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionBDD);
        }
        #endregion

        #region OpenConnection
        public static bool OpenConnection()
        {
            server = "localhost";
            database = "infotool";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            try
            {
                if (connection == null)
                {
                    connection = new MySqlConnection(connectionString);
                }

                // Vérifiez si la connexion est déjà ouverte
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur connexion BDD");
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server. Contact Administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        #endregion

        #region CloseConnection
        public static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
        #endregion

        #region Employer

        #region InsertEmployer
        public static void InsertEmployer(int idEmp, string nomEmp, string prenomEmp, string typeEmp, string mailEmp, decimal telEmp)
        {
            string query = "INSERT INTO Employee (idEmp, nomEmp, prenomEmp, typeEmp, mailEmp, telEmp) VALUES ('" + idEmp + "','" + nomEmp + "','" + prenomEmp + "','" + typeEmp + "','" + mailEmp + "','" + telEmp + "' )";
            Console.WriteLine(query);
            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        #region UpdateEmployer

        public static void UpdateEmployer(int idEmp, string nomEmp, string prenomEmp, string typeEmp, string mailEmp, decimal telEmp)
        {
            string query = "UPDATE Employee SET idEmp = '" + idEmp + "', nomEmp = '" + nomEmp + '", prenomEmp = "' + prenomEmp + "', typeEmp = '" + typeEmp + '", mailEmp = "' + mailEmp + '", telEmp ="' + telEmp;
            Console.WriteLine(query);
            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        #region DeleteEmployer
        public static void DeleteEmployer(int idEmp)
        {
            string query = "DELETE FROM Employee WHERE idEmp =" + idEmp;
            if (bdd.OpenConnection() == true)
                {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection(); 
                }
            #endregion

        #region SearchEmployer
        public static Employer SearchEmployer(int idEmp) 
        {
            string query = "SELECT * FROM Employee WHERE id=" + idEmp;
            List<Employer> dbEmployer = new List<Employer>();
            MySqlCommand cmdS = new MySqlCommand(query, connection);
            MySqlDataReader dataReaderS = cmdS.ExecuteReader();
            while (dataReaderS.Read()) 
            {
                Employer lemployer = new Employer(Convert.ToInt32(dataReaderS["idEmp"]), Convert.ToString(dataReaderS["nomEmp"]), Convert.ToString(dataReaderS["prenomEmp"]), Convert.ToString(dataReaderS["typeEmp"]), Convert.ToString(dataReaderS["mailEmp"]), Convert.ToDecimal(dataReaderS["telEmp"]));
                dbEmployer.Add(lemployer);
            }
            dataReaderS.Close();
            return dbEmployer[0];
        }
        #endregion

        #region SelectEmployer
        public static List<Employer> SelectEmployer(int idEmp) 
        {
            string query = "SELECT * FROM Employee";
            List<Employer> dbEmployer = new List<Employer>();
            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Employer lemployer = new Employer(Convert.ToInt32(dataReader["idEmp"]), Convert.ToString(dataReader["nomEmp"]), Convert.ToString(dataReader["prenomEmp"]), Convert.ToString(dataReader["typeEmp"]), Convert.ToString(dataReader["mailEmp"]), Convert.ToDecimal(dataReader["telEmp"]));
                    dbEmployer.Add(lemployer);
                }
                dataReader.Close();
                bdd.CloseConnection();
                return dbEmployer;
            }
            else
            {
                return dbEmployer;
            }
        }
        #endregion
        #endregion
    }
}