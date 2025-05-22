using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTools
{
    public class LigneCommande
    {
        public int IdProduit { get; set; }
        public string NomProduit { get; set; }
        public int Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }

        public LigneCommande(int idProduit, string nomProduit, int quantite, decimal prixUnitaire)
        {
            IdProduit = idProduit;
            NomProduit = nomProduit;
            Quantite = quantite;
            PrixUnitaire = prixUnitaire;
        }
    }


}
