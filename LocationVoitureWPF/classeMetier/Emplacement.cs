using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.classeMetier
{
    public class Emplacement
    {
        public string Intitule { get; set; }
        public int? VoitureId { get; set; }
        public Emplacement()
        {

        }
        public Emplacement(string intitule, int? voitureId)
        {
            Intitule = intitule;
            VoitureId = voitureId;
        }

        public Emplacement(Emplacement emplacement)
        {
            Intitule = emplacement.Intitule;
            VoitureId = emplacement.VoitureId;
        }
    }
}
