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
            database = "infotools-new";
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
                    Prospect leProspect = new Prospect(Convert.ToInt16(dataReader["id"]), Convert.ToString(dataReader["nom"]), Convert.ToString(dataReader["email"]), Convert.ToString(dataReader["telephone"]), Convert.ToString(dataReader["adresse"]));
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
        public static void InsertProspect(string nomProspect, string emailProspect, string telephoneProspect, string adresseProspect)
        {
            string query = "INSERT INTO prospects (nom, email, telephone, adresse) " +
                           "VALUES('" + nomProspect + "', '" + emailProspect + "', '" + telephoneProspect + "', '" + adresseProspect + "')";
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
        public static void UpdateProspect(int idProspect, string nomProspect, string emailProspect, string telephoneProspect, string adresseProspect)
        {
            string query = "UPDATE prospects SET nom='" + nomProspect +
                           "', email='" + emailProspect +
                           "', telephone='" + telephoneProspect +
                           "', adresse='" + adresseProspect +
                           "' WHERE id=" + idProspect;

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
            string query = "DELETE FROM prospects WHERE id=" + idProspect;

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
            string query = "SELECT * FROM prospects WHERE id = " + idProspect;

            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
                    if (dataReaderS.Read())
                    {
                        leProspect = new Prospect(
                            Convert.ToInt32(dataReaderS["id"]),
                            Convert.ToString(dataReaderS["nom"]),
                            Convert.ToString(dataReaderS["email"]),
                            Convert.ToString(dataReaderS["telephonet"]),
                            Convert.ToString(dataReaderS["adresse"]));
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
            string query = "SELECT id, nom, email, telephone, adresse FROM clients";

            List<Client> dbClient = new List<Client>();

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Client leClient = new Client(
                        Convert.ToInt32(dataReader["id"]),
                        dataReader["nom"].ToString(),
                        dataReader["email"].ToString(),
                        dataReader["telephone"].ToString(),
                        dataReader["adresse"].ToString()
                    );

                    dbClient.Add(leClient);
                }

                dataReader.Close();
                bdd.CloseConnection();
            }

            return dbClient;
        }
        #endregion


        // Ajouter un client
        #region Insert Client
        public static void InsertClient(string nomClient, string emailClient, string telephoneClient, string adresseClient)
        {
            string query = "INSERT INTO clients (nom, email, telephone, adresse) VALUES('"
                + nomClient + "','"
                + emailClient + "','"
                + telephoneClient + "','"
                + adresseClient + "')";
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
        public static void UpdateClient(int idClient, string nomClient, string emailClient, string telephoneClient, string adresseClient)
        {
            string query = "UPDATE clients SET nom='" + nomClient + "', email='" + emailClient + "', telephone='" + telephoneClient + "', adresse='" + adresseClient + "' WHERE id=" + idClient;
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
            string query = "DELETE FROM clients WHERE id=" + idClient;

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
            string query = "SELECT * FROM clients WHERE id = " + idClient;

            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
                    if (dataReaderS.Read())
                    {
                        leClient = new Client(
                            Convert.ToInt32(dataReaderS["id"]),
                            Convert.ToString(dataReaderS["nom"]),
                            Convert.ToString(dataReaderS["email"]),
                            Convert.ToString(dataReaderS["telephone"]),
                            Convert.ToString(dataReaderS["adresse"]));
                    }
                }
            }
            return leClient;
        }
        #endregion

        #endregion

        // ------------- Commerciaux -----------------------
        #region Commerciaux

        // Selection d'un commercial
        #region Select Commerciaux
        public static List<Commerciaux> SelectCommercials()
        {
            string query = "SELECT * FROM commercials";

            List<Commerciaux> dbCommerciaux = new List<Commerciaux>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Commerciaux leCommercial = new Commerciaux(Convert.ToInt16(dataReader["id"]), Convert.ToString(dataReader["nom"]), Convert.ToString(dataReader["email"]), Convert.ToString(dataReader["telephone"]), Convert.ToString(dataReader["adresse"]));
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
        public static void InsertCommerciaux(string nomCommerciaux, string mailCommerciaux, string telCommerciaux, string adrCommerciaux)
        {
            string query = "INSERT INTO commercials (nom, email, telephone, adresse) " +
                           "VALUES('" + nomCommerciaux + "','" + mailCommerciaux + "','" + telCommerciaux + "','" + adrCommerciaux + "')";
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
        public static void UpdateCommerciaux(int idCommerciaux, string nomCommerciaux, string mailCommerciaux, string telCommerciaux, string adrCommerciaux)
        {
            string query = "UPDATE commercials SET nom='" + nomCommerciaux + "', email='" + mailCommerciaux + "', telephone ='" + telCommerciaux + "', adresse ='" + adrCommerciaux + "', id ='" + idCommerciaux + "'  WHERE id=" + idCommerciaux;
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
            string query = "DELETE FROM commercials WHERE Id=" + idCommerciaux;

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
            string query = "SELECT * FROM commercials WHERE id = " + idCommerciaux;

            using (MySqlConnection tempConnection = new MySqlConnection(connection.ConnectionString))
            {
                tempConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, tempConnection))
                using (MySqlDataReader dataReaderS = cmd.ExecuteReader())
                {
                    if (dataReaderS.Read())
                    {
                        leCommercial = new Commerciaux(
                            Convert.ToInt32(dataReaderS["id"]),
                            Convert.ToString(dataReaderS["nom"]),
                            Convert.ToString(dataReaderS["email"]),
                            Convert.ToString(dataReaderS["telephone"]),
                            Convert.ToString(dataReaderS["adresse"]));
                    }
                }
            }
            return leCommercial;
        }
        #endregion

        #endregion

        // ------------- Produit ---------------------------
        #region Produit

        // Selection d'un produit
        #region Select Produit
        public static List<Produit> SelectProduit()
        {
            string query = "SELECT * FROM produits";

            List<Produit> dbProduit = new List<Produit>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Produit leProduit = new Produit(Convert.ToInt16(dataReader["id"]), Convert.ToString(dataReader["nom_produit"]), Convert.ToString(dataReader["description"]), Convert.ToInt16(dataReader["prix"]), Convert.ToInt16(dataReader["stock"]));
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
        public static void InsertProduit(string nom_produit, string description, int prix, int stock)
        {
            string query = "INSERT INTO produits (nom_produit, description, prix, stock) " +
                           "VALUES('" + nom_produit + "','" 
                                      + description + "','" 
                                      + prix + "','" 
                                      + stock + "')";
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
        public static void UpdateProduit(int id, string nom_produit, string description, int prix, int stock)
        {
            string query = "UPDATE produits SET nom_produit='" + nom_produit + "', description='" + description + "', prix ='" + prix + "', stock ='" + stock + "', id ='" + id + "'  WHERE id =" + id;
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
            string query = "DELETE FROM produits WHERE id=" + idProduit;

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
                            Convert.ToInt32(dataReaderS["id"]),
                            Convert.ToString(dataReaderS["nom_produit"]),
                            Convert.ToString(dataReaderS["description"]),
                            Convert.ToInt16(dataReaderS["prix"]),
                            Convert.ToInt16(dataReaderS["stock"]));
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
            string query = "SELECT * FROM rendez_vous";

            List<RendezVous> dbRendezVous = new List<RendezVous>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    RendezVous leRendezVous = new RendezVous(Convert.ToInt16(dataReader["id"]), SearchClient(Convert.ToInt32(dataReader["client_id"])), SearchCommerciaux(Convert.ToInt32(dataReader["commercial_id"])), Convert.ToString(dataReader["date_rendez_vous"]), (TimeSpan)dataReader["heure_rendez_vous"], Convert.ToString(dataReader["description"]));
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
        public static void InsertRendezVous(Client leClient, Commerciaux leCommercial, string date_rendez_vous, string heure_rendez_vous, string description)
        {
            // Conversion du format de date
            DateTime date = DateTime.ParseExact(date_rendez_vous, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string dateFormatSql = date.ToString("yyyy-MM-dd");

            string query = "INSERT INTO rendez_vous (client_id, commercial_id, date_rendez_vous, heure_rendez_vous, description) " +
                           "VALUES('" + leClient.IdClient + "','"
                                      + leCommercial.IdCommerciaux + "','"
                                      + dateFormatSql + "','"
                                      + heure_rendez_vous + "','"
                                      + description + "')";

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
        public static void UpdateRendezVous(int id, int clientId, int commercialId, string date_rendez_vous, string heure_rendez_vous, string description)
        {
            // Conversion du format de date
            DateTime date = DateTime.ParseExact(date_rendez_vous, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string dateFormatSql = date.ToString("yyyy-MM-dd");

            string query = "UPDATE rendez_vous SET " +
                           "client_id = @clientId, " +
                           "commercial_id = @commercialId, " +
                           "date_rendez_vous = @dateRv, " +
                           "heure_rendez_vous = @heureRv, " +
                           "description = @desc " +
                           "WHERE id = @id";

            Console.WriteLine(query);

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@clientId", clientId);
                cmd.Parameters.AddWithValue("@commercialId", commercialId);
                cmd.Parameters.AddWithValue("@dateRv", dateFormatSql);
                cmd.Parameters.AddWithValue("@heureRv", heure_rendez_vous);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        #endregion

        //#region Update RendezVous
        //public static void UpdateRendezVous(int id, Client leClient, Commerciaux leCommercial, string date_rendez_vous, string heure_rendez_vous, string description)
        //{
        //    string query = "UPDATE rendez_vous SET client_id='" + leClient + "', commercial_id='" + leCommercial + "', date_rendez_vous ='" + date_rendez_vous + "', heure_rendez_vous ='" + heure_rendez_vous + "', description ='" + description + "' , id ='" + id + "'  WHERE id =" + id;
        //    Console.WriteLine(query);
        //    if (bdd.OpenConnection() == true)
        //    {
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandText = query;
        //        cmd.Connection = connection;

        //        cmd.ExecuteNonQuery();
        //        bdd.CloseConnection();
        //    }
        //}
        //#endregion

        // Suprimmer un rendez vous
        #region Delete RendezVous
        public static void DeleteRendezVous(int idRendezVous)
        {
            string query = "DELETE FROM rendez_vous WHERE id=" + idRendezVous;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion

        #endregion

        // ------------- Commande ---------------------------
        #region Commande

        // Selection d'une facture
        #region Select Commande
        public static List<Commande> SelectCommande()
        {
            string query = "SELECT * FROM commandes";

            List<Commande> dbCommande = new List<Commande>();

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Commande laCommande = new Commande(
                        Convert.ToInt16(dataReader["id"]),
                        SearchClient(Convert.ToInt32(dataReader["client_id"])),
                        Convert.ToInt32(dataReader["montant_total"]),
                        Convert.ToString(dataReader["created_at"]));
                    dbCommande.Add(laCommande);
                }

                dataReader.Close();
                bdd.CloseConnection();
                return dbCommande;
            }
            else
            {
                return dbCommande;
            }
        }
        #endregion

        // Ajouter une Comande
        #region Insert Commande
        public static void InsertCommande(Client leClient, int montant_total)
        {
            string query = "INSERT INTO commandes (client_id, montant_total) " +
                           "VALUES('" + leClient.IdClient + "', '" 
                                      + montant_total + ")";
            Console.WriteLine(query);

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static void InsertCommandeAvecProduits(Commande commande)
        {
            if (!bdd.OpenConnection()) return;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;

            // 1. Insérer la commande
            cmd.CommandText = "INSERT INTO commandes (client_id, montant_total, created_at) VALUES (@client, @total, NOW()); SELECT LAST_INSERT_ID();";
            cmd.Parameters.AddWithValue("@client", commande.LeClient.IdClient);
            cmd.Parameters.AddWithValue("@total", commande.MontantTotal);

            int idCommande;
            using (var reader = cmd.ExecuteReader())
            {
                reader.Read();
                idCommande = Convert.ToInt32(reader[0]);
            }

            // 2. Insérer les produits liés
            foreach (var ligne in commande.ProduitsCommande)
            {
                cmd = new MySqlCommand("INSERT INTO commande_produit (commande_id, produit_id, quantite, prix_unitaire, created_at) VALUES (@idCommande, @idProduit, @quantite, @prix, NOW());", connection);
                cmd.Parameters.AddWithValue("@idCommande", idCommande);
                cmd.Parameters.AddWithValue("@idProduit", ligne.IdProduit);
                cmd.Parameters.AddWithValue("@quantite", ligne.Quantite);
                cmd.Parameters.AddWithValue("@prix", ligne.PrixUnitaire);
                cmd.ExecuteNonQuery();
            }

            bdd.CloseConnection();
        }
        public static void AjouterProduitDansCommande(int commandeId, int produitId, int quantite, decimal prixUnitaire)
        {
            string query = @"INSERT INTO commande_produit (commande_id, produit_id, quantite, prix_unitaire, created_at)
                     VALUES (@commandeId, @produitId, @quantite, @prix, NOW())";

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@commandeId", commandeId);
                cmd.Parameters.AddWithValue("@produitId", produitId);
                cmd.Parameters.AddWithValue("@quantite", quantite);
                cmd.Parameters.AddWithValue("@prix", prixUnitaire);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static void SupprimerProduitDansCommande(int commandeId, int produitId)
        {
            string query = @"DELETE FROM commande_produit
                     WHERE commande_id = @commandeId AND produit_id = @produitId";

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@commandeId", commandeId);
                cmd.Parameters.AddWithValue("@produitId", produitId);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }


        public static List<LigneCommande> GetProduitsCommande(int commandeId)
        {
            List<LigneCommande> produits = new List<LigneCommande>();

            string query = @"SELECT cp.produit_id, p.nom_produit, cp.quantite, cp.prix_unitaire
                     FROM commande_produit cp
                     JOIN produits p ON p.id = cp.produit_id
                     WHERE cp.commande_id = @commandeId";

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@commandeId", commandeId);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    produits.Add(new LigneCommande(
                        Convert.ToInt32(reader["produit_id"]),
                        reader["nom_produit"].ToString(),
                        Convert.ToInt32(reader["quantite"]),
                        Convert.ToDecimal(reader["prix_unitaire"])
                    ));
                }

                reader.Close();
                bdd.CloseConnection();
            }

            return produits;
        }

        public static List<ProduitSelectionne> GetProduitsParCommande(int commandeId)
        {
            List<ProduitSelectionne> produits = new List<ProduitSelectionne>();
            string query = "SELECT produit_id, quantite, prix_unitaire FROM commande_produit WHERE commande_id = @commandeId";

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@commandeId", commandeId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    produits.Add(new ProduitSelectionne
                    {
                        IdProduit = Convert.ToInt32(reader["produit_id"]),
                        Quantite = Convert.ToInt32(reader["quantite"]),
                        Prix = Convert.ToDecimal(reader["prix_unitaire"])
                    });
                }

                reader.Close();
                bdd.CloseConnection();
            }

            return produits;
        }



        #endregion

        // Mise a jours de la Comamnde
        #region Update Commande
        public static void UpdateCommande(int id, int clientId, decimal montant_total)
        {
            string query = "UPDATE commandes SET " +
                           "client_id = '" + clientId + "', " +
                           "montant_total = '" + montant_total + "' " +
                           "WHERE id = " + id;

            Console.WriteLine(query);

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }
        #endregion


        // Suprimmer commande
        #region Delete Commande
        public static void DeleteCommande(int idCommande)
        {
            string query = "DELETE FROM commandes WHERE id=" + idCommande;

            if (bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static void SupprimerProduitsCommande(int commandeId)
        {
            string query = "DELETE FROM commande_produit WHERE commande_id = @commandeId";

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@commandeId", commandeId);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        #endregion

        // Calculer le montant total de la commande
        #region Montant Total
        public static decimal CalculerMontantTotalCommande(int commandeId)
        {
            decimal total = 0;
            string query = @"SELECT quantite, prix_unitaire FROM commande_produit WHERE commande_id = @commandeId";

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@commandeId", commandeId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int qte = Convert.ToInt32(reader["quantite"]);
                    decimal prix = Convert.ToDecimal(reader["prix_unitaire"]);
                    total += qte * prix;
                }

                reader.Close();
                bdd.CloseConnection();
            }

            return total;
        }
        public static void UpdateMontantCommande(int commandeId, decimal montant)
        {
            string query = "UPDATE commandes SET montant_total = @montant WHERE id = @id";

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@montant", montant);
                cmd.Parameters.AddWithValue("@id", commandeId);
                cmd.ExecuteNonQuery();
                bdd.CloseConnection();
            }
        }

        public static int InsertCommandeEtRetourneId(int clientId, decimal montantTotal)
        {
            string query = "INSERT INTO commandes (client_id, montant_total, created_at) VALUES (@clientId, @montant, NOW()); SELECT LAST_INSERT_ID();";
            int idCommande = 0;

            if (bdd.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@clientId", clientId);
                cmd.Parameters.AddWithValue("@montant", montantTotal);
                idCommande = Convert.ToInt32(cmd.ExecuteScalar());
                bdd.CloseConnection();
            }

            return idCommande;
        }

        #endregion

        #endregion

    }
}
