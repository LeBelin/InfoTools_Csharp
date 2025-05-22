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
        private string emailClient;
        private string telephoneClient;
        private string adresseClient;
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
        



        public Client(int idClient, string nomClient, string emailClient, string telephoneClient, string adresseClient)
        {
            this.idClient = idClient;
            this.nomClient = nomClient;
            this.emailClient = emailClient;
            this.telephoneClient = telephoneClient;
            this.adresseClient = adresseClient;
        }

        public override string ToString()
        {
            return NomClient;
        }

    }
}
