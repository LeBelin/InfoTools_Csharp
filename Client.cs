using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class Client
    {
        #region Propriétés
        private int idClient;
        private string nomClient;
        private string prenomClient;
        private string emailClient;
        private string telephoneClient;
        private string adresseClient;
        private string dateCreationClient;
        private Commerciaux leCommercial;
        #endregion

        public int IdClient
        {
            get { return idClient; }
            set { idClient = value; }
        }

        public string NomClient
        {
            get { return nomClient; }
            set { nomClient = value; }
        }

        public string PrenomClient
        {
            get { return prenomClient; }
            set { prenomClient = value; }
        }

        public string EmailClient
        {
            get { return emailClient; }
            set { emailClient = value; }
        }

        public string TelephoneClient
        {
            get { return telephoneClient; }
            set { telephoneClient = value; }
        }

        public string AdresseClient
        {
            get { return adresseClient; }
            set { adresseClient = value; }
        }
        
        public string DateCreationClient
        {
            get { return dateCreationClient; }
            set { dateCreationClient = value; }
        }

        public Commerciaux LeCommercial
        {
            get { return leCommercial; }
            set { leCommercial = value; }
        }



        public Client(int idClient, string nomClient, string prenomClient, string emailClient, string telephoneClient, string adresseClient, string dateCreationClient, Commerciaux leCommercial)
        {
            this.idClient = idClient;
            this.nomClient = nomClient;
            this.prenomClient = prenomClient;
            this.emailClient = emailClient;
            this.telephoneClient = telephoneClient;
            this.adresseClient = adresseClient;
            this.dateCreationClient = dateCreationClient;
            this.leCommercial = leCommercial;
        }

        public override string ToString()
        {
            return NomClient + " " + PrenomClient;
        }

    }
}
