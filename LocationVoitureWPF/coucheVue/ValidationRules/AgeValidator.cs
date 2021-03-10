using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Windows.Controls;

namespace LocationVoitureWPF.coucheVue.ValidationRules
{
    class AgeValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime dateNaissance;
            try
            {
                dateNaissance = (DateTime)value;
                if (!CheckAge(dateNaissance))
                {
                    return new ValidationResult(false, " doit avoir 21 ans ou plus et en-dessous de 101 ans pour s'inscrire.");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Ce champ doit être complété");
            }
            return new ValidationResult(true, null);
        }

        private int CalculAge(DateTime dateNaissance)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateNaissance.Year;
            if (dateNaissance.Date > today.AddYears(-age)) age--;
            return age;
        }

        private bool CheckAge(DateTime dateNaissance)
        {
            int age = CalculAge(dateNaissance);
            if (age > 20 && age < 101)
                return true;
            return false;
        }
    }
}
