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
        private string prenomProspect;
        private string telephoneProspect;
        private string emailProspect;
        private string dateCreation;
        private Commerciaux leCommercial;
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

        public string PrenomProspect
        {
            get { return prenomProspect; }
            set { prenomProspect = value; }
        }

        public string TelephoneProspect
        {
            get { return telephoneProspect; }
            set { telephoneProspect = value; }
        }

        public string EmailProspect
        {
            get { return emailProspect; }
            set { emailProspect = value; }
        }

        public string DateCreation
        {
            get { return dateCreation; }
            set { dateCreation = value; }
        }

        public Commerciaux LeCommercial
        {
            get { return leCommercial; }
            set { leCommercial = value; }
        }



        public Prospect(int idProspect, string nomProspect, string prenomProspect, string telephoneProspect, string emailProspect, string dateCreation, Commerciaux leCommercial)
        {
            this.idProspect = idProspect;
            this.nomProspect = nomProspect;
            this.prenomProspect = prenomProspect;
            this.telephoneProspect = telephoneProspect;
            this.emailProspect = emailProspect;
            this.dateCreation = dateCreation;
            this.leCommercial = leCommercial;
        }




    }
}
