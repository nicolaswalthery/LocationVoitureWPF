using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.classeMetier
{
    public class Modele
    {
        public int? Id { get; set; }
        public string CategorieNom { get; set; }
        public string Marque { get; set; }
        public string Nom { get; set; }
        public int? NbrSieges { get; set; }
        public Modele()
        {

        }
        public Modele(int? id, string categorieNom, string marque, string nom, int? nbrSieges)
        {
            Id = id;
            CategorieNom = categorieNom;
            Marque = marque;
            Nom = nom;
            NbrSieges = nbrSieges;
        }

        public Modele(Modele modele)
        {
            Id = modele.Id;
            CategorieNom = modele.CategorieNom;
            Marque = modele.Marque;
            Nom = modele.Nom;
            NbrSieges = modele.NbrSieges;
        }
    }
}
