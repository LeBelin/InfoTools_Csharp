using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class Prospect
    {
        #region Propriétés
        private int idProspect;
        private string nomProspect;
        private string emailProspect;
        private string telephoneProspect;
        private string adresseProspect;
        #endregion

        public int IdProspect
        {
            get { return idProspect; }
            set { idProspect = value; }
        }

        public string NomProspect
        {
            get { return nomProspect; }
            set { nomProspect = value; }
        }

        public string EmailProspect
        {
            get { return emailProspect; }
            set { emailProspect = value; }
        }

        public string TelephoneProspect
        {
            get { return telephoneProspect; }
            set { telephoneProspect = value; }
        }

        public string AdresseProspect
        {
            get { return adresseProspect; }
            set { adresseProspect = value; }
        }




        public Prospect(int idProspect, string nomProspect, string emailProspect, string telephoneProspect, string adresseProspect)
        {
            this.idProspect = idProspect;
            this.nomProspect = nomProspect;
            this.emailProspect = emailProspect;
            this.telephoneProspect = telephoneProspect;
            this.adresseProspect = adresseProspect;
        }

        public override string ToString()
        {
            return NomProspect;
        }


    }
}
