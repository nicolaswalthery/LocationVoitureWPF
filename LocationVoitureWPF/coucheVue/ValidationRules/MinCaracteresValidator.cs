﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Windows.Controls;

namespace LocationVoitureWPF.coucheVue.ValidationRules
{
    class MinCaracteresValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string nom;
            try
            {
                nom = value.ToString();
                if (nom.Length < 3)
                {
                    return new ValidationResult(false, " doit contenir au moins 3 caractères");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Ce champ doit être complété");
            }
            return new ValidationResult(true, null);
        }
    }
}
