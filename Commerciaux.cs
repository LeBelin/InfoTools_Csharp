using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class Commerciaux
    {

        #region Propriétés
        private int idCommerciaux;
        private string nomCommerciaux;
        private string prenomCommerciaux;
        private string adresseCommerciaux;
        private string villeCommerciaux;
        private string cpCommerciaux;
        private string mailCommerciaux;
        private string telCommerciaux;
        #endregion

        #region Méthodes
        public int IdCommerciaux
        {
            get { return idCommerciaux; }
            set { idCommerciaux = value; }
        }

        public string NomCommerciaux
        {
            get { return nomCommerciaux; }
            set { nomCommerciaux = value; }
        }

        public string PrenomCommerciaux
        {
            get { return prenomCommerciaux; }
            set { prenomCommerciaux = value; }
        }

        public string AdresseCommerciaux
        {
            get { return adresseCommerciaux; }
            set { adresseCommerciaux = value; }
        }

        public string VilleCommerciaux
        {
            get { return villeCommerciaux; }
            set { villeCommerciaux = value; }
        }

        public string CpCommerciaux
        {
            get { return cpCommerciaux; }
            set { cpCommerciaux = value; }
        }

        public string MailCommerciaux
        {
            get { return mailCommerciaux; }
            set { mailCommerciaux = value; }
        }

        public string TelCommerciaux
        {
            get { return telCommerciaux; }
            set { telCommerciaux = value; }
        }

        #endregion

        #region Constructeur
        public Commerciaux(int idCommerciaux, string nomCommerciaux, string prenomCommerciaux, string adresseCommerciaux, string villeCommerciaux, string cpCommerciaux, string mailCommerciaux, string telCommerciaux)
        {
            this.idCommerciaux = idCommerciaux;
            this.nomCommerciaux = nomCommerciaux;
            this.prenomCommerciaux = prenomCommerciaux;
            this.adresseCommerciaux = adresseCommerciaux;
            this.villeCommerciaux = villeCommerciaux;
            this.cpCommerciaux = cpCommerciaux;
            this.mailCommerciaux = mailCommerciaux;
            this.telCommerciaux = telCommerciaux;
        }
        #endregion

        public override string ToString()
        {
            return nomCommerciaux + " " + prenomCommerciaux;
        }

    }
}
