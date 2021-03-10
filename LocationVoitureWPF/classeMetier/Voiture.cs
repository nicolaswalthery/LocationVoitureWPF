using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.classeMetier
{
    public class Voiture
    {
        public int? Id { get; set; }
        public int? ModeleId { get; set; }
        public string Immatriculation { get; set; }
        public string Couleur { get; set; }
        public Voiture()
        {

        }
        public Voiture(int? id, string immatriculation, string couleur, int? modeleId)
        {
            Id = id;
            ModeleId = modeleId;
            Immatriculation = immatriculation;
            Couleur = couleur;
        }

        public Voiture(Voiture voiture)
        {
            Id = voiture.Id;
            ModeleId = voiture.ModeleId;
            Immatriculation = voiture.Immatriculation;
            Couleur = voiture.Couleur;
        }
    }
}
