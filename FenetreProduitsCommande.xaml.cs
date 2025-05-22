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
using System.Windows.Shapes;

namespace InfoTools
{
    /// <summary>
    /// Logique d'interaction pour FenetreProduitsCommande.xaml
    /// </summary>
    public partial class FenetreProduitsCommande : Window
    {
        public List<ProduitSelectionne> ProduitsSelectionnes { get; private set; } = new List<ProduitSelectionne>();
        private List<ProduitSelectionne> produitsSelectionnes;

        public FenetreProduitsCommande(List<ProduitSelectionne> produits)
        {
            InitializeComponent();
            produitsSelectionnes = produits;
            dtgProduits.ItemsSource = produitsSelectionnes;
        }

        public FenetreProduitsCommande(List<Produit> produitsDispo)
        {
            InitializeComponent();
            var produitsAvecQuantite = produitsDispo.Select(p => new ProduitSelectionne
            {
                IdProduit = p.IdProduit,
                NomProduit = p.NomProduit,
                Prix = p.PrixUnitaire,
                Quantite = 0
            }).ToList();

            dtgProduits.ItemsSource = produitsAvecQuantite;
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            ProduitsSelectionnes = ((List<ProduitSelectionne>)dtgProduits.ItemsSource)
                .Where(p => p.Quantite > 0).ToList();

            DialogResult = true;
            Close();
        }
    }
    public class ProduitSelectionne
    {
        public int IdProduit { get; set; }
        public string NomProduit { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
    }

}
