using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Text.RegularExpressions;

namespace InfoTools
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Commerciaux> lesCommerciaux = new List<Commerciaux>();
        List<Prospect> lesProspect = new List<Prospect>();
        List<Client> lesClients = new List<Client>();
        List<Produit> lesProduit = new List<Produit>();
        List<RendezVous> lesRendezVous = new List<RendezVous>();
        List<Commande> lesCommandes = new List<Commande>();

        public MainWindow()
        {
            bdd.Initialize();
            InitializeComponent();

            lesCommerciaux = bdd.SelectCommercials();
            lesProspect = bdd.SelectProspect();
            lesClients = bdd.SelectClient();
            lesProduit = bdd.SelectProduit();
            lesRendezVous = bdd.SelectRendezVous();
            lesCommandes = bdd.SelectCommande();

            //Lie le Datagrid dtgMagazine avec la collection cMagazine
            dtgCommerciaux.ItemsSource = lesCommerciaux;
            dtgProspect.ItemsSource = lesProspect;
            dtgClient.ItemsSource = lesClients;
            dtgProduit.ItemsSource = lesProduit;
            dtgRendezVous.ItemsSource = lesRendezVous;
            dtgCommande.ItemsSource = lesCommandes;

            dtgCommerciaux.SelectedIndex = 0;
            dtgProspect.SelectedIndex = 0;
            dtgClient.SelectedIndex = 0;
            dtgProduit.SelectedIndex = 0;
            dtgRendezVous.SelectedIndex = 0;
            dtgCommande.SelectedIndex = 0;

            //

            cboClientFacture.ItemsSource = lesClients;
            cboClientFacture.DisplayMemberPath = "NomClient"; // Affiche le nom du client
            cboClientFacture.SelectedValuePath = "IdClient";

            //
            cboCommerciauxRendezVous.ItemsSource = lesCommerciaux;
            cboCommerciauxRendezVous.DisplayMemberPath = "NomCommerciaux";
            cboCommerciauxRendezVous.SelectedValuePath = "IdCommerciaux";

            //cboCommerciauxRendezVous.DisplayMemberPath = "NomProspect";


            cboClientRendezVous.ItemsSource = lesClients;
            //cboCommerciauxRendezVous.DisplayMemberPath = "NomClient";
            cboClientRendezVous.SelectedValuePath = "IdClient";

        }


        // ------------- Prospect -----------------
        #region Prospect

        // Affichage Prospect
        #region DataGrid Prospect
        private void dtgProspect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            Prospect selectedProspect = dtgProspect.SelectedItem as Prospect;
            if (dtgProspect.SelectedItem != null)
            {
                try
                {
                    //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat
                    txtNumProspect.Text = Convert.ToString(selectedProspect.IdProspect);
                    txtNomProspect.Text = selectedProspect.NomProspect;
                    txtTelProspect.Text = Convert.ToString(selectedProspect.TelephoneProspect);
                    txtAdresseProspect.Text = selectedProspect.AdresseProspect;
                    txtMailProspect.Text = Convert.ToString(selectedProspect.EmailProspect);
                    //cboProspectCommercial.SelectedIndex = selectedProspect.LeCommercial;

                    // Sélection du pigiste concerné dans la Combobox
                    //cboPigiste.SelectedItem = selectedContrat.PigisteContrat;
                }
                catch (Exception)
                {
                    Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
                }
            }
        }
        #endregion

        // Ajouter un Prospect
        #region BTN Ajouter Prospect
        private void btnAjouterProspect_Click(object sender, RoutedEventArgs e)
        {
            Prospect tmpProspect = new Prospect(0, txtNomProspect.Text, txtMailProspect.Text, txtTelProspect.Text, txtMailProspect.Text);
            bdd.InsertProspect(tmpProspect.NomProspect, tmpProspect.EmailProspect, tmpProspect.TelephoneProspect, tmpProspect.EmailProspect);

            // Mets a jours pigistes
            lesProspect = bdd.SelectProspect();

            // Met à jour le DataGrid
            dtgProspect.ItemsSource = lesProspect;
            dtgProspect.SelectedIndex = 0;
            dtgProspect.Items.Refresh();
            dtgRendezVous.Items.Refresh();

            // lesContrats.Add(tmpContrat);
        }
        #endregion

        // Modifier un Prospect
        #region BTN Modifier un prospect
        private void btnModifierProspect_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            int indice = lesProspect.IndexOf((Prospect)dtgProspect.SelectedItem);

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            lesProspect.ElementAt(indice).NomProspect = txtNomProspect.Text;
            lesProspect.ElementAt(indice).EmailProspect = txtMailProspect.Text;
            lesProspect.ElementAt(indice).TelephoneProspect = txtTelProspect.Text;
            lesProspect.ElementAt(indice).AdresseProspect = txtAdresseProspect.Text;

            Prospect prospectModifie = lesProspect.ElementAt(indice);
            bdd.UpdateProspect(
                prospectModifie.IdProspect,
                prospectModifie.NomProspect,
                prospectModifie.EmailProspect,
                prospectModifie.TelephoneProspect,
                prospectModifie.AdresseProspect
            );

            dtgProspect.Items.Refresh();
            dtgRendezVous.Items.Refresh();
        }
        #endregion

        // Suprimmer un Prospect
        #region Suprimmer Prospect
        private void btnSupprimerProspect_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProspect.SelectedIndex >= 0)
            {
                bdd.DeleteProspect(Convert.ToInt16(txtNumProspect.Text));

                // Actualise magazines dans l'application
                lesProspect = bdd.SelectProspect();

                dtgProspect.ItemsSource = lesProspect;
                dtgProspect.SelectedIndex = 0;

                dtgProspect.Items.Refresh();
                dtgRendezVous.Items.Refresh();
            }
        }
        #endregion

        #endregion


        // ------------- Client -------------------
        #region Client

        // Affichage des clients
        #region DataGrid Client
        private void dtgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client selectedClient = dtgClient.SelectedItem as Client;

            if (selectedClient != null) // Vérifier si un client est sélectionné
            {
                try
                {
                    txtIdClient.Text = Convert.ToString(selectedClient.IdClient);
                    txtNomClient.Text = selectedClient.NomClient;
                    txtEmailClient.Text = Convert.ToString(selectedClient.EmailClient);
                    txtTelClient.Text = Convert.ToString(selectedClient.TelephoneClient);
                    txtAdresseClient.Text = Convert.ToString(selectedClient.AdresseClient);
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Erreur sur la mise à jour du formulaire : " + ex.Message);
                }
            }

        }
        #endregion

        // Ajouter un client
        #region BTN Ajouter un client
        private void btnAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            Client tmpCLient = new Client(0, txtNomClient.Text, txtEmailClient.Text, txtTelClient.Text, txtAdresseClient.Text);
            bdd.InsertClient(tmpCLient.NomClient, tmpCLient.EmailClient, tmpCLient.TelephoneClient, tmpCLient.AdresseClient);

            lesClients = bdd.SelectClient();

            dtgClient.ItemsSource = lesClients;
            dtgClient.SelectedIndex = 0;
            dtgClient.Items.Refresh();
            dtgCommande.Items.Refresh();
            cboClientFacture.Items.Refresh();
            dtgRendezVous.Items.Refresh();
        }
        #endregion

        // Modifier un client
        #region BTN Modifier client
        private void btnModifierClient_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            int indice = lesClients.IndexOf((Client)dtgClient.SelectedItem);

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            lesClients.ElementAt(indice).NomClient = txtNomClient.Text;
            lesClients.ElementAt(indice).EmailClient = txtEmailClient.Text;
            lesClients.ElementAt(indice).TelephoneClient = txtTelClient.Text;
            lesClients.ElementAt(indice).AdresseClient = txtAdresseClient.Text;

            Client clientModifie = lesClients.ElementAt(indice);
            bdd.UpdateClient(
                clientModifie.IdClient,
                clientModifie.NomClient,
                clientModifie.EmailClient,
                clientModifie.TelephoneClient,
                clientModifie.AdresseClient
            );

            dtgClient.Items.Refresh();
            dtgCommande.Items.Refresh();
            cboClientFacture.Items.Refresh();
            dtgRendezVous.Items.Refresh();
        }
        #endregion

        // Suprimmer un client
        #region BTN Suprimmer client
        private void btnSupprimerClient_Click(object sender, RoutedEventArgs e)
        {
            if (dtgClient.SelectedIndex >= 0)
            {
                bdd.DeleteClient(Convert.ToInt16(txtIdClient.Text));

                // Actualise magazines dans l'application
                lesClients = bdd.SelectClient();

                dtgClient.ItemsSource = lesClients;
                dtgClient.SelectedIndex = 0;
                dtgClient.Items.Refresh();
                dtgCommande.Items.Refresh();
                cboClientFacture.Items.Refresh();
                dtgRendezVous.Items.Refresh();
            }
        }
        #endregion

        #endregion


        // ------------- Commerciaux --------------
        #region Commerciaux

        // Affichage d'un commercial
        #region DataGrid Commerciaux
        private void dtgCommerciaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgCommerciaux.SelectedItem != null)
            {
                Commerciaux selectedCommerciaux = dtgCommerciaux.SelectedItem as Commerciaux;

                if (selectedCommerciaux != null)
                {
                    txtNumCommercial.Text = Convert.ToString(selectedCommerciaux.IdCommerciaux);
                    txtNomCommercial.Text = selectedCommerciaux.NomCommerciaux;
                    txtMailCommercial.Text = selectedCommerciaux.MailCommerciaux;
                    txtAdresseCommercial.Text = selectedCommerciaux.AdresseCommerciaux;
                    txtTelCommercial.Text = selectedCommerciaux.TelCommerciaux;
                }
            }
        }
        #endregion

        // Ajouter un commercial
        #region BTN Ajouter Commerciaux
        private void btnAjouterCommerciaux_Click(object sender, RoutedEventArgs e)
        {
            Commerciaux tmpCommerciaux = new Commerciaux(0, txtNomCommercial.Text, txtMailCommercial.Text, txtTelCommercial.Text, txtAdresseCommercial.Text);
            bdd.InsertCommerciaux(tmpCommerciaux.NomCommerciaux, tmpCommerciaux.MailCommerciaux, tmpCommerciaux.TelCommerciaux, tmpCommerciaux.AdresseCommerciaux);
            
            lesCommerciaux = bdd.SelectCommercials();

            dtgCommerciaux.ItemsSource = lesCommerciaux;
            dtgCommerciaux.SelectedIndex = 0;
            dtgCommerciaux.Items.Refresh();
                        
            dtgProspect.Items.Refresh();
            dtgClient.Items.Refresh();
            dtgRendezVous.Items.Refresh();
            cboCommerciauxRendezVous.Items.Refresh();
        }
        #endregion

        // Modifier un commercial
        #region BTN Modifier commerciaux
        private void btnModifierCommerciaux_Click(object sender, RoutedEventArgs e)
        {
            int indice = lesCommerciaux.IndexOf((Commerciaux)dtgCommerciaux.SelectedItem);

            if (dtgCommerciaux.SelectedIndex >= 0)
            {
                bdd.UpdateCommerciaux(Convert.ToInt32(txtNumCommercial.Text), txtNomCommercial.Text, txtMailCommercial.Text, txtTelCommercial.Text, txtAdresseClient.Text);

                // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
                lesCommerciaux.ElementAt(indice).NomCommerciaux = txtNomCommercial.Text;
                lesCommerciaux.ElementAt(indice).MailCommerciaux = txtMailCommercial.Text;
                lesCommerciaux.ElementAt(indice).TelCommerciaux = txtTelCommercial.Text;
                lesCommerciaux.ElementAt(indice).AdresseCommerciaux = txtAdresseCommercial.Text;
            }

            dtgCommerciaux.Items.Refresh();
            dtgProspect.Items.Refresh();
            dtgClient.Items.Refresh();
            dtgRendezVous.Items.Refresh();
            cboCommerciauxRendezVous.Items.Refresh();

        }
        #endregion

        // Suprimmer un commercial
        #region BTN Suprimmer Commerciaux
        private void btnSupprimerCommerciaux_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCommerciaux.SelectedItem != null)
            {
                Commerciaux selectedCommerciaux = dtgCommerciaux.SelectedItem as Commerciaux;

                if (selectedCommerciaux != null)
                {
                    bdd.DeleteCommerciaux(selectedCommerciaux.IdCommerciaux);
                    lesCommerciaux.Remove(selectedCommerciaux);

                    dtgCommerciaux.Items.Refresh();
                    dtgProspect.Items.Refresh();
                    dtgClient.Items.Refresh();
                    dtgRendezVous.Items.Refresh();
                    cboCommerciauxRendezVous.Items.Refresh();
                }
                dtgCommerciaux.SelectedIndex = 0;
            }
        }
        #endregion

        #endregion


        // ------------- Produit ------------------
        #region Produit

        //Affiche des produits
        #region DataGrid Produit
        private void dtgProduit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgProduit.SelectedItem != null)
            {
                Produit selectedProduit = dtgProduit.SelectedItem as Produit;

                if (selectedProduit != null)
                {
                    txtIdProduit.Text = Convert.ToString(selectedProduit.IdProduit);
                    txtNomProduit.Text = selectedProduit.NomProduit;
                    txtDescProduit.Text = selectedProduit.DesciptionProduit;
                    txtPrixUnitaire.Text = Convert.ToString(selectedProduit.PrixUnitaire);
                    txtStockProduit.Text = Convert.ToString(selectedProduit.StockProduit);
                }
            }
        }
        #endregion

        // Ajouter un produit
        #region BTN Ajouter Produit
        private void btnAjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProduit.SelectedIndex >= 0)
            {
                Produit tmpProduit = new Produit(0, txtNomProduit.Text, txtDescProduit.Text, Convert.ToInt16(txtPrixUnitaire.Text), Convert.ToInt16(txtStockProduit.Text));
                bdd.InsertProduit(tmpProduit.NomProduit, tmpProduit.DesciptionProduit, tmpProduit.PrixUnitaire, tmpProduit.StockProduit);

                lesProduit = bdd.SelectProduit();

                dtgProduit.ItemsSource = lesProduit;
                dtgProduit.SelectedIndex = 0;
                dtgProduit.Items.Refresh();
                dtgCommande.Items.Refresh();
            }
        }
        #endregion

        // Modifier un Produit
        #region Modifier Produit
        private void btnModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            int indice = lesProduit.IndexOf((Produit)dtgProduit.SelectedItem);

            if (dtgProduit.SelectedIndex >= 0)
            {
                bdd.UpdateProduit(
                    Convert.ToInt32(txtIdProduit.Text),
                    txtNomProduit.Text,
                    txtDescProduit.Text,
                    Convert.ToInt16(txtPrixUnitaire.Text),
                    Convert.ToInt16(txtStockProduit.Text)
                );


                lesProduit.ElementAt(indice).NomProduit = txtNomProduit.Text;
                lesProduit.ElementAt(indice).DesciptionProduit = txtDescProduit.Text;
                lesProduit.ElementAt(indice).PrixUnitaire = Convert.ToInt16(txtPrixUnitaire.Text);
                lesProduit.ElementAt(indice).StockProduit = Convert.ToInt16(txtStockProduit.Text);
            }

            dtgProduit.Items.Refresh();
            dtgCommande.Items.Refresh();
        }
        #endregion

        // Suprimmer un produit
        #region Suprimmer produit
        private void btnSupprimerProduit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProduit.SelectedItem != null)
            {
                Produit selectedProduit = dtgProduit.SelectedItem as Produit;

                if (selectedProduit != null)
                {
                    bdd.DeleteProduit(selectedProduit.IdProduit);
                    lesProduit.Remove(selectedProduit);

                    dtgProduit.Items.Refresh();
                    dtgCommande.Items.Refresh();
                }
                dtgProduit.SelectedIndex = 0;
            }
        }
        #endregion

        #endregion


        // ------------- RendezVous ---------------
        #region RendezVous

        #region Format de l'heure a afficher dans la datagrid
        private string FormatTimeSpanAs1h00(TimeSpan time)
        {
            return $"{time.Hours}h{time.Minutes:00}";
        }
        #endregion

        #region Fomat de l'heure lors de l'ajout
        private TimeSpan ConvertToTimeSpan(string time)
        {
            string formattedTime = time.Replace("h", ":") + ":00";
            return TimeSpan.Parse(formattedTime);
        }
        #endregion

        // Datagrid rendezVous
        #region Datagrid RendezVous
        private void dtgRendezVous_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RendezVous selectedRendezVous = dtgRendezVous.SelectedItem as RendezVous;
            if (dtgRendezVous.SelectedItem != null)
            {
                try
                {
                    txIdRendezVous.Text = Convert.ToString(selectedRendezVous.IdRendezVous);

                    // Mettre à jour la sélection du client dans le ComboBox
                    if (selectedRendezVous.LeClient != null)
                    {
                        cboClientRendezVous.SelectedValue = selectedRendezVous.LeClient.IdClient;
                    }

                    // Mettre à jour la sélection du rendezvous dans le ComboBox
                    if (selectedRendezVous.LeCommercial != null)
                    {
                        cboCommerciauxRendezVous.SelectedValue = selectedRendezVous.LeCommercial.IdCommerciaux;
                    }

                    dtpRendezVous.Text = selectedRendezVous.DateRendezVous;
                    txtDescRendezVous.Text = Convert.ToString(selectedRendezVous.DescriptionRendezVous);
                    txtHeureRendezVous.Text = selectedRendezVous.HeureRendezVous.ToString(@"hh\:mm");
                    //cboRendezVous.Text = FormatTimeSpanAs1h00(selectedRendezVous.HeureRendezVous);


                    int i = 0;
                    bool trouve = false;

                    while (i < cboCommerciauxRendezVous.Items.Count && trouve == false)
                    {
                        if (Convert.ToString(cboCommerciauxRendezVous.Items[i]) == Convert.ToString(selectedRendezVous.LeCommercial))
                        {
                            trouve = true;
                            cboCommerciauxRendezVous.SelectedIndex = i;
                        }
                        i++;
                    }


                    while (i < cboClientRendezVous.Items.Count && trouve == false)
                    {
                        if (Convert.ToString(cboClientRendezVous.Items[i]) == Convert.ToString(selectedRendezVous.LeClient))
                        {
                            trouve = true;
                            cboClientRendezVous.SelectedIndex = i;
                        }
                        i++;
                    }


                }
                catch (Exception)
                {

                    Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
                }
            }
        }
        #endregion

        // Ajouter un rendez vous
        #region BTN Ajouter RendezVous
        private void btnAjouterRendezVous_Click(object sender, RoutedEventArgs e)
        {
            Commerciaux ModifCommerciauxRendezVous = cboCommerciauxRendezVous.SelectedItem as Commerciaux;
            Client ModifClientRendezVous = cboClientRendezVous.SelectedItem as Client;

            // Vérification de l'heure saisie
            TimeSpan heureRdv;
            if (!TimeSpan.TryParse(txtHeureRendezVous.Text, out heureRdv))
            {
                MessageBox.Show("Format d'heure invalide. Exemple : 14:30", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RendezVous tmpRendezVous = new RendezVous(
                0,
                ModifClientRendezVous,
                ModifCommerciauxRendezVous,
                dtpRendezVous.Text,
                heureRdv,
                txtDescRendezVous.Text
            );

            // Insertion en base
            bdd.InsertRendezVous(
                tmpRendezVous.LeClient,
                tmpRendezVous.LeCommercial,
                tmpRendezVous.DateRendezVous,
                tmpRendezVous.HeureRendezVous.ToString(@"hh\:mm"),
                tmpRendezVous.DescriptionRendezVous
            );

            // Rafraîchissement
            lesRendezVous = bdd.SelectRendezVous();
            dtgRendezVous.ItemsSource = lesRendezVous;
            dtgRendezVous.SelectedIndex = 0;
            dtgRendezVous.Items.Refresh();
        }
        private void txtHeureRendezVous_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Autoriser chiffres et ':'
            e.Handled = !Regex.IsMatch(e.Text, @"[\d:]");
        }

        #endregion

        #region BTN Modifier RendezVous
        private void btnModifierRendezVous_Click(object sender, RoutedEventArgs e)
        {
            int indice = lesRendezVous.IndexOf((RendezVous)dtgRendezVous.SelectedItem);

            if (indice < 0)
                return;

            // Vérification de l'heure saisie
            TimeSpan heureRdv;
            if (!TimeSpan.TryParse(txtHeureRendezVous.Text, out heureRdv))
            {
                MessageBox.Show("Format d'heure invalide. Exemple : 14:30", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RendezVous rdv = lesRendezVous[indice];
            rdv.LeClient = (Client)cboClientRendezVous.SelectedItem;
            rdv.LeCommercial = (Commerciaux)cboCommerciauxRendezVous.SelectedItem;
            rdv.DateRendezVous = dtpRendezVous.Text;
            rdv.HeureRendezVous = heureRdv;

            //if (TimeSpan.TryParse(cboRendezVouss.Text, out TimeSpan heure))
            //{
            //    rdv.HeureRendezVous = heure;
            //}
            //else
            //{
            //    MessageBox.Show("Format d'heure invalide, veuillez entrer une heure au format HH:mm.");
            //    return;
            //}

            rdv.DescriptionRendezVous = txtDescRendezVous.Text;

            bdd.UpdateRendezVous(
                rdv.IdRendezVous,
                rdv.LeClient.IdClient,
                rdv.LeCommercial.IdCommerciaux,
                rdv.DateRendezVous,
                rdv.HeureRendezVous.ToString(@"hh\:mm"),
                rdv.DescriptionRendezVous
            );

            dtgRendezVous.Items.Refresh();
        }

        #endregion


        // Suprimmer un Rendez Vous
        #region Suprimmer RendezVous
        private void btnSupprimerRendezVous_Click(object sender, RoutedEventArgs e)
        {
            if (dtgRendezVous.SelectedIndex >= 0)
            {
                bdd.DeleteRendezVous(Convert.ToInt16(txIdRendezVous.Text));

                lesRendezVous = bdd.SelectRendezVous();

                dtgRendezVous.ItemsSource = lesRendezVous;
                dtgRendezVous.SelectedIndex = 0;
            }
        }
        #endregion

        #endregion


        // ------------- Commande ------------------
        #region Commande

        // Affichage dans la datagrid Commande
        #region Affichage Commande
        private void dtgFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgCommande.SelectedItem is Commande commandeSelectionnee)
            {
                txtIdFacture.Text = commandeSelectionnee.IdCommande.ToString();
                txtMontantTotal.Text = commandeSelectionnee.MontantTotal.ToString();
                dateCommande.SelectedDate = DateTime.Parse(commandeSelectionnee.DateCommande);
                // Utiliser SelectedValue avec Id
                cboClientFacture.SelectedValue = commandeSelectionnee.LeClient.IdClient;

                // Charger et afficher les produits de la commande
                List<LigneCommande> produits = bdd.GetProduitsCommande(commandeSelectionnee.IdCommande);
                dtgProduitsCommande.ItemsSource = produits;
            }
        }

        #endregion

        // Ajouter une Facture
        #region BTN Ajouter une facture
        private void btnAjouterFacture_Click(object sender, RoutedEventArgs e)
        {
            var client = (Client)cboClientFacture.SelectedItem;
            if (client == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.");
                return;
            }

            var tousProduits = bdd.SelectProduit(); // méthode à créer si pas déjà fait

            var fenetreProduits = new FenetreProduitsCommande(tousProduits);
            if (fenetreProduits.ShowDialog() == true)
            {
                var produitsChoisis = fenetreProduits.ProduitsSelectionnes;

                if (!produitsChoisis.Any())
                {
                    MessageBox.Show("Aucun produit sélectionné.");
                    return;
                }

                decimal montantTotal = produitsChoisis.Sum(p => p.Prix * p.Quantite);

                int idCommande = bdd.InsertCommandeEtRetourneId(client.IdClient, montantTotal);

                foreach (var p in produitsChoisis)
                {
                    bdd.AjouterProduitDansCommande(idCommande, p.IdProduit, p.Quantite, p.Prix);
                }

                MessageBox.Show("Commande ajoutée avec succès.");
                dtgCommande.ItemsSource = bdd.SelectCommande();
            }
        }




        private void btnSupprimerProduitCommande_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCommande.SelectedItem is Commande commande &&
                dtgProduitsCommande.SelectedItem is LigneCommande ligne)
            {
                bdd.SupprimerProduitDansCommande(commande.IdCommande, ligne.IdProduit);

                // Refresh
                dtgProduitsCommande.ItemsSource = bdd.GetProduitsCommande(commande.IdCommande);
            }
        }


        private void dtgCommande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgCommande.SelectedItem is Commande commandeSelectionnee)
            {
                txtIdFacture.Text = commandeSelectionnee.IdCommande.ToString();
                txtMontantTotal.Text = commandeSelectionnee.MontantTotal.ToString();
                dateCommande.SelectedDate = DateTime.Parse(commandeSelectionnee.DateCommande);
                cboClientFacture.SelectedItem = commandeSelectionnee.LeClient;

                //  Charger les produits liés à la commande
                var produitsCommande = bdd.GetProduitsCommande(commandeSelectionnee.IdCommande);
                dtgProduitsCommande.ItemsSource = produitsCommande;
            }
        }

        #endregion

        // Modifier une facture
        #region BTN Modifier Facture
        private void btnModifierFacture_Click(object sender, RoutedEventArgs e)
        {
            int indice = lesCommandes.IndexOf((Commande)dtgCommande.SelectedItem);
            if (indice < 0) return;

            var commande = lesCommandes[indice];
            var client = (Client)cboClientFacture.SelectedItem;
            if (client == null) return;

            // Charger les produits disponibles et ceux liés à la commande
            var produitsDispo = bdd.SelectProduit();
            var produitsCommande = bdd.GetProduitsParCommande(commande.IdCommande);

            // Injecter les quantités actuelles
            var produitsAvecQuantite = produitsDispo.Select(p =>
            {
                var existant = produitsCommande.FirstOrDefault(cp => cp.IdProduit == p.IdProduit);
                return new ProduitSelectionne
                {
                    IdProduit = p.IdProduit,
                    NomProduit = p.NomProduit,
                    Prix = p.PrixUnitaire,
                    Quantite = existant?.Quantite ?? 0
                };
            }).ToList();

            // Ouvrir la fenêtre
            var fenetreProduits = new FenetreProduitsCommande(produitsAvecQuantite);
            if (fenetreProduits.ShowDialog() == true)
            {
                var produitsSelectionnes = fenetreProduits.ProduitsSelectionnes;
                if (produitsSelectionnes.Count == 0)
                {
                    MessageBox.Show("Aucun produit sélectionné.");
                    return;
                }

                decimal montantTotal = produitsSelectionnes.Sum(p => p.Prix * p.Quantite);

                // 1. Mettre à jour la commande
                bdd.UpdateCommande(commande.IdCommande, client.IdClient, montantTotal);

                // 2. Supprimer les anciens produits de la commande
                bdd.SupprimerProduitsCommande(commande.IdCommande);

                // 3. Ajouter les nouveaux
                foreach (var p in produitsSelectionnes)
                {
                    bdd.AjouterProduitDansCommande(commande.IdCommande, p.IdProduit, p.Quantite, p.Prix);
                }

                MessageBox.Show("Commande modifiée.");
                dtgCommande.ItemsSource = bdd.SelectCommande();
            }
        }

        #endregion

        // Suprimmer une facture
        #region BTN Suprimmer Facture
        private void btnSupprimerFacture_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCommande.SelectedIndex >= 0)
            {
                bdd.DeleteCommande(Convert.ToInt16(txtIdFacture.Text));

                lesCommandes = bdd.SelectCommande();

                dtgCommande.ItemsSource = lesCommandes;
                dtgCommande.SelectedIndex = 0;
            }
        }
        #endregion

        // Exporter une facture
        #region BTN Exporter Facture
        private void btnExporterFacture_Click(object sender, RoutedEventArgs e)
        {
            // Vérifiez si une facture est sélectionnée dans le DataGrid
            Commande selectedFacture = dtgCommande.SelectedItem as Commande;

            if (selectedFacture == null)
            {
                MessageBox.Show("Veuillez sélectionner une facture pour l'exporter.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Générer un nom de fichier basé sur le numéro et la date de la facture
                string sanitizedDate = selectedFacture.DateCommande.Replace("/", "-").Replace("\\", "-");
                string factureFileName = $"Facture_{selectedFacture.IdCommande}_{sanitizedDate}.pdf";
                string exportPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), factureFileName);

                // Créez un document PDF
                PdfDocument pdfDoc = new PdfDocument();
                PdfPage page = pdfDoc.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Définir la police
                XFont font = new XFont("Verdana", 12);

                // Ajouter un titre centré
                gfx.DrawString("Facture", font, XBrushes.Black, new XRect(0, 40, page.Width, page.Height), XStringFormats.Center);

                // Ajouter les informations de la facture
                gfx.DrawString($"Numéro de Facture: {selectedFacture.IdCommande}", font, XBrushes.Black, 40, 80);
                gfx.DrawString($"Date de Facture: {selectedFacture.LeClient.IdClient}", font, XBrushes.Black, 40, 110);
                gfx.DrawString($"Montant Total: {selectedFacture.MontantTotal} €", font, XBrushes.Black, 40, 140);

                // Ajouter les informations du produit
                //if (selectedFacture.LeProduit != null)
                //{
                //    gfx.DrawString($"Produit: {selectedFacture.LeProduit.NomProduit}", font, XBrushes.Black, 40, 230);
                //}

                // Ajouter les informations du client
                if (selectedFacture.LeClient != null)
                {
                    gfx.DrawString($"Client: {selectedFacture.LeClient.NomClient}", font, XBrushes.Black, 40, 260);
                }

                // Sauvegarder le document PDF sur le disque
                pdfDoc.Save(exportPath);

                // Message de confirmation
                MessageBox.Show($"Facture exportée avec succès !\nChemin: {exportPath}", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'exportation : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        // Voir le graphique des factures
        #region Graphique des factures
        private void btnGraphiqueFacture_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les factures depuis la base de données (ou DataGrid)
            List<Commande> commandes = bdd.SelectCommande();

            // Créer et ouvrir la fenêtre du graphique
            GraphiqueWindow graphiqueWindow = new GraphiqueWindow(commandes);
            graphiqueWindow.Show();
        }
        #endregion

        #endregion


        // BTN Actualiser
        #region BTN Actualiser
        private void btnActualliser_Click(object sender, RoutedEventArgs e)
        {
            // Rafraîchissez les données depuis la base de données
            List<Commerciaux> commerciaux = bdd.SelectCommercials(); // Exemple : récupérer les commerciaux depuis la base de données
            List<Prospect> prospects = bdd.SelectProspect(); // Exemple : récupérer les prospects depuis la base de données
            List<Client> clients = bdd.SelectClient(); // Exemple : récupérer les clients depuis la base de données
            List<RendezVous> rendezVous = bdd.SelectRendezVous(); // Exemple : récupérer les rendez-vous depuis la base de données
            List<Commande> factures = bdd.SelectCommande(); // Exemple : récupérer les factures depuis la base de données
            List<Produit> produits = bdd.SelectProduit(); // Exemple : récupérer les produits depuis la base de données

            // Actualisez les DataGrids avec les nouvelles données
            dtgCommerciaux.ItemsSource = commerciaux;
            dtgProspect.ItemsSource = prospects;
            dtgClient.ItemsSource = clients;
            dtgRendezVous.ItemsSource = rendezVous;
            dtgCommande.ItemsSource = factures;
            dtgProduit.ItemsSource = produits;

            // Actualisez les ComboBox avec les nouvelles données
            cboCommerciauxRendezVous.ItemsSource = commerciaux;
            cboClientRendezVous.ItemsSource = clients;
            cboClientFacture.ItemsSource = clients;

            // Rafraîchissez les éléments de l'interface graphique
            dtgCommerciaux.Items.Refresh();
            dtgProspect.Items.Refresh();
            dtgClient.Items.Refresh();
            dtgRendezVous.Items.Refresh();
            dtgCommande.Items.Refresh();
            dtgProduit.Items.Refresh();

            cboCommerciauxRendezVous.Items.Refresh();
            cboClientRendezVous.Items.Refresh();
            cboClientFacture.Items.Refresh();

            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            // Ferme la fenêtre actuelle
            this.Close();
        }
        #endregion

    }
}
