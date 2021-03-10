using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.DataTransferObjects
{
    public class ModeleDto
    {
        public int? Id { get; set; }
        public string CategorieNom { get; set; }
        public string Marque { get; set; }
        public string Nom { get; set; }
        public string NbrSieges { get; set; }
        public ModeleDto()
        {

        }
        public ModeleDto(int? id, string categorieNom, string marque, string nom, string nbrSieges)
        {
            Id = id;
            CategorieNom = categorieNom;
            Marque = marque;
            Nom = nom;
            NbrSieges = nbrSieges;
        }

        public ModeleDto(ModeleDto modele)
        {
            Id = modele.Id;
            CategorieNom = modele.CategorieNom;
            Marque = modele.Marque;
            Nom = modele.Nom;
            NbrSieges = modele.NbrSieges;
        }
    }
}
