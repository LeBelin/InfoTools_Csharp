using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace InfoTools
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
            database = "infotools";
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





        // ------------- Commerciaux -----------------------
        #region Commerciaux
        public static List<Commerciaux> SelectCommerciaux()
        {
            //Select statement
            string query = "SELECT * FROM commerciaux";

            //Create a list to store the result
            List<Commerciaux> dbCommerciaux = new List<Commerciaux>();

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
                    Commerciaux leCommercial = new Commerciaux(Convert.ToInt16(dataReader["idCommerciaux"]), Convert.ToString(dataReader["nomCommerciaux"]), Convert.ToString(dataReader["prenomCommerciaux"]), Convert.ToString(dataReader["adresseCommerciaux"]), Convert.ToString(dataReader["villeCommerciaux"]), Convert.ToString(dataReader["cpCommerciaux"]), Convert.ToString(dataReader["mailCommerciaux"]), Convert.ToString(dataReader["telCommerciaux"]));
                    dbCommerciaux.Add(leCommercial);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbCommerciaux;
            }
            else
            {
                return dbCommerciaux;
            }
        }


        public static void InsertCommerciaux(string nomCommerciaux, string prenomCommerciaux, string adrCommerciaux, string villeCommerciaux, string cpCommerciaux, string mailCommerciaux, string telCommerciaux)
        {
            // Update the query to match the number of values and columns
            string query = "INSERT INTO commerciaux (nomCommerciaux, prenomCommerciaux, adresseCommerciaux, villeCommerciaux, cpCommerciaux, mailCommerciaux, telCommerciaux) " +
                           "VALUES('" + nomCommerciaux + "','" + prenomCommerciaux + "','" + adrCommerciaux + "','" + villeCommerciaux + "','" + cpCommerciaux + "','" + mailCommerciaux + "','" + telCommerciaux + "')";
            Console.WriteLine(query);

            // Open the connection and execute the command
            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();  // Execute the query

                bdd.CloseConnection();  // Close the connection
            }
        }



        public static void UpdateCommerciaux(int idCommerciaux, string nomCommerciaux, string prenomCommerciaux, string adrCommerciaux, string villeCommerciaux, string cpCommerciaux, string mailCommerciaux, string telCommerciaux)
        {
            //Update Magazine
            string query = "UPDATE commerciaux SET nomCommerciaux='" + nomCommerciaux + "', prenomCommerciaux='" + prenomCommerciaux + "', adresseCommerciaux ='" + adrCommerciaux + "', villeCommerciaux ='" + villeCommerciaux + "', cpCommerciaux ='" + cpCommerciaux + "', mailCommerciaux ='" + mailCommerciaux + "', telCommerciaux ='" + telCommerciaux + "', idCommerciaux ='" + idCommerciaux + "'  WHERE idCommerciaux=" + idCommerciaux;
            Console.WriteLine(query);
            //Open connection
            if (bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                bdd.CloseConnection();
            }
        }




        public static void DeleteCommerciaux(int idCommerciaux)
        {
            //Delete Pigiste
            string query = "DELETE FROM commerciaux WHERE IdCommerciaux=" + idCommerciaux;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static Commerciaux SearchCommerciaux(int idCommerciaux)
        {
            Commerciaux leCommercial = null;
            string query = "SELECT * FROM commerciaux WHERE idCommerciaux = " + idCommerciaux;

            // Ouverture d'une nouvelle connexion
            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open(); // Nouvelle connexion
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
                    // Lecture des données
                    if (dataReaderS.Read())
                    {
                        leCommercial = new Commerciaux(
                            Convert.ToInt32(dataReaderS["idCommerciaux"]),
                            Convert.ToString(dataReaderS["nomCommerciaux"]),
                            Convert.ToString(dataReaderS["prenomCommerciaux"]),
                            Convert.ToString(dataReaderS["adresseCommerciaux"]),
                            Convert.ToString(dataReaderS["villeCommerciaux"]),
                            Convert.ToString(dataReaderS["cpCommerciaux"]),
                            Convert.ToString(dataReaderS["mailCommerciaux"]),
                            Convert.ToString(dataReaderS["telCommerciaux"]));
                    }
                }
            }
            return leCommercial;
        }

        #endregion

        // ------------- Prospect --------------------------
        #region Prospect
        public static List<Prospect> SelectProspect()
        {
            //Select statement
            string query = "SELECT * FROM prospects";

            //Create a list to store the result
            List<Prospect> dbProspect = new List<Prospect>();

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
                    Prospect leProspect = new Prospect(Convert.ToInt16(dataReader["idProspect"]), Convert.ToString(dataReader["nomProspect"]), Convert.ToString(dataReader["prenomProspect"]), Convert.ToString(dataReader["telephoneProspect"]), Convert.ToString(dataReader["emailProspect"]), Convert.ToString(dataReader["dateCreation"]), SearchCommerciaux(Convert.ToInt32(dataReader["idCommerciaux"])));
                    dbProspect.Add(leProspect);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbProspect;
            }
            else
            {
                return dbProspect;
            }
        }


        public static void InsertProspect(string nomProspect, string prenomProspect, string telephoneProspect, string emailProspect, string dateCreation, Commerciaux leCommercial)
        {
            string query = "INSERT INTO prospects (nomProspect, prenomProspect, telephoneProspect, emailProspect, dateCreation, idCommerciaux) " +
                           "VALUES('" + nomProspect + "', '" + prenomProspect + "', '" + telephoneProspect + "', '" + emailProspect + "', '" + dateCreation + "', " + leCommercial.IdCommerciaux + ")";
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }


        public static void UpdateProspect(int idProspect, string nomProspect, string prenomProspect, string telephoneProspect, string emailProspect, string dateCreation, int idCommerciaux)
        {
            string query = "UPDATE prospects SET nomProspect='" + nomProspect + "', prenomProspect='" + prenomProspect + "', telephoneProspect='" + telephoneProspect + "', emailProspect='" + emailProspect + "', dateCreation='" + dateCreation + "', idCommerciaux=" + idCommerciaux + " WHERE idProspect=" + idProspect;
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static void DeleteProspect(int idProspect)
        {
            //Delete Contrat
            string query = "DELETE FROM prospects WHERE idProspect=" + idProspect;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        // ------------- Client --------------------------
        #region Client
        public static List<Client> SelectClient()
        {
            //Select statement
            string query = "SELECT * FROM clients";

            //Create a list to store the result
            List<Client> dbClient = new List<Client>();

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
                    Client leClient = new Client(Convert.ToInt16(dataReader["idClient"]), Convert.ToString(dataReader["nomClient"]), Convert.ToString(dataReader["prenomClient"]), Convert.ToString(dataReader["emailClient"]), Convert.ToString(dataReader["telephoneClient"]), Convert.ToString(dataReader["adresseClient"]), Convert.ToString(dataReader["dateCreationClient"]), SearchCommerciaux(Convert.ToInt32(dataReader["idCommerciaux"])));
                    dbClient.Add(leClient);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbClient;
            }
            else
            {
                return dbClient;
            }
        }


        public static void InsertClient(string nomClient, string prenomClient, string emailClient, string telephoneClient, string adresseClient, string dateCreationClient, Commerciaux leCommercial)
        {
            string query = "INSERT INTO clients (nomClient, prenomClient, emailClient, telephoneClient, adresseClient, dateCreationClient, idCommerciaux) VALUES('" + nomClient + "','" + prenomClient + "','" + emailClient + "'," + telephoneClient + ",'" + adresseClient + "','" + dateCreationClient + "'," + leCommercial.IdCommerciaux + ")";
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static void UpdateClient(int idClient, string nomClient, string prenomClient, string emailClient, string telephoneClient, string adresseClient, string dateCreationClient, int idCommerciaux)
        {
            //Update Magazine
            string query = "UPDATE clients SET nomClient='" + nomClient + "', prenomClient='" + prenomClient + "', emailClient='" + emailClient + "', telephoneClient = " + telephoneClient + ", adresseClient='" + adresseClient + "', dateCreationClient='" + dateCreationClient + "', idCommerciaux=" + idCommerciaux + " WHERE idClient=" + idClient;
            Console.WriteLine(query);
            //Open connection
            if (bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                bdd.CloseConnection();
            }
        }
        public static void DeleteClient(int idClient)
        {
            string query = "DELETE FROM clients WHERE idClient=" + idClient;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static Client SearchClient(int idClient)
        {
            Client leClient = null;
            string query = "SELECT * FROM clients WHERE idClient = " + idClient;

            // Ouverture d'une nouvelle connexion
            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open(); // Nouvelle connexion
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
                    // Lecture des données
                    if (dataReaderS.Read())
                    {
                        leClient = new Client(
                            Convert.ToInt32(dataReaderS["idClient"]),
                            Convert.ToString(dataReaderS["nomClient"]),
                            Convert.ToString(dataReaderS["prenomClient"]),
                            Convert.ToString(dataReaderS["emailClient"]),
                            Convert.ToString(dataReaderS["telephoneClient"]),
                            Convert.ToString(dataReaderS["adresseClient"]),
                            Convert.ToString(dataReaderS["dateCreationClient"]),
                            SearchCommerciaux(Convert.ToInt32(dataReaderS["idCommerciaux"])));
                    }
                }
            }
            return leClient;
        }
        #endregion

        // ------------- Produit --------------------------
        #region Produit
        public static List<Produit> SelectProduit()
        {
            //Select statement
            string query = "SELECT * FROM produit";

            //Create a list to store the result
            List<Produit> dbProduit = new List<Produit>();

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
                    Produit leProduit = new Produit(Convert.ToInt16(dataReader["idProduit"]), Convert.ToString(dataReader["nomProduit"]), Convert.ToString(dataReader["desciptionProduit"]), Convert.ToInt16(dataReader["prixUnitaire"]), Convert.ToString(dataReader["dateAjoutProduit"]), Convert.ToString(dataReader["imgProduit"]));
                    dbProduit.Add(leProduit);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbProduit;
            }
            else
            {
                return dbProduit;
            }
        }


        public static void InsertProduit(string nomProduit, string desciptionProduit, int prixUnitaire, string dateAjoutProduit, string imgProduit)
        {
            // Update the query to match the number of values and columns
            string query = "INSERT INTO produit (nomProduit, desciptionProduit, prixUnitaire, dateAjoutProduit, imgProduit) " +
                           "VALUES('" + nomProduit + "','" + desciptionProduit + "','" + prixUnitaire + "','" + dateAjoutProduit + "','" + imgProduit + "')";
            Console.WriteLine(query);

            // Open the connection and execute the command
            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();  // Execute the query

                bdd.CloseConnection();  // Close the connection
            }
        }



        public static void UpdateProduit(int idProduit, string nomProduit, string desciptionProduit, int prixUnitaire, string dateAjoutProduit, string imgProduit)
        {
            //Update Magazine
            string query = "UPDATE produit SET nomProduit='" + nomProduit + "', desciptionProduit='" + desciptionProduit + "', prixUnitaire ='" + prixUnitaire + "', dateAjoutProduit ='" + dateAjoutProduit + "', imgProduit ='" + imgProduit + "', idProduit ='" + idProduit + "'  WHERE idProduit=" + idProduit;
            Console.WriteLine(query);
            //Open connection
            if (bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                bdd.CloseConnection();
            }
        }




        public static void DeleteProduit(int idProduit)
        {
            //Delete Pigiste
            string query = "DELETE FROM produit WHERE idProduit=" + idProduit;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static Produit SearchProduit(int idProduit)
        {
            Produit leProduit = null;
            string query = "SELECT * FROM produit WHERE idProduit = " + idProduit;

            // Ouverture d'une nouvelle connexion
            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open(); // Nouvelle connexion
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
                    // Lecture des données
                    if (dataReaderS.Read())
                    {
                        leProduit = new Produit(
                            Convert.ToInt32(dataReaderS["idProduit"]),
                            Convert.ToString(dataReaderS["nomProduit"]),
                            Convert.ToString(dataReaderS["desciptionProduit"]),
                            Convert.ToInt16(dataReaderS["prixUnitaire"]),
                            Convert.ToString(dataReaderS["dateAjoutProduit"]),
                            Convert.ToString(dataReaderS["imgProduit"]));
                    }
                }
            }
            return leProduit;
        }

        #endregion

        // ------------- Facture --------------------------
        #region Facture
        public static List<Facture> SelectFacture()
        {
            //Select statement
            string query = "SELECT * FROM factures";

            //Create a list to store the result
            List<Facture> dbFacture = new List<Facture>();

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
                    Facture laFacture = new Facture(
                        Convert.ToInt16(dataReader["idFacture"]), 
                        Convert.ToString(dataReader["dateFacture"]),
                        Convert.ToInt16(dataReader["montantTotal"]),
                        Convert.ToInt16(dataReader["statutFacture"]),
                        Convert.ToString(dataReader["datePaiment"]),
                        SearchProduit(Convert.ToInt32(dataReader["numProduit"])),
                        SearchClient(Convert.ToInt32(dataReader["idClient"])));
                    dbFacture.Add(laFacture);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbFacture;
            }
            else
            {
                return dbFacture;
            }
        }


        public static void InsertFacture(string dateFacture, int montantTotal, int statutFacture, string datePaiment, Produit leProduit, Client leClient)
        {
            string query = "INSERT INTO factures (dateFacture, montantTotal, statutFacture, datePaiment, numProduit, idClient) " +
                           "VALUES('" + dateFacture + "', '" + montantTotal + "', '" + statutFacture + "', '" + datePaiment + "', '" + leProduit.IdProduit + "', " + leClient.IdClient + ")";
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }


        public static void UpdateFacture(int idFacture, string dateFacture, int montantTotal, int statutFacture, string datePaiment, int numProduit, int idClient)
        {
            string query = "UPDATE factures SET dateFacture='" + dateFacture + "', montantTotal='" + montantTotal + "', statutFacture='" + statutFacture + "', datePaiment='" + datePaiment + "', numProduit='" + numProduit + "', idClient=" + idClient + " WHERE idFacture=" + idFacture;
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static void DeleteFacture(int idFacture)
        {
            string query = "DELETE FROM factures WHERE idFacture=" + idFacture;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion
    }
}
