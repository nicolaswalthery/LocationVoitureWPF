using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.classeMetier
{
    public class CategorieDto
    {
        public string Nom { get; set; }
        public string PrixJour { get; set; }
        public CategorieDto()
        {
                
        }
        public CategorieDto(string nom, string prixJour)
        {
            Nom = nom;
            PrixJour = prixJour;
        }

        public CategorieDto(CategorieDto categorie)
        {
            Nom = categorie.Nom;
            PrixJour = categorie.PrixJour;
        }
    }
}
