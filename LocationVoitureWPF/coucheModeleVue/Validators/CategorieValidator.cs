using LocationVoitureWPF.classeMetier;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationVoitureWPF.coucheModeleVue.Validators
{
    public class CategorieValidator
    {
        private CategorieDto _categorie;
        public CategorieValidator(CategorieDto categorie)
        {
            _categorie = categorie;
        }

        public bool Validate()
        {
            if (IsNomFilled() && IsPrixJourFilled() && IsPositive())
                return true;
            return false;
        }

        private bool IsNomFilled()
        {
            if (_categorie.Nom != null )
                return true;
            else
                throw new Exception("Le champs Nom est vide.");
        }

        private bool IsPrixJourFilled()
        {
            if (_categorie.PrixJour != null)
                return true;
            else
                throw new Exception("Le champs Prix/Jour est vide.");
        }

        private bool IsPositive()
        {
            if (Convert.ToDecimal(_categorie.PrixJour) > 0m)
                return true;
            else
                throw new Exception($"Le champs \"Prix/Jour\" ne peut être négatif.");
        }
    }
}
