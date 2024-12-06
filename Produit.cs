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
        private string dateAjoutProduit;
        private string imgProduit;
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

        public string DateAjoutProduit
        {
            get { return dateAjoutProduit; }
            set { dateAjoutProduit = value; }
        }

        public string ImgProduit
        {
            get { return imgProduit; }
            set { imgProduit = value; }
        }


        #endregion

        #region Constructeur
        public Produit(int idProduit, string nomProduit, string desciptionProduit, int prixUnitaire, string dateAjoutProduit, string imgProduit)
        {
            this.idProduit = idProduit;
            this.nomProduit = nomProduit;
            this.desciptionProduit = desciptionProduit;
            this.prixUnitaire = prixUnitaire;
            this.dateAjoutProduit = dateAjoutProduit;
            this.imgProduit = imgProduit;
        }
        #endregion

        public override string ToString()
        {
            return nomProduit + " " ;
        }


    }
}
