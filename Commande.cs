using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class Commande
    {
        #region Propriétés
        private int idCommande;
        private Client leClient;
        private int montantTotal;
        private string dateCommande;
        #endregion

        public int IdCommande
        {
            get { return idCommande; }
            set { idCommande = value; }
        }
        public Client LeClient
        {
            get { return leClient; }
            set { leClient = value; }
        }

        public int MontantTotal
        {
            get { return montantTotal; }
            set { montantTotal = value; }
        }

        public string DateCommande
        {
            get { return dateCommande; }
            set { dateCommande = value; }
        }

        // 🔥 Nouvelle propriété
        private List<LigneCommande> produitsCommande;

        public List<LigneCommande> ProduitsCommande
        {
            get { return produitsCommande; }
            set { produitsCommande = value; }
        }

        public Commande(int idCommande, Client leClient, int montantTotal, string dateCommande)
        {
            this.idCommande = idCommande;
            this.leClient = leClient;
            this.montantTotal = montantTotal;
            this.dateCommande = dateCommande;
            this.produitsCommande = new List<LigneCommande>();
        }

        public override string ToString()
        {
            return IdCommande + " ";
        }
    }
}
