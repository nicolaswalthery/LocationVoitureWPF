using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.DataTransferObjects
{
    public class EmplacementDto
    {
        public string Intitule { get; set; }
        public string VoitureId { get; set; }
        public EmplacementDto()
        {

        }
        public EmplacementDto(string intitule, string voitureId)
        {
            Intitule = intitule;
            VoitureId = voitureId;
        }

        public EmplacementDto(EmplacementDto emplacement)
        {
            Intitule = emplacement.Intitule;
            VoitureId = emplacement.VoitureId;
        }
    }
}
