using LocationVoitureWPF.coucheModeleVue.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.Validators
{
    public class ModeleValidator
    {
        private ModeleDto _modele;
        public ModeleValidator(ModeleDto modele)
        {
            _modele = modele;
        }

        public bool Validate()
        {
            if (IsFilled(_modele.Nom, nameof(_modele.Nom)) && IsFilled(_modele.CategorieNom, nameof(_modele.CategorieNom)) &&
                IsFilled(_modele.Marque, nameof(_modele.Marque)) && IsFilled(_modele.NbrSieges, nameof(_modele.NbrSieges)) && IsPositive())
                return true;
            return false;
        }

        private bool IsFilled(string field, string nameField)
        {
            if (nameField == "CategorieNom")
                nameField = "Nom de la catégorie";
            if (nameField == "NbrSieges")
                nameField = "Le nombre de sièges";

            if (field == null || field == String.Empty)
                throw new Exception($"{nameField} ne peut être vide.");
            return true;
        }
        private bool IsPositive()
        {
            if (Convert.ToInt32(_modele.NbrSieges) > 0)
                return true;
            else
                throw new Exception($"Le champs \"Prix/Jour\" ne peut être inférieur à 1.");
        }
    }
}
