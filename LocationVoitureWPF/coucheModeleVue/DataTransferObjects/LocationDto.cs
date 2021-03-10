using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.DataTransferObjects
{
    public class LocationDto
    {
        public int? VoitureId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateLocation { get; set; }
        public DateTime DateRetour { get; set; }
        public bool EstRendue { get; set; }
        public string Montant { get; set; }
        public LocationDto()
        {

        }
        public LocationDto(int? voitureId, int clientId, DateTime dateLocation, DateTime dateRetour,
                        bool estRendue, string montant)
        {
            VoitureId = voitureId;
            ClientId = clientId;
            DateLocation = dateLocation;
            DateRetour = dateRetour;
            EstRendue = estRendue;
            Montant = montant;
        }

        public LocationDto(LocationDto location)
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
