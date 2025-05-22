using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class CommandeProduit
    {
        #region Propriétés
        private int idCommandeProduit;
        private Commande laCommande;
        private Produit leProduit;
        private int quantite;
        private decimal prixUnitaireCommandeProduit;
        #endregion

        public int IdCommandeProduit
        {
            get { return idCommandeProduit; }
            set { idCommandeProduit = value; }
        }
        public Commande LaCommande
        {
            get { return laCommande; }
            set { laCommande = value; }
        }

        public Produit LeProduit
        {
            get { return leProduit; }
            set { leProduit = value; }
        }

        public int Quantite
        {
            get { return quantite; }
            set { quantite = value; }
        }

        public decimal PrixUnitaireCommandeProduit
        {
            get { return prixUnitaireCommandeProduit; }
            set { prixUnitaireCommandeProduit = value; }
        }


        public CommandeProduit(int idCommandeProduit, Commande laCommande, Produit leProduit, int quantite, decimal prixUnitaireCommandeProduit)
        {
            this.idCommandeProduit = idCommandeProduit;
            this.laCommande = laCommande;
            this.leProduit = leProduit;
            this.quantite = quantite;
            this.prixUnitaireCommandeProduit = prixUnitaireCommandeProduit;
        }
    }
}
