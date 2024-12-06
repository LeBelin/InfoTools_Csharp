using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

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
        List<Facture> lesFactures = new List<Facture>();

        public MainWindow()
        {
            bdd.Initialize();
            InitializeComponent();

            lesCommerciaux = bdd.SelectCommerciaux();
            lesProspect = bdd.SelectProspect();
            lesClients = bdd.SelectClient();
            lesProduit = bdd.SelectProduit();
            lesFactures = bdd.SelectFacture();

            //Lie le Datagrid dtgMagazine avec la collection cMagazine
            dtgCommerciaux.ItemsSource = lesCommerciaux;
            dtgProspect.ItemsSource = lesProspect;
            dtgClient.ItemsSource = lesClients;
            dtgProduit.ItemsSource = lesProduit;
            dtgFacture.ItemsSource = lesFactures;

            dtgCommerciaux.SelectedIndex = 0;
            dtgProspect.SelectedIndex = 0;
            dtgClient.SelectedIndex = 0;
            dtgProduit.SelectedIndex = 0;
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

        }

        // ------- Commerciaux -----------
        #region Commerciaux
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

        private void btnAjouterCommerciaux_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCommerciaux.SelectedIndex >= 0)
            {
                Commerciaux tmpCommerciaux = new Commerciaux(0, txtNomCommercial.Text, txtPrenomCommercial.Text, txtAdresseCommercial.Text, txtVilleCommercial.Text, txtCPCommercial.Text, txtMailCommercial.Text, txtTelCommercial.Text);
                bdd.InsertCommerciaux(tmpCommerciaux.NomCommerciaux, tmpCommerciaux.PrenomCommerciaux, tmpCommerciaux.AdresseCommerciaux, tmpCommerciaux.VilleCommerciaux, tmpCommerciaux.CpCommerciaux, tmpCommerciaux.MailCommerciaux, tmpCommerciaux.TelCommerciaux);

                // Mets a jours pigistes
                lesCommerciaux = bdd.SelectCommerciaux();

                // Met à jour le DataGrid
                dtgCommerciaux.ItemsSource = lesCommerciaux;
                dtgCommerciaux.SelectedIndex = 0;
                dtgCommerciaux.Items.Refresh();
            }
        }

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
        }

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
                }
                dtgCommerciaux.SelectedIndex = 0;
            }
        }
        #endregion

        // ------------- Prospect -----------------
        #region Prospect
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

        private void btnAjouterProspect_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProspect.SelectedIndex >= 0)
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
            }
            // lesContrats.Add(tmpContrat);
        }

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
        }

        private void btnSupprimerProspect_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProspect.SelectedIndex >= 0)
            {
                bdd.DeleteProspect(Convert.ToInt16(txtNumProspect.Text));

                // Actualise magazines dans l'application
                lesProspect = bdd.SelectProspect();

                dtgProspect.ItemsSource = lesProspect;
                dtgProspect.SelectedIndex = 0;
            }
        }
        #endregion

        // ------------- Client -----------------
        #region Client
    private void dtgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // On stocke dans l'objet selectedClient le Client sélectionné dans le datagrid dtgClient
        Client selectedClient = dtgClient.SelectedItem as Client;

        if (selectedClient != null) // Vérifier si un client est sélectionné
        {
            try
            {
                // Remplissage des TextBox avec les données du client sélectionné
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

                // Assurez-vous que la ComboBox a des éléments
                while (i < cboCommercialClient.Items.Count && !trouve)
                {
                    // Comparez les éléments de la ComboBox à la valeur du commercial
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
                // Afficher un message d'erreur plus explicite en cas de problème
                MessageBox.Show("Erreur sur la mise à jour du formulaire : " + ex.Message);
            }
        }

    }


        private void btnAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            if (dtgClient.SelectedIndex >= 0)
            {
                // Récupération du Pigiste sélectionné dans le Combobox cboPigiste
                Commerciaux ModifCommercial = cboProspectCommercial.SelectedItem as Commerciaux;


                Client tmpCLient = new Client(0, txtNomClient.Text, txtPrenomClient.Text, txtEmailClient.Text, txtTelClient.Text, txtAdresseClient.Text, dtpCreationClient.Text, (Commerciaux)cboCommercialClient.SelectedItem);
                bdd.InsertClient(tmpCLient.NomClient, tmpCLient.PrenomClient, tmpCLient.EmailClient, tmpCLient.TelephoneClient, tmpCLient.AdresseClient, tmpCLient.DateCreationClient, tmpCLient.LeCommercial);

                // Mets a jours pigistes
                lesClients = bdd.SelectClient();

                // Met à jour le DataGrid
                dtgClient.ItemsSource = lesClients;
                dtgClient.SelectedIndex = 0;
                dtgClient.Items.Refresh();
            }
            // lesContrats.Add(tmpContrat);
        }

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
        }

        private void btnSupprimerClient_Click(object sender, RoutedEventArgs e)
        {
            if (dtgClient.SelectedIndex >= 0)
            {
                bdd.DeleteClient(Convert.ToInt16(txtIdClient.Text));

                // Actualise magazines dans l'application
                lesClients = bdd.SelectClient();

                dtgClient.ItemsSource = lesClients;
                dtgClient.SelectedIndex = 0;
            }
        }
        #endregion

        // ------------- Produit -----------------
        #region Produit
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

        private void btnAjouterProduit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgClient.SelectedIndex >= 0)
            {
                Produit tmpProduit = new Produit(0, txtNomProduit.Text, txtDescProduit.Text, Convert.ToInt16(txtPrixUnitaire.Text), dateAjoutProduit.Text, txtImageProduit.Text);
                bdd.InsertProduit(tmpProduit.NomProduit, tmpProduit.DesciptionProduit, tmpProduit.PrixUnitaire, tmpProduit.DateAjoutProduit, tmpProduit.ImgProduit);

                // Mets a jours pigistes
                lesProduit = bdd.SelectProduit();

                // Met à jour le DataGrid
                dtgProduit.ItemsSource = lesProduit;
                dtgProduit.SelectedIndex = 0;
                dtgProduit.Items.Refresh();
            }
        }

        private void btnModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            int indice = lesProduit.IndexOf((Produit)dtgProduit.SelectedItem);

            if (dtgProduit.SelectedIndex >= 0)
            {
                bdd.UpdateProduit(Convert.ToInt32(txtIdProduit.Text), txtNomProduit.Text, txtDescProduit.Text, Convert.ToInt16(txtPrixUnitaire.Text),dateAjoutProduit.Text, txtImageProduit.Text);

                // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
                lesProduit.ElementAt(indice).NomProduit = txtNomProduit.Text;
                lesProduit.ElementAt(indice).DesciptionProduit = txtDescProduit.Text;
                lesProduit.ElementAt(indice).PrixUnitaire = Convert.ToInt16(txtPrixUnitaire.Text);
                lesProduit.ElementAt(indice).DateAjoutProduit = dateAjoutProduit.Text;
                lesProduit.ElementAt(indice).ImgProduit = txtImageProduit.Text;
            }

            dtgProduit.Items.Refresh();
        }

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
                }
                dtgProduit.SelectedIndex = 0;
            }
        }


        #endregion

        // ------------- Facture -----------------
        #region Facture
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

        private void btnAjouterFacture_Click(object sender, RoutedEventArgs e)
        {
            if (dtgFacture.SelectedIndex >= 0)
            {
                // Récupération du Pigiste sélectionné dans le Combobox cboPigiste
                Produit ModifProduitFacture = cboProduitFacture.SelectedItem as Produit;
                Client ModifClientFacture = cboClientFacture.SelectedItem as Client;


                Facture tmpFacture = new Facture(0, dateFacture.Text, Convert.ToInt16(txtMontantTotal.Text), Convert.ToInt16(cboStatutFacture.Text), datePaiementFacture.Text, (Produit)cboProduitFacture.SelectedItem, (Client)cboClientFacture.SelectedItem);
                bdd.InsertFacture(tmpFacture.DateFacture, tmpFacture.MontantTotal, tmpFacture.StatutFacture, tmpFacture.DatePaiment, tmpFacture.LeProduit, tmpFacture.LeClient);

                // Mets a jours pigistes
                lesClients = bdd.SelectClient();

                // Met à jour le DataGrid
                dtgClient.ItemsSource = lesClients;
                dtgClient.SelectedIndex = 0;
                dtgClient.Items.Refresh();
            }
        }

        private void btnModifierFacture_Click(object sender, RoutedEventArgs e)
        {
            int indice = lesFactures.IndexOf((Facture)dtgFacture.SelectedItem);

            lesFactures.ElementAt(indice).DateFacture = dateFacture.Text;
            lesFactures.ElementAt(indice).MontantTotal = Convert.ToInt16(txtMontantTotal.Text);
            lesFactures.ElementAt(indice).StatutFacture = Convert.ToInt16( cboStatutFacture.Text);
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

    }
}
