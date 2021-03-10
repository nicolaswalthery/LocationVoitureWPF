using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace LocationVoitureWPF.coucheVue.ValidationRules
{
    class OnlyLettersValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (!Regex.IsMatch(value.ToString(), "^[a-zA-Zéè]+$"))
                {
                    return new ValidationResult(false, " ne doit contenir que des lettres.");
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
