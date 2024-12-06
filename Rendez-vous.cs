using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class Rendez_vous
    {
        #region Propriétés
        private int idRendezVous;
        private string dateRendezVous;
        private string descriptionRendezVous;
        private string heureDebutRendezVous;
        private string heureFintRendezVous;
        private Commerciaux leCommercial;
        private Prospect leProspect;
        private Client leClient;
        #endregion

        #region Méthodes
        public int IdRendezVous
        {
            get { return idRendezVous; }
            set { idRendezVous = value; }
        }

        public string DateRendezVous
        {
            get { return dateRendezVous; }
            set { dateRendezVous = value; }
        }

        public string DescriptionRendezVous
        {
            get { return descriptionRendezVous; }
            set { descriptionRendezVous = value; }
        }

        public string HeureDebutRendezVous
        {
            get { return heureDebutRendezVous; }
            set { heureDebutRendezVous = value; }
        }

        public string HeureFintRendezVous
        {
            get { return heureFintRendezVous; }
            set { heureFintRendezVous = value; }
        }

        public Commerciaux LeCommercial
        {
            get { return leCommercial; }
            set { leCommercial = value; }
        }

        public Prospect LeProspect
        {
            get { return leProspect; }
            set { leProspect = value; }
        }

        public Client LeClient
        {
            get { return leClient; }
            set { leClient = value; }
        }

        #endregion

        #region Constructeur
        public Rendez_vous(int idRendezVous, string dateRendezVous, string descriptionRendezVous, string heureDebutRendezVous, string heureFintRendezVous, Commerciaux leCommercial, Prospect leProspect, Client leClient)
        {
            this.idRendezVous = idRendezVous;
            this.dateRendezVous = dateRendezVous;
            this.descriptionRendezVous = descriptionRendezVous;
            this.heureDebutRendezVous = heureDebutRendezVous;
            this.heureFintRendezVous = HeureFintRendezVous;
            this.leCommercial = leCommercial;
            this.leProspect = leProspect;
            this.leClient = leClient;
        }
        #endregion

        public override string ToString()
        {
            return dateRendezVous + " ";
        }

    }
}
