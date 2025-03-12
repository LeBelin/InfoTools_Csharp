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
        // --- Connection et fermeture de la bdd ---
        #region BDD
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
        #endregion


        // ------------- Commerciaux -----------------------
        #region Commerciaux

        // Selection d'un commercial
        #region Select Commerciaux
        public static List<Commerciaux> SelectCommerciaux()
        {
            string query = "SELECT * FROM commerciaux";

            List<Commerciaux> dbCommerciaux = new List<Commerciaux>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Commerciaux leCommercial = new Commerciaux(Convert.ToInt16(dataReader["idCommerciaux"]), Convert.ToString(dataReader["nomCommerciaux"]), Convert.ToString(dataReader["prenomCommerciaux"]), Convert.ToString(dataReader["adresseCommerciaux"]), Convert.ToString(dataReader["villeCommerciaux"]), Convert.ToString(dataReader["cpCommerciaux"]), Convert.ToString(dataReader["mailCommerciaux"]), Convert.ToString(dataReader["telCommerciaux"]));
                    dbCommerciaux.Add(leCommercial);
                }

                dataReader.Close();

                bdd.CloseConnection();
                return dbCommerciaux;
            }
            else
            {
                return dbCommerciaux;
            }
        }
        #endregion

        // Ajouter un commercial
        #region Insert Commerciaux
        public static void InsertCommerciaux(string nomCommerciaux, string prenomCommerciaux, string adrCommerciaux, string villeCommerciaux, string cpCommerciaux, string mailCommerciaux, string telCommerciaux)
        {
            string query = "INSERT INTO commerciaux (nomCommerciaux, prenomCommerciaux, adresseCommerciaux, villeCommerciaux, cpCommerciaux, mailCommerciaux, telCommerciaux) " +
                           "VALUES('" + nomCommerciaux + "','" + prenomCommerciaux + "','" + adrCommerciaux + "','" + villeCommerciaux + "','" + cpCommerciaux + "','" + mailCommerciaux + "','" + telCommerciaux + "')";
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                bdd.CloseConnection();
            }
        }
        #endregion

        // Mis a jour d'un commercial
        #region Update Commerciaux
        public static void UpdateCommerciaux(int idCommerciaux, string nomCommerciaux, string prenomCommerciaux, string adrCommerciaux, string villeCommerciaux, string cpCommerciaux, string mailCommerciaux, string telCommerciaux)
        {
            string query = "UPDATE commerciaux SET nomCommerciaux='" + nomCommerciaux + "', prenomCommerciaux='" + prenomCommerciaux + "', adresseCommerciaux ='" + adrCommerciaux + "', villeCommerciaux ='" + villeCommerciaux + "', cpCommerciaux ='" + cpCommerciaux + "', mailCommerciaux ='" + mailCommerciaux + "', telCommerciaux ='" + telCommerciaux + "', idCommerciaux ='" + idCommerciaux + "'  WHERE idCommerciaux=" + idCommerciaux;
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

        // Suprimmer un commercial
        #region Delete Commerciaux
        public static void DeleteCommerciaux(int idCommerciaux)
        {
            string query = "DELETE FROM commerciaux WHERE IdCommerciaux=" + idCommerciaux;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        // Rechercher un commercial
        #region Search Commerciaux
        public static Commerciaux SearchCommerciaux(int idCommerciaux)
        {
            Commerciaux leCommercial = null;
            string query = "SELECT * FROM commerciaux WHERE idCommerciaux = " + idCommerciaux;

            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
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

        #endregion

        // ------------- Prospect --------------------------
        #region Prospect

        // Selection d'un Prospect
        #region Select Prospect
        public static List<Prospect> SelectProspect()
        {
            string query = "SELECT * FROM prospects";

            List<Prospect> dbProspect = new List<Prospect>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Prospect leProspect = new Prospect(Convert.ToInt16(dataReader["idProspect"]), Convert.ToString(dataReader["nomProspect"]), Convert.ToString(dataReader["prenomProspect"]), Convert.ToString(dataReader["telephoneProspect"]), Convert.ToString(dataReader["emailProspect"]), Convert.ToString(dataReader["dateCreation"]), SearchCommerciaux(Convert.ToInt32(dataReader["idCommerciaux"])));
                    dbProspect.Add(leProspect);
                }

                dataReader.Close();
                bdd.CloseConnection();

                return dbProspect;
            }
            else
            {
                return dbProspect;
            }
        }
        #endregion

        // Ajouter un Prospect
        #region Insert Prospect
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
        #endregion

        // Mis a jour d'un Prospect
        #region Update Prospect
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
        #endregion

        // Suprimmer un Prospect
        #region Delete Prospect
        public static void DeleteProspect(int idProspect)
        {
            string query = "DELETE FROM prospects WHERE idProspect=" + idProspect;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        // Rechercher un prospect
        #region Search Pospect
        public static Prospect SearchProspect(int idProspect)
        {
            Prospect leProspect = null;
            string query = "SELECT * FROM prospects WHERE idProspect = " + idProspect;

            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
                    if (dataReaderS.Read())
                    {
                        leProspect = new Prospect(
                            Convert.ToInt32(dataReaderS["idProspect"]),
                            Convert.ToString(dataReaderS["nomProspect"]),
                            Convert.ToString(dataReaderS["prenomProspect"]),
                            Convert.ToString(dataReaderS["telephoneProspect"]),
                            Convert.ToString(dataReaderS["emailProspect"]),
                            Convert.ToString(dataReaderS["dateCreation"]),
                            SearchCommerciaux(Convert.ToInt32(dataReaderS["idCommerciaux"])));
                    }
                }
            }
            return leProspect;
        }
        #endregion

        #endregion

        // ------------- Client ----------------------------
        #region Client

        // Selection d'un client
        #region Select Client
        public static List<Client> SelectClient()
        {
            string query = "SELECT * FROM clients";

            List<Client> dbClient = new List<Client>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Client leClient = new Client(Convert.ToInt16(dataReader["idClient"]), Convert.ToString(dataReader["nomClient"]), Convert.ToString(dataReader["prenomClient"]), Convert.ToString(dataReader["emailClient"]), Convert.ToString(dataReader["telephoneClient"]), Convert.ToString(dataReader["adresseClient"]), Convert.ToString(dataReader["dateCreationClient"]), SearchCommerciaux(Convert.ToInt32(dataReader["idCommerciaux"])));
                    dbClient.Add(leClient);
                }

                dataReader.Close();
                bdd.CloseConnection();
                return dbClient;
            }
            else
            {
                return dbClient;
            }
        }
        #endregion

        // Ajouter un client
        #region Insert Client
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
        #endregion

        // Mis a jour d'un client
        #region Update Client
        public static void UpdateClient(int idClient, string nomClient, string prenomClient, string emailClient, string telephoneClient, string adresseClient, string dateCreationClient, int idCommerciaux)
        {
            string query = "UPDATE clients SET nomClient='" + nomClient + "', prenomClient='" + prenomClient + "', emailClient='" + emailClient + "', telephoneClient = " + telephoneClient + ", adresseClient='" + adresseClient + "', dateCreationClient='" + dateCreationClient + "', idCommerciaux=" + idCommerciaux + " WHERE idClient=" + idClient;
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

        // Suprimmer un client
        #region Delete Client
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
        #endregion

        // Rechercher un client
        #region Search Client
        public static Client SearchClient(int idClient)
        {
            Client leClient = null;
            string query = "SELECT * FROM clients WHERE idClient = " + idClient;

            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
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

        #endregion

        // ------------- Produit ---------------------------
        #region Produit

        // Selection d'un produit
        #region Select Produit
        public static List<Produit> SelectProduit()
        {
            string query = "SELECT * FROM produit";

            List<Produit> dbProduit = new List<Produit>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Produit leProduit = new Produit(Convert.ToInt16(dataReader["idProduit"]), Convert.ToString(dataReader["nomProduit"]), Convert.ToString(dataReader["desciptionProduit"]), Convert.ToInt16(dataReader["prixUnitaire"]), Convert.ToString(dataReader["dateAjoutProduit"]), Convert.ToString(dataReader["imgProduit"]));
                    dbProduit.Add(leProduit);
                }

                dataReader.Close();
                bdd.CloseConnection();
                return dbProduit;
            }
            else
            {
                return dbProduit;
            }
        }
        #endregion

        // Ajouter un produit
        #region Insert Produit
        public static void InsertProduit(string nomProduit, string desciptionProduit, int prixUnitaire, string dateAjoutProduit, string imgProduit)
        {
            string query = "INSERT INTO produit (nomProduit, desciptionProduit, prixUnitaire, dateAjoutProduit, imgProduit) " +
                           "VALUES('" + nomProduit + "','" 
                                      + desciptionProduit + "','" 
                                      + prixUnitaire + "','" 
                                      + dateAjoutProduit + "','" 
                                      + imgProduit + "')";
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();  
                bdd.CloseConnection(); 
            }
        }
        #endregion

        // Mis a jour d'un produit
        #region Update Produit
        public static void UpdateProduit(int idProduit, string nomProduit, string desciptionProduit, int prixUnitaire, string dateAjoutProduit, string imgProduit)
        {
            string query = "UPDATE produit SET nomProduit='" + nomProduit + "', desciptionProduit='" + desciptionProduit + "', prixUnitaire ='" + prixUnitaire + "', dateAjoutProduit ='" + dateAjoutProduit + "', imgProduit ='" + imgProduit + "', idProduit ='" + idProduit + "'  WHERE idProduit=" + idProduit;
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

        // Suprimmer un prodit
        #region Delete Produit
        public static void DeleteProduit(int idProduit)
        {
            string query = "DELETE FROM produit WHERE idProduit=" + idProduit;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        // Rechercher un produit
        #region Search Produit
        public static Produit SearchProduit(int idProduit)
        {
            Produit leProduit = null;
            string query = "SELECT * FROM produit WHERE idProduit = " + idProduit;

            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
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

        #endregion

        // ------------- RendezVous ------------------------
        #region RendezVous

        // Selection d'un RendezVous
        #region Select RendezVous
        public static List<RendezVous> SelectRendezVous()
        {
            string query = "SELECT * FROM rendezvous";

            List<RendezVous> dbRendezVous = new List<RendezVous>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    RendezVous leRendezVous = new RendezVous(Convert.ToInt16(dataReader["idRendezVous"]), Convert.ToString(dataReader["dateRendezVous"]), Convert.ToString(dataReader["descriptionRendezVous"]), (TimeSpan)dataReader["heureDebutRendezVous"], (TimeSpan)dataReader["heureFinRendezVous"], SearchCommerciaux(Convert.ToInt32(dataReader["idCommerciaux"])), SearchProspect(Convert.ToInt32(dataReader["idProspect"])), SearchClient(Convert.ToInt32(dataReader["idClient"])));
                    dbRendezVous.Add(leRendezVous);
                }

                dataReader.Close();
                bdd.CloseConnection();
                return dbRendezVous;
            }
            else
            {
                return dbRendezVous;
            }
        }
        #endregion

        // Ajout d'un Rendez Vous
        #region Insert RendezVous
        public static void InsertRendezVous(string dateRendezVous, string descriptionRendezVous, string heureDebutRendezVous, string heureFinRendezVous, Commerciaux leCommercial, Prospect leProspect, Client leClient)
        {
            string query = "INSERT INTO rendezvous (dateRendezVous, descriptionRendezVous, heureDebutRendezVous, heureFinRendezVous, idCommerciaux, idProspect, idClient) " +
                           "VALUES('" + dateRendezVous + "','" 
                                      + descriptionRendezVous + "','" 
                                      + heureDebutRendezVous + "','" 
                                      + heureFinRendezVous + "','" 
                                      + leCommercial.IdCommerciaux + "','" 
                                      + leProspect.IdProspect + "','" 
                                      + leClient.IdClient + "')";
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        // Mis a jour d'un Rendez vous
        #region Update RendezVous
        public static void UpdateRendezVous(int idRendezVous, string dateRendezVous, string descriptionRendezVous, string heureDebutRendezVous, string heureFinRendezVous, int idCommerciaux, int idProspect, int idClient)
        {
            string query = "UPDATE rendezvous SET dateRendezVous='" + dateRendezVous + "', descriptionRendezVous='" + descriptionRendezVous + "', heureDebutRendezVous ='" + heureDebutRendezVous + "', heureFinRendezVous ='" + heureFinRendezVous + "', idCommerciaux ='" + idCommerciaux + "', idProspect ='" + idProspect + "', idClient ='" + idClient + "', idRendezVous ='" + idRendezVous + "'  WHERE idRendezVous=" + idRendezVous;
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

        // Suprimmer un rendez vous
        #region Delete RendezVous
        public static void DeleteRendezVous(int idRendezVous)
        {
            string query = "DELETE FROM rendezvous WHERE idRendezVous=" + idRendezVous;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        #endregion

        // ------------- Facture ---------------------------
        #region Facture

        // Selection d'une facture
        #region Select Facture
        public static List<Facture> SelectFacture()
        {
            string query = "SELECT * FROM factures";

            List<Facture> dbFacture = new List<Facture>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Facture laFacture = new Facture(
                        Convert.ToInt16(dataReader["idFacture"]), 
                        Convert.ToString(dataReader["dateFacture"]),
                        Convert.ToInt32(dataReader["montantTotal"]),
                        Convert.ToInt16(dataReader["statutFacture"]),
                        Convert.ToString(dataReader["datePaiment"]),
                        SearchProduit(Convert.ToInt32(dataReader["numProduit"])),
                        SearchClient(Convert.ToInt32(dataReader["idClient"])));
                    dbFacture.Add(laFacture);
                }

                dataReader.Close();
                bdd.CloseConnection();
                return dbFacture;
            }
            else
            {
                return dbFacture;
            }
        }
        #endregion

        // Ajouter une Facture
        #region Insert Facture
        public static void InsertFacture(string dateFacture, int montantTotal, int statutFacture, string datePaiment, Produit leProduit, Client leClient)
        {
            string query = "INSERT INTO factures (dateFacture, montantTotal, statutFacture, datePaiment, numProduit, idClient) " +
                           "VALUES('" + dateFacture + "', '" 
                                      + montantTotal + "', '" 
                                      + statutFacture + "', '" 
                                      + datePaiment + "', '" 
                                      + leProduit.IdProduit + "', " 
                                      + leClient.IdClient + ")";
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        // Mise a jours de la facture
        #region Update Facture
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
        #endregion

        // Suprimmer facture
        #region Delete Facture
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

        #endregion

    }
}
