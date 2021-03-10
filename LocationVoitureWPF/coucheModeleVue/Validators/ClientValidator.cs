using LocationVoitureWPF.coucheModeleVue.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LocationVoitureWPF.coucheModeleVue.Validators
{
    class ClientValidator
    {
        private ClientDto _client;
        public ClientValidator(ClientDto client)
        {
            _client = client;
        }

        public bool Validate()
        {
            if (IsFieldsFilled() && HasMinimumCharacterRequired() && IsAgeValid() && IsPostalCodeValid()) 
                return true;
            return false;
        }
        private bool IsFieldsFilled()
        {
            if (_client.Nom != null && _client.Prenom != null && _client.NumPermisConduire != null && _client.Pays != null
                && _client.Ville != null && _client.Cp != null && _client.DateNaissance != null && _client.Adresse != null
                && _client.Region != null)
                return true;
            else
                throw new Exception("Un des champs du client n'est pas rempli.");
        }

        public bool HasMinimumCharacterRequired()
        {
            if (_client.Nom.Length < 3)
            {
                throw new Exception("Le nom doit contenir au moins 3 caractères");
            }
            if (_client.Prenom.Length < 3)
            {
                throw new Exception("Le prénom doit contenir au moins 3 caractères");
            }
            return true;
        }
        private bool IsAgeValid()
        {
            if (!CheckAge(_client.DateNaissance))
            {
                throw new Exception("Le client doit avoir 21 ans ou plus pour s'inscrire.");
            }
            return true;
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
            if (CalculAge(dateNaissance) >= 21)
                return true;
            return false;
        }

        private bool IsPostalCodeValid()
        {
            if (!Regex.IsMatch(_client.Cp, "^[1-9]{1}[0-9]{2}[0-9]+$"))
            {
                throw new Exception ("Le code postal ne doit contenir que des chiffres positifs sans virgule.");
            }
            return true;
        }
    }
}
