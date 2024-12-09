using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Nemo
{
    public class bdd
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;



        //Initialisation des valeurs
        public static void Initialize()
        {
            server = "localhost";
            database = "nemo";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                Console.WriteLine("Erreur connexion BDD");
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
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
        public static MySqlConnection Connection
        {
            get { return connection; }
        }


        // ------------- Employer -----------------------
        #region Employer

        public static List<Employer> SelectEmployer()
        {
            //Select statement
            string query = "SELECT * FROM Employee";

            //Create a list to store the result
            List<Employer> dbEmp = new List<Employer>();

            //Ouverture connection
            if (bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Employer leEmp = new Employer(Convert.ToInt16(dataReader["idEmp"]), Convert.ToString(dataReader["nomEmp"]), Convert.ToString(dataReader["prenomEmp"]), Convert.ToString(dataReader["typeEmp"]), Convert.ToString(dataReader["mailEmp"]), Convert.ToDecimal(dataReader["telEmp"]));
                    dbEmp.Add(leEmp);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbEmp;
            }
            else
            {
                return dbEmp;
            }
        }


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
            string query = "UPDATE Employee SET nomEmp='" + nomEmp + "', prenomEmp='" + prenomEmp + "', typeEmp ='" + typeEmp + "', mailEmp ='" + mailEmp + "', telEmp ='" + telEmp + "'  WHERE idEmp=" + idEmp;
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
        }


        #endregion
    }
}

