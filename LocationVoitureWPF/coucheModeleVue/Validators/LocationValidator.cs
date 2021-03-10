using LocationVoitureWPF.coucheModeleVue.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.Validators
{
    public class LocationValidator
    {
        private LocationDto _location;
        public LocationValidator(LocationDto location)
        {
            _location = location;
        }

        public bool Validate()
        {
            if (/*IsDateLocationNotFromPast() &&*/ IsDateRetourValid() && IsDateFilled(_location.DateLocation, nameof(_location.DateLocation)) 
                && IsDateFilled(_location.DateRetour, nameof(_location.DateRetour)) && IsFieldFilled(_location.VoitureId, nameof(_location.VoitureId))
                && IsFieldFilled(_location.ClientId, nameof(_location.ClientId)))
                return true;
            return false;
        }

        //private bool IsDateLocationNotFromPast()
        //{
        //    if (_location.DateLocation < DateTime.Today)
        //        throw new Exception("La date du début de la location est invalide.");
        //    return true;
        //}

        private bool IsDateRetourValid()
        {
            if (_location.DateRetour < DateTime.Today /*_location.DateLocation*/)
                throw new Exception("La date de retour ne peut être inférieur à la date de location.");
            return true;
        }

        private bool IsDateFilled(DateTime field, string nameField)
        {
            if (field == null)
                throw new Exception($"{nameField} ne peut être vide.");
            return true;
        }
        private bool IsFieldFilled(int? field, string nameField)
        {
            if (field == null)
                throw new Exception($"{nameField} ne peut être vide.");
            return true;
        }

    }
}
