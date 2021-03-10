using LocationVoitureWPF.coucheModeleVue.DataTransferObjects;
using LocationVoitureWPF.coucheModeleVue.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.Validators
{
    public class VoitureValidator
    {
        private VoitureModeleDto _voiture;
        public VoitureValidator(VoitureModeleDto voiture)
        {
            _voiture = voiture;
        }

        public bool Validate()
        {
            if (IsFilled(_voiture.VoitureCouleur, nameof(_voiture.VoitureCouleur)) && IsFilled(_voiture.VoitureImmatriculation, nameof(_voiture.VoitureImmatriculation)))
                return true;
            return false;
        }

        private bool IsFilled(string field, string nameField)
        {
            if (field == null || field == String.Empty)
                throw new Exception($"{nameField} ne peut être vide.");
            return true;
        }
    }
}
