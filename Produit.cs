using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class Produit
    {

        #region Propriétés
        private int idProduit;
        private string nomProduit;
        private string desciptionProduit;
        private int prixUnitaire;
        private int stockProduit;
        #endregion

        #region Méthodes
        public int IdProduit
        {
            get { return idProduit; }
            set { idProduit = value; }
        }

        public string NomProduit
        {
            get { return nomProduit; }
            set { nomProduit = value; }
        }

        public string DesciptionProduit
        {
            get { return desciptionProduit; }
            set { desciptionProduit = value; }
        }

        public int PrixUnitaire
        {
            get { return prixUnitaire; }
            set { prixUnitaire = value; }
        }

        public int StockProduit
        {
            get { return stockProduit; }
            set { stockProduit = value; }
        }

        #endregion

        #region Constructeur
        public Produit(int idProduit, string nomProduit, string desciptionProduit, int prixUnitaire, int stockProduit)
        {
            this.idProduit = idProduit;
            this.nomProduit = nomProduit;
            this.desciptionProduit = desciptionProduit;
            this.prixUnitaire = prixUnitaire;
            this.stockProduit = stockProduit;
        }
        #endregion

        public override string ToString()
        {
            return nomProduit + " " ;
        }


    }
}
