using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.classeMetier
{
    public class Location
    {
        public int VoitureId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateLocation { get; set; }
        public DateTime DateRetour { get; set; }
        public bool EstRendue { get; set; }
        public decimal Montant { get; set; }
        public Location()
        {

        }
        public Location(int voitureId, int clientId, DateTime dateLocation, DateTime dateRetour,
                        bool estRendue, decimal montant)
        {
            VoitureId = voitureId;
            ClientId = clientId;
            DateLocation = dateLocation;
            DateRetour = dateRetour;
            EstRendue = estRendue;
            Montant = montant;
        }

        public Location(Location location)
        {
            VoitureId = location.VoitureId;
            ClientId = location.ClientId;
            DateLocation = location.DateLocation;
            DateRetour = location.DateRetour;
            EstRendue = location.EstRendue;
            Montant = location.Montant;
        }
    }
}
