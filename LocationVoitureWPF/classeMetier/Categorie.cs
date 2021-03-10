using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.classeMetier
{
    public class Categorie
    {
        public string Nom { get; set; }
        public decimal? PrixJour { get; set; }
        mlskmlqdkmsqkld
        public Categorie()
        {
                
        }
        public Categorie(string nom, decimal? prixJour)
        {
            Nom = nom;
            PrixJour = prixJour;
        }

        public Categorie(Categorie categorie)
        {
            Nom = categorie.Nom;
            PrixJour = categorie.PrixJour;
        }
    }
}
