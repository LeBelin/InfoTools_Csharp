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
        List<Facture> lesFactures = new List<Facture>();

        public MainWindow()
        {
            bdd.Initialize();
            InitializeComponent();

            lesCommerciaux = bdd.SelectCommerciaux();
            lesProspect = bdd.SelectProspect();
            lesClients = bdd.SelectClient();
            lesProduit = bdd.SelectProduit();
            lesRendezVous = bdd.SelectRendezVous();
            lesFactures = bdd.SelectFacture();

            //Lie le Datagrid dtgMagazine avec la collection cMagazine
            dtgCommerciaux.ItemsSource = lesCommerciaux;
            dtgProspect.ItemsSource = lesProspect;
            dtgClient.ItemsSource = lesClients;
            dtgProduit.ItemsSource = lesProduit;
            dtgRendezVous.ItemsSource = lesRendezVous;
            dtgFacture.ItemsSource = lesFactures;

            dtgCommerciaux.SelectedIndex = 0;
            dtgProspect.SelectedIndex = 0;
            dtgClient.SelectedIndex = 0;
            dtgProduit.SelectedIndex = 0;
            dtgRendezVous.SelectedIndex = 0;
            dtgFacture.SelectedIndex = 0;

            cboProspectCommercial.ItemsSource = lesCommerciaux;
            cboCommercialClient.ItemsSource = lesCommerciaux;
            //
            cboProduitFacture.ItemsSource = lesProduit;
            cboProduitFacture.DisplayMemberPath = "NomProduit"; // Affiche le nom du produit
            cboProduitFacture.SelectedValuePath = "IdProduit";

            cboClientFacture.ItemsSource = lesClients;
            cboClientFacture.DisplayMemberPath = "NomClient"; // Affiche le nom du client
            cboClientFacture.SelectedValuePath = "IdClient";

            //
            cboCommerciauxRendezVous.ItemsSource = lesCommerciaux;
            cboCommerciauxRendezVous.DisplayMemberPath = "NomCommerciaux";
            cboCommerciauxRendezVous.SelectedValuePath = "IdCommerciaux";

            cboProspectRendezVous.ItemsSource = lesProspect;
            //cboCommerciauxRendezVous.DisplayMemberPath = "NomProspect";
            cboProspectRendezVous.SelectedValuePath = "IdProspect";

            cboClientRendezVous.ItemsSource = lesClients;
            //cboCommerciauxRendezVous.DisplayMemberPath = "NomClient";
            cboClientRendezVous.SelectedValuePath = "IdClient";

        }


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
                    txtPrenomCommercial.Text = selectedCommerciaux.PrenomCommerciaux;
                    txtAdresseCommercial.Text = selectedCommerciaux.AdresseCommerciaux;
                    txtCPCommercial.Text = selectedCommerciaux.CpCommerciaux;
                    txtVilleCommercial.Text = selectedCommerciaux.VilleCommerciaux;
                    txtTelCommercial.Text = selectedCommerciaux.TelCommerciaux;
                    txtMailCommercial.Text = selectedCommerciaux.MailCommerciaux;
                }
            }
        }
        #endregion

        // Ajouter un commercial
        #region BTN Ajouter Commerciaux
        private void btnAjouterCommerciaux_Click(object sender, RoutedEventArgs e)
        {
            Commerciaux tmpCommerciaux = new Commerciaux(0, txtNomCommercial.Text, txtPrenomCommercial.Text, txtAdresseCommercial.Text, txtVilleCommercial.Text, txtCPCommercial.Text, txtMailCommercial.Text, txtTelCommercial.Text);
            bdd.InsertCommerciaux(tmpCommerciaux.NomCommerciaux, tmpCommerciaux.PrenomCommerciaux, tmpCommerciaux.AdresseCommerciaux, tmpCommerciaux.VilleCommerciaux, tmpCommerciaux.CpCommerciaux, tmpCommerciaux.MailCommerciaux, tmpCommerciaux.TelCommerciaux);
            
            lesCommerciaux = bdd.SelectCommerciaux();

            dtgCommerciaux.ItemsSource = lesCommerciaux;
            dtgCommerciaux.SelectedIndex = 0;
            dtgCommerciaux.Items.Refresh();
                        
            dtgProspect.Items.Refresh();
            cboProspectCommercial.Items.Refresh();
            dtgClient.Items.Refresh();
            cboCommercialClient.Items.Refresh();
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
                bdd.UpdateCommerciaux(Convert.ToInt32(txtNumCommercial.Text), txtNomCommercial.Text, txtPrenomCommercial.Text, txtAdresseCommercial.Text, txtVilleCommercial.Text, txtCPCommercial.Text, txtMailCommercial.Text, txtTelCommercial.Text);

                // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
                lesCommerciaux.ElementAt(indice).NomCommerciaux = txtNomCommercial.Text;
                lesCommerciaux.ElementAt(indice).PrenomCommerciaux = txtPrenomCommercial.Text;
                lesCommerciaux.ElementAt(indice).AdresseCommerciaux = txtAdresseCommercial.Text;
                lesCommerciaux.ElementAt(indice).VilleCommerciaux = txtVilleCommercial.Text;
                lesCommerciaux.ElementAt(indice).CpCommerciaux = txtCPCommercial.Text;
                lesCommerciaux.ElementAt(indice).TelCommerciaux = txtTelCommercial.Text;
                lesCommerciaux.ElementAt(indice).MailCommerciaux = txtMailCommercial.Text;
            }

            dtgCommerciaux.Items.Refresh();
            dtgProspect.Items.Refresh();
            cboProspectCommercial.Items.Refresh();
            dtgClient.Items.Refresh();
            cboCommercialClient.Items.Refresh();
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
                    cboProspectCommercial.Items.Refresh();
                    dtgClient.Items.Refresh();
                    cboCommercialClient.Items.Refresh();
                    dtgRendezVous.Items.Refresh();
                    cboCommerciauxRendezVous.Items.Refresh();
                }
                dtgCommerciaux.SelectedIndex = 0;
            }
        }
        #endregion

        #endregion


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
                    txtPrenomProspect.Text = Convert.ToString(selectedProspect.PrenomProspect);
                    txtTelProspect.Text = Convert.ToString(selectedProspect.TelephoneProspect);
                    txtMailProspect.Text = Convert.ToString(selectedProspect.EmailProspect);
                    dtpCeationProspect.Text = Convert.ToString(selectedProspect.DateCreation);
                    //cboProspectCommercial.SelectedIndex = selectedProspect.LeCommercial;
                    cboProspectCommercial.SelectedValue = selectedProspect.LeCommercial.IdCommerciaux;

                    // Sélection du pigiste concerné dans la Combobox
                    //cboPigiste.SelectedItem = selectedContrat.PigisteContrat;

                    int i = 0;
                    bool trouve = false;

                    while (i < cboProspectCommercial.Items.Count && trouve == false)
                    {
                        if (Convert.ToString(cboProspectCommercial.Items[i]) == Convert.ToString(selectedProspect.LeCommercial))
                        {
                            trouve = true;
                            cboProspectCommercial.SelectedIndex = i;
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

        // Ajouter un Prospect
        #region BTN Ajouter Prospect
        private void btnAjouterProspect_Click(object sender, RoutedEventArgs e)
        {

                // Récupération du Pigiste sélectionné dans le Combobox cboPigiste
                Commerciaux ModifCommercial = cboProspectCommercial.SelectedItem as Commerciaux;


                Prospect tmpProspect = new Prospect(0, txtNomProspect.Text, txtPrenomProspect.Text, txtTelProspect.Text, txtMailProspect.Text, dtpCeationProspect.Text, (Commerciaux)cboProspectCommercial.SelectedItem);
                bdd.InsertProspect(tmpProspect.NomProspect, tmpProspect.PrenomProspect, tmpProspect.TelephoneProspect, tmpProspect.EmailProspect, tmpProspect.DateCreation, tmpProspect.LeCommercial);

                // Mets a jours pigistes
                lesProspect = bdd.SelectProspect();

                // Met à jour le DataGrid
                dtgProspect.ItemsSource = lesProspect;
                dtgProspect.SelectedIndex = 0;
                dtgProspect.Items.Refresh();
                dtgRendezVous.Items.Refresh();
                cboProspectRendezVous.Items.Refresh();
            
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
            lesProspect.ElementAt(indice).PrenomProspect = txtPrenomProspect.Text;
            lesProspect.ElementAt(indice).TelephoneProspect = txtTelProspect.Text;
            lesProspect.ElementAt(indice).EmailProspect = txtMailProspect.Text;
            lesProspect.ElementAt(indice).DateCreation = dtpCeationProspect.Text;
            lesProspect.ElementAt(indice).LeCommercial = (Commerciaux)cboProspectCommercial.SelectedItem;

            Prospect prospectModifie = lesProspect.ElementAt(indice);
            bdd.UpdateProspect(
                prospectModifie.IdProspect,
                prospectModifie.NomProspect,
                prospectModifie.PrenomProspect,
                prospectModifie.TelephoneProspect,
                prospectModifie.EmailProspect,
                prospectModifie.DateCreation,
                prospectModifie.LeCommercial.IdCommerciaux // Ici on passe l'ID du commercial
            );

            dtgProspect.Items.Refresh();
            dtgRendezVous.Items.Refresh();
            cboProspectRendezVous.Items.Refresh();
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
                cboProspectRendezVous.Items.Refresh();
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
                    txtPrenomClient.Text = Convert.ToString(selectedClient.PrenomClient);
                    txtEmailClient.Text = Convert.ToString(selectedClient.EmailClient);
                    txtTelClient.Text = Convert.ToString(selectedClient.TelephoneClient);
                    txtAdresseClient.Text = Convert.ToString(selectedClient.AdresseClient);
                    dtpCreationClient.Text = Convert.ToString(selectedClient.DateCreationClient);

                    // Vérifiez si LeCommercial existe avant d'affecter la valeur
                    if (selectedClient.LeCommercial != null)
                    {
                        cboCommercialClient.SelectedValue = selectedClient.LeCommercial.IdCommerciaux;
                    }

                    // Sélectionner le commercial concerné dans la ComboBox
                    int i = 0;
                    bool trouve = false;

                    while (i < cboCommercialClient.Items.Count && !trouve)
                    {
                        if (Convert.ToString(cboCommercialClient.Items[i]) == Convert.ToString(selectedClient.LeCommercial))
                        {
                            trouve = true;
                            cboCommercialClient.SelectedIndex = i;
                        }
                        i++;
                    }
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
            Commerciaux ModifCommercial = cboProspectCommercial.SelectedItem as Commerciaux;

            Client tmpCLient = new Client(0, txtNomClient.Text, txtPrenomClient.Text, txtEmailClient.Text, txtTelClient.Text, txtAdresseClient.Text, dtpCreationClient.Text, (Commerciaux)cboCommercialClient.SelectedItem);
            bdd.InsertClient(tmpCLient.NomClient, tmpCLient.PrenomClient, tmpCLient.EmailClient, tmpCLient.TelephoneClient, tmpCLient.AdresseClient, tmpCLient.DateCreationClient, tmpCLient.LeCommercial);

            lesClients = bdd.SelectClient();

            dtgClient.ItemsSource = lesClients;
            dtgClient.SelectedIndex = 0;
            dtgClient.Items.Refresh();
            dtgFacture.Items.Refresh();
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
            lesClients.ElementAt(indice).PrenomClient = txtPrenomClient.Text;
            lesClients.ElementAt(indice).EmailClient = txtEmailClient.Text;
            lesClients.ElementAt(indice).TelephoneClient = txtTelClient.Text;
            lesClients.ElementAt(indice).AdresseClient = txtAdresseClient.Text;
            lesClients.ElementAt(indice).DateCreationClient = dtpCreationClient.Text;
            lesClients.ElementAt(indice).LeCommercial = (Commerciaux)cboCommercialClient.SelectedItem;

            Client clientModifie = lesClients.ElementAt(indice);
            bdd.UpdateClient(
                clientModifie.IdClient,
                clientModifie.NomClient,
                clientModifie.PrenomClient,
                clientModifie.EmailClient,
                clientModifie.TelephoneClient,
                clientModifie.AdresseClient,
                clientModifie.DateCreationClient,
                clientModifie.LeCommercial.IdCommerciaux // Ici on passe l'ID du commercial
            );

            dtgClient.Items.Refresh();
            dtgFacture.Items.Refresh();
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
                dtgFacture.Items.Refresh();
                cboClientFacture.Items.Refresh();
                dtgRendezVous.Items.Refresh();
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
                    dateAjoutProduit.Text = selectedProduit.DateAjoutProduit;
                    txtImageProduit.Text = selectedProduit.ImgProduit;
                }
            }
        }
        #endregion

        // Ajouter un produit
        #region BTN Ajouter Produit
        private void btnAjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgClient.SelectedIndex >= 0)
            {
                Produit tmpProduit = new Produit(0, txtNomProduit.Text, txtDescProduit.Text, Convert.ToInt16(txtPrixUnitaire.Text), dateAjoutProduit.Text, txtImageProduit.Text);
                bdd.InsertProduit(tmpProduit.NomProduit, tmpProduit.DesciptionProduit, tmpProduit.PrixUnitaire, tmpProduit.DateAjoutProduit, tmpProduit.ImgProduit);

                lesProduit = bdd.SelectProduit();

                dtgProduit.ItemsSource = lesProduit;
                dtgProduit.SelectedIndex = 0;
                dtgProduit.Items.Refresh();
                dtgFacture.Items.Refresh();
                cboProduitFacture.Items.Refresh();
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
                bdd.UpdateProduit(Convert.ToInt32(txtIdProduit.Text), txtNomProduit.Text, txtDescProduit.Text, Convert.ToInt16(txtPrixUnitaire.Text),dateAjoutProduit.Text, txtImageProduit.Text);

                lesProduit.ElementAt(indice).NomProduit = txtNomProduit.Text;
                lesProduit.ElementAt(indice).DesciptionProduit = txtDescProduit.Text;
                lesProduit.ElementAt(indice).PrixUnitaire = Convert.ToInt16(txtPrixUnitaire.Text);
                lesProduit.ElementAt(indice).DateAjoutProduit = dateAjoutProduit.Text;
                lesProduit.ElementAt(indice).ImgProduit = txtImageProduit.Text;
            }

            dtgProduit.Items.Refresh();
            dtgFacture.Items.Refresh();
            cboProduitFacture.Items.Refresh();
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
                    dtgFacture.Items.Refresh();
                    cboProduitFacture.Items.Refresh();
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
                    dtpRendezVous.Text = selectedRendezVous.DateRendezVous;
                    txtDescRendezVous.Text = Convert.ToString(selectedRendezVous.DescriptionRendezVous);
                    cboDebutRendezVous.SelectedItem = FormatTimeSpanAs1h00(selectedRendezVous.HeureDebutRendezVous);
                    cboFinRendezVous.SelectedItem = FormatTimeSpanAs1h00(selectedRendezVous.HeureFinRendezVous);

                    // Mettre à jour la sélection du rendezvous dans le ComboBox
                    if (selectedRendezVous.LeCommercial != null)
                    {
                        cboCommerciauxRendezVous.SelectedValue = selectedRendezVous.LeCommercial.IdCommerciaux;
                    }

                    // Mettre à jour la sélection du prospect dans le ComboBox
                    if (selectedRendezVous.LeProspect != null)
                    {
                        cboProspectRendezVous.SelectedValue = selectedRendezVous.LeProspect.IdProspect;
                    }

                    // Mettre à jour la sélection du client dans le ComboBox
                    if (selectedRendezVous.LeClient != null)
                    {
                        cboClientRendezVous.SelectedValue = selectedRendezVous.LeClient.IdClient;
                    }


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

                    while (i < cboProspectRendezVous.Items.Count && trouve == false)
                    {
                        if (Convert.ToString(cboProspectRendezVous.Items[i]) == Convert.ToString(selectedRendezVous.LeProspect))
                        {
                            trouve = true;
                            cboProspectRendezVous.SelectedIndex = i;
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
            // Récupération du Pigiste sélectionné dans le Combobox cboPigiste
            Commerciaux ModifCommerciauxRendezVous = cboCommerciauxRendezVous.SelectedItem as Commerciaux;
            Prospect ModifProspectRendezVous = cboProspectRendezVous.SelectedItem as Prospect;
            Client ModifClientRendezVous = cboClientRendezVous.SelectedItem as Client;


            RendezVous tmpRendezVous = new RendezVous(
                0,
                dtpRendezVous.Text,
                txtDescRendezVous.Text,
                ConvertToTimeSpan((string)cboDebutRendezVous.SelectedItem),
                ConvertToTimeSpan((string)cboFinRendezVous.SelectedItem),
                (Commerciaux)cboCommerciauxRendezVous.SelectedItem,
                (Prospect)cboProspectRendezVous.SelectedItem,
                (Client)cboClientRendezVous.SelectedItem
            );


            bdd.InsertRendezVous(
                tmpRendezVous.DateRendezVous,
                tmpRendezVous.DescriptionRendezVous,
                tmpRendezVous.HeureDebutRendezVous.ToString(@"hh\:mm"),
                tmpRendezVous.HeureFinRendezVous.ToString(@"hh\:mm"),
                tmpRendezVous.LeCommercial,
                tmpRendezVous.LeProspect,
                tmpRendezVous.LeClient
            );


            // Mets a jours pigistes
            lesRendezVous = bdd.SelectRendezVous();

            // Met à jour le DataGrid
            dtgRendezVous.ItemsSource = lesRendezVous;
            dtgRendezVous.SelectedIndex = 0;
            dtgRendezVous.Items.Refresh();
        }
        #endregion

        // Modifier un Rendez Vous
        #region BTN Modifier RendezVous
        private void btnModifierRendezVous_Click(object sender, RoutedEventArgs e)
        {
            int indice = lesRendezVous.IndexOf((RendezVous)dtgRendezVous.SelectedItem);

            lesRendezVous.ElementAt(indice).DateRendezVous = dtpRendezVous.Text;
            lesRendezVous.ElementAt(indice).DescriptionRendezVous = Convert.ToString(txtDescRendezVous.Text);
            lesRendezVous.ElementAt(indice).HeureDebutRendezVous = TimeSpan.FromHours(cboDebutRendezVous.SelectedIndex);
            lesRendezVous.ElementAt(indice).HeureFinRendezVous = TimeSpan.FromHours(cboFinRendezVous.SelectedIndex);

            lesRendezVous.ElementAt(indice).LeCommercial = (Commerciaux)cboCommerciauxRendezVous.SelectedItem;
            lesRendezVous.ElementAt(indice).LeProspect = (Prospect)cboProspectRendezVous.SelectedItem;
            lesRendezVous.ElementAt(indice).LeClient = (Client)cboClientRendezVous.SelectedItem;

            RendezVous rendezvousModifie = lesRendezVous.ElementAt(indice);
            bdd.UpdateRendezVous(
                rendezvousModifie.IdRendezVous,
                rendezvousModifie.DateRendezVous,
                rendezvousModifie.DescriptionRendezVous,
                rendezvousModifie.HeureDebutRendezVous.ToString(@"hh\:mm"),
                rendezvousModifie.HeureFinRendezVous.ToString(@"hh\:mm"),
                rendezvousModifie.LeCommercial.IdCommerciaux,
                rendezvousModifie.LeProspect.IdProspect,
                rendezvousModifie.LeClient.IdClient
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


        // ------------- Facture ------------------
        #region Facture

        // Affichage dans la datagrid Facture
        #region Affichage Facture
        private void dtgFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Facture selectedFacture = dtgFacture.SelectedItem as Facture;
            if (dtgFacture.SelectedItem != null)
            {
                try
                {
                    txtIdFacture.Text = Convert.ToString(selectedFacture.IdFacture);
                    dateFacture.Text = selectedFacture.DateFacture;
                    txtMontantTotal.Text = Convert.ToString(selectedFacture.MontantTotal);
                    //cboStatutFacture.SelectedValue = selectedFacture.StatutFacture;

                    cboStatutFacture.SelectedIndex = selectedFacture.StatutFacture;

                    datePaiementFacture.Text = Convert.ToString(selectedFacture.DatePaiment);
                    //cboProduitFacture.SelectedValue = selectedFacture.LeProduit.IdProduit;
                    //cboClientFacture.SelectedValue = selectedFacture.LeClient.IdClient;

                    // Mettre à jour la sélection du produit dans le ComboBox
                    if (selectedFacture.LeProduit != null)
                    {
                        cboProduitFacture.SelectedValue = selectedFacture.LeProduit.IdProduit;
                    }

                    // Mettre à jour la sélection du client dans le ComboBox
                    if (selectedFacture.LeClient != null)
                    {
                        cboClientFacture.SelectedValue = selectedFacture.LeClient.IdClient;
                    }

                    int i = 0;
                    bool trouve = false;

                    while (i < cboProduitFacture.Items.Count && trouve == false)
                    {
                        if (Convert.ToString(cboProduitFacture.Items[i]) == Convert.ToString(selectedFacture.LeProduit))
                        {
                            trouve = true;
                            cboProduitFacture.SelectedIndex = i;
                        }
                        i++;
                    }

                    while (i < cboClientFacture.Items.Count && trouve == false)
                    {
                        if (Convert.ToString(cboClientFacture.Items[i]) == Convert.ToString(selectedFacture.LeClient))
                        {
                            trouve = true;
                            cboClientFacture.SelectedIndex = i;
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

        // Ajouter une Facture
        #region BTN Ajouter une facture
        private void btnAjouterFacture_Click(object sender, RoutedEventArgs e)
        {
                // Récupération du Pigiste sélectionné dans le Combobox cboPigiste
                Produit ModifProduitFacture = cboProduitFacture.SelectedItem as Produit;
                Client ModifClientFacture = cboClientFacture.SelectedItem as Client;

                Facture tmpFacture = new Facture(0, dateFacture.Text, Convert.ToInt32(txtMontantTotal.Text), cboStatutFacture.SelectedIndex, datePaiementFacture.Text, (Produit)cboProduitFacture.SelectedItem, (Client)cboClientFacture.SelectedItem);
                bdd.InsertFacture(tmpFacture.DateFacture, tmpFacture.MontantTotal, tmpFacture.StatutFacture, tmpFacture.DatePaiment, tmpFacture.LeProduit, tmpFacture.LeClient);

                lesFactures = bdd.SelectFacture();

                dtgFacture.ItemsSource = lesFactures;
                dtgFacture.SelectedIndex = 0;
                dtgFacture.Items.Refresh();
        }
        #endregion

        // Modifier une facture
        #region BTN Modifier Facture
        private void btnModifierFacture_Click(object sender, RoutedEventArgs e)
        {
            int indice = lesFactures.IndexOf((Facture)dtgFacture.SelectedItem);

            lesFactures.ElementAt(indice).DateFacture = dateFacture.Text;
            lesFactures.ElementAt(indice).MontantTotal = Convert.ToInt32(txtMontantTotal.Text);
            lesFactures.ElementAt(indice).StatutFacture = cboStatutFacture.SelectedIndex;
            lesFactures.ElementAt(indice).DatePaiment = datePaiementFacture.Text;
            lesFactures.ElementAt(indice).LeProduit = (Produit)cboProduitFacture.SelectedItem;
            lesFactures.ElementAt(indice).LeClient = (Client)cboClientFacture.SelectedItem;

            Facture factureModifie = lesFactures.ElementAt(indice);
            bdd.UpdateFacture(
                factureModifie.IdFacture,
                factureModifie.DateFacture,
                factureModifie.MontantTotal,
                factureModifie.StatutFacture,
                factureModifie.DatePaiment,
                factureModifie.LeProduit.IdProduit,
                factureModifie.LeClient.IdClient
            );

            dtgFacture.Items.Refresh();
        }
        #endregion

        // Suprimmer une facture
        #region BTN Suprimmer Facture
        private void btnSupprimerFacture_Click(object sender, RoutedEventArgs e)
        {
            if (dtgFacture.SelectedIndex >= 0)
            {
                bdd.DeleteFacture(Convert.ToInt16(txtIdFacture.Text));

                lesFactures = bdd.SelectFacture();

                dtgFacture.ItemsSource = lesFactures;
                dtgFacture.SelectedIndex = 0;
            }
        }
        #endregion

        // Exporter une facture
        #region BTN Exporter Facture
        private void btnExporterFacture_Click(object sender, RoutedEventArgs e)
        {
            // Vérifiez si une facture est sélectionnée dans le DataGrid
            Facture selectedFacture = dtgFacture.SelectedItem as Facture;

            if (selectedFacture == null)
            {
                MessageBox.Show("Veuillez sélectionner une facture pour l'exporter.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Générer un nom de fichier basé sur le numéro et la date de la facture
                string sanitizedDate = selectedFacture.DateFacture.Replace("/", "-").Replace("\\", "-");
                string factureFileName = $"Facture_{selectedFacture.IdFacture}_{sanitizedDate}.pdf";
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
                gfx.DrawString($"Numéro de Facture: {selectedFacture.IdFacture}", font, XBrushes.Black, 40, 80);
                gfx.DrawString($"Date de Facture: {selectedFacture.DateFacture}", font, XBrushes.Black, 40, 110);
                gfx.DrawString($"Montant Total: {selectedFacture.MontantTotal} €", font, XBrushes.Black, 40, 140);
                gfx.DrawString($"Statut: {selectedFacture.StatutFacture}", font, XBrushes.Black, 40, 170);
                gfx.DrawString($"Date de Paiement: {selectedFacture.DatePaiment}", font, XBrushes.Black, 40, 200);

                // Ajouter les informations du produit
                if (selectedFacture.LeProduit != null)
                {
                    gfx.DrawString($"Produit: {selectedFacture.LeProduit.NomProduit}", font, XBrushes.Black, 40, 230);
                }

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
            List<Facture> factures = bdd.SelectFacture();

            // Créer et ouvrir la fenêtre du graphique
            GraphiqueWindow graphiqueWindow = new GraphiqueWindow(factures);
            graphiqueWindow.Show();
        }
        #endregion

        #endregion


        // BTN Actualiser
        #region BTN Actualiser
        private void btnActualliser_Click(object sender, RoutedEventArgs e)
        {
            // Rafraîchissez les données depuis la base de données
            List<Commerciaux> commerciaux = bdd.SelectCommerciaux(); // Exemple : récupérer les commerciaux depuis la base de données
            List<Prospect> prospects = bdd.SelectProspect(); // Exemple : récupérer les prospects depuis la base de données
            List<Client> clients = bdd.SelectClient(); // Exemple : récupérer les clients depuis la base de données
            List<RendezVous> rendezVous = bdd.SelectRendezVous(); // Exemple : récupérer les rendez-vous depuis la base de données
            List<Facture> factures = bdd.SelectFacture(); // Exemple : récupérer les factures depuis la base de données
            List<Produit> produits = bdd.SelectProduit(); // Exemple : récupérer les produits depuis la base de données

            // Actualisez les DataGrids avec les nouvelles données
            dtgCommerciaux.ItemsSource = commerciaux;
            dtgProspect.ItemsSource = prospects;
            dtgClient.ItemsSource = clients;
            dtgRendezVous.ItemsSource = rendezVous;
            dtgFacture.ItemsSource = factures;
            dtgProduit.ItemsSource = produits;

            // Actualisez les ComboBox avec les nouvelles données
            cboProspectCommercial.ItemsSource = prospects;
            cboCommercialClient.ItemsSource = commerciaux;
            cboCommerciauxRendezVous.ItemsSource = commerciaux;
            cboProspectRendezVous.ItemsSource = prospects;
            cboClientRendezVous.ItemsSource = clients;
            cboProduitFacture.ItemsSource = produits;
            cboClientFacture.ItemsSource = clients;

            // Rafraîchissez les éléments de l'interface graphique
            dtgCommerciaux.Items.Refresh();
            dtgProspect.Items.Refresh();
            dtgClient.Items.Refresh();
            dtgRendezVous.Items.Refresh();
            dtgFacture.Items.Refresh();
            dtgProduit.Items.Refresh();

            cboProspectCommercial.Items.Refresh();
            cboCommercialClient.Items.Refresh();
            cboCommerciauxRendezVous.Items.Refresh();
            cboProspectRendezVous.Items.Refresh();
            cboClientRendezVous.Items.Refresh();
            cboStatutFacture.Items.Refresh();
            cboProduitFacture.Items.Refresh();
            cboClientFacture.Items.Refresh();
        }
        #endregion

    }
}
