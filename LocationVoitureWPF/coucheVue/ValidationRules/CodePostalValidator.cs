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
    class CodePostalValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (!Regex.IsMatch(value.ToString(), "^[1-9]{1}[0-9]{2}[0-9]+$"))
                {
                    return new ValidationResult(false, " ne doit contenir que des chiffres positifs sans virgule.");
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
