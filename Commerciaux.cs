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
        private string mailCommerciaux;
        private string telCommerciaux;
        private string adresseCommerciaux;
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

        public string AdresseCommerciaux
        {
            get { return adresseCommerciaux; }
            set { adresseCommerciaux = value; }
        }


        #endregion

        #region Constructeur
        public Commerciaux(int idCommerciaux, string nomCommerciaux, string mailCommerciaux, string telCommerciaux, string adresseCommerciaux)
        {
            this.idCommerciaux = idCommerciaux;
            this.nomCommerciaux = nomCommerciaux;
            this.mailCommerciaux = mailCommerciaux;
            this.telCommerciaux = telCommerciaux;
            this.adresseCommerciaux = adresseCommerciaux;
        }
        #endregion

        public override string ToString()
        {
            return nomCommerciaux;
        }

    }
}
