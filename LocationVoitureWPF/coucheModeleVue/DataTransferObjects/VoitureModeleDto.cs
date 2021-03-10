using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.DataTransferObjects
{
    public class VoitureModeleDto
    {
        public int? VoitureId { get; set; }
        public string VoitureModeleId { get; set; }
        public string VoitureImmatriculation { get; set; }
        public string VoitureCouleur { get; set; }

        //public int? Id { get; set; }
        public string ModeleCategorieNom { get; set; }
        public string ModeleMarque { get; set; }
        public string ModeleNom { get; set; }
        public string ModeleNbrSieges { get; set; }
        public VoitureModeleDto()
        {

        }

        public VoitureModeleDto(int? voitureId, string voitureModeleId, string voitureImmatriculation, string voitureCouleur, 
            string modeleCategorieNom, string modeleMarque, string modeleNom, string modeleNbrSieges)
        {
            VoitureId = voitureId;
            VoitureModeleId = voitureModeleId;
            VoitureImmatriculation = voitureImmatriculation;
            VoitureCouleur = voitureCouleur;
            ModeleCategorieNom = modeleCategorieNom;
            ModeleMarque = modeleMarque;
            ModeleNom = modeleNom;
            ModeleNbrSieges = modeleNbrSieges;
        }


        public VoitureModeleDto(VoitureModeleDto voitureModeleDto)
        {
            VoitureId = voitureModeleDto.VoitureId;
            VoitureModeleId = voitureModeleDto.VoitureModeleId;
            VoitureImmatriculation = voitureModeleDto.VoitureImmatriculation;
            VoitureCouleur = voitureModeleDto.VoitureCouleur;
            ModeleCategorieNom = voitureModeleDto.ModeleCategorieNom;
            ModeleMarque = voitureModeleDto.ModeleMarque;
            ModeleNom = voitureModeleDto.ModeleNom;
            ModeleNbrSieges = voitureModeleDto.ModeleNbrSieges;
        }
    }
}
