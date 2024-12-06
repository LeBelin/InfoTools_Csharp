using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class Facture
    {
        #region Propriétés
        private int idFacture;
        private string dateFacture;
        private int montantTotal;
        private int statutFacture;
        private string datePaiment;
        private Produit leProduit;
        private Client leClient;
        #endregion

        public int IdFacture
        {
            get { return idFacture; }
            set { idFacture = value; }
        }

        public string DateFacture
        {
            get { return dateFacture; }
            set { dateFacture = value; }
        }

        public int MontantTotal
        {
            get { return montantTotal; }
            set { montantTotal = value; }
        }

        public int StatutFacture
        {
            get { return statutFacture; }
            set { statutFacture = value; }
        }

        public string DatePaiment
        {
            get { return datePaiment; }
            set { datePaiment = value; }
        }

        public Produit LeProduit
        {
            get { return leProduit; }
            set { leProduit = value; }
        }

        public Client LeClient
        {
            get { return leClient; }
            set { leClient = value; }
        }



        public Facture(int idFacture, string dateFacture, int montantTotal, int statutFacture, string datePaiment, Produit leProduit, Client leClient)
        {
            this.idFacture = idFacture;
            this.dateFacture = dateFacture;
            this.montantTotal = montantTotal;
            this.statutFacture = statutFacture;
            this.datePaiment = datePaiment;
            this.leProduit = leProduit;
            this.leClient = leClient;
        }
    }
}
