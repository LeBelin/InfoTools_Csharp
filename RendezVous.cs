using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class RendezVous
    {
        #region Propriétés
        private int idRendezVous;
        private Client leClient;
        private Commerciaux leCommercial;
        private string dateRendezVous;
        private TimeSpan heureRendezVous;
        private string descriptionRendezVous;
        #endregion

        #region Méthodes
        public int IdRendezVous
        {
            get { return idRendezVous; }
            set { idRendezVous = value; }
        }
        public Client LeClient
        {
            get { return leClient; }
            set { leClient = value; }
        }

        public Commerciaux LeCommercial
        {
            get { return leCommercial; }
            set { leCommercial = value; }
        }


        public string DateRendezVous
        {
            get { return dateRendezVous; }
            set { dateRendezVous = value; }
        }

        public TimeSpan HeureRendezVous
        {
            get { return heureRendezVous; }
            set { heureRendezVous = value; }
        }

        public string DescriptionRendezVous
        {
            get { return descriptionRendezVous; }
            set { descriptionRendezVous = value; }
        }
        #endregion

        #region Constructeur
        public RendezVous(int idRendezVous, Client leClient, Commerciaux leCommercial, string dateRendezVous, TimeSpan heureRendezVous, string descriptionRendezVous)
        {
            this.idRendezVous = idRendezVous;
            this.leCommercial = leCommercial;
            this.leClient = leClient;
            this.dateRendezVous = dateRendezVous;
            this.heureRendezVous = heureRendezVous;
            this.descriptionRendezVous = descriptionRendezVous;
        }
        #endregion


    }
}
