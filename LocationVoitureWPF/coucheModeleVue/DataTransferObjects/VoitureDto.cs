using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.DataTransferObjects
{
    public class VoitureDto
    {
        public int? Id { get; set; }
        public string ModeleId { get; set; }
        public string Immatriculation { get; set; }
        public string Couleur { get; set; }
        public VoitureDto()
        {

        }
        public VoitureDto(int? id, string immatriculation, string couleur, string modeleId)
        {
            Id = id;
            ModeleId = modeleId;
            Immatriculation = immatriculation;
            Couleur = couleur;
        }

        public VoitureDto(VoitureDto voiture)
        {
            Id = voiture.Id;
            ModeleId = voiture.ModeleId;
            Immatriculation = voiture.Immatriculation;
            Couleur = voiture.Couleur;
        }
    }
}
