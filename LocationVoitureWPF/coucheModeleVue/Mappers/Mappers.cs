using LocationVoitureWPF.classeMetier;
using LocationVoitureWPF.coucheModeleVue.DataTransferObjects;
using LocationVoitureWPF.coucheModeleVue.Enum;
using System;

namespace LocationVoitureWPF.coucheModeleVue.Mappers
{
    public static class Mappers
    {
        public static Client ToMetier(this ClientDto input)
        {
            return new Client
            {
                Id = input.Id,
                Nom = input.Nom,
                Prenom = input.Prenom,
                DateNaissance = input.DateNaissance,
                NumPermisConduire = input.NumPermisConduire,
                Pays = input.Pays,
                Region = input.Region,
                Ville = input.Ville,
                Adresse = input.Adresse,
                Cp = Convert.ToInt32(input.Cp)
            };
        }

        public static ClientDto ToDto(this Client input)
        {
            return new ClientDto
            {
                Id = input.Id,
                Nom = input.Nom,
                Prenom = input.Prenom,
                DateNaissance = input.DateNaissance == null ? DateTime.Today : (DateTime)input.DateNaissance,
                NumPermisConduire = input.NumPermisConduire,
                Pays = input.Pays,
                Region = input.Region,
                Ville = input.Ville,
                Adresse = input.Adresse,
                Cp = input.Cp.ToString()
            };
        }

        public static Categorie ToMetier(this CategorieDto input)
        {
            return new Categorie
            {
                Nom = input.Nom,
                PrixJour = input.PrixJour.Contains(".") ? Decimal.Parse(input.PrixJour.Replace(".", ",")) : Decimal.Parse(input.PrixJour)
            };
        }

        public static CategorieDto ToDto(this Categorie input)
        {
            return new CategorieDto
            {
                Nom = input.Nom,
                PrixJour = input.PrixJour.ToString()
            };
        }

        public static Modele ToMetier(this ModeleDto input)
        {
            return new Modele
            {
                Id = input.Id,
                CategorieNom  = input.CategorieNom,
                Marque = input.Marque,
                Nom = input.Nom,
                NbrSieges = Convert.ToInt32(input.NbrSieges)
            };
        }

        public static ModeleDto ToDto(this Modele input)
        {
            return new ModeleDto
            {
                Id = input.Id,
                CategorieNom = input.CategorieNom,
                Marque = input.Marque,
                Nom = input.Nom,
                NbrSieges = input.NbrSieges.ToString()
            };
        }

        public static Emplacement ToMetier(this EmplacementDto input)
        {
            return new Emplacement
            {
                Intitule = input.Intitule,
                VoitureId = Convert.ToInt32(input.VoitureId)
            };
        }

        public static EmplacementDto ToDto(this Emplacement input)
        {
            return new EmplacementDto
            {
                Intitule = input.Intitule,
                VoitureId = input.VoitureId.ToString()
            };
        }

        public static Location ToMetier(this LocationDto input)
        {
            return new Location
            {
                VoitureId = Convert.ToInt32(input.VoitureId),
                ClientId = Convert.ToInt32(input.ClientId),
                DateLocation = input.DateLocation,
                DateRetour = input.DateRetour,
                EstRendue = input.EstRendue,
                Montant = input.Montant.Contains(".") ? Decimal.Parse(input.Montant.Replace(".", ",")) : Decimal.Parse(input.Montant)
            };
        }

        public static LocationDto ToDto(this Location input)
        {
            return new LocationDto
            {
                VoitureId = input.VoitureId,
                ClientId = input.ClientId,
                DateLocation = input.DateLocation,
                DateRetour = input.DateRetour,
                EstRendue = input.EstRendue,
                Montant = input.Montant.ToString()
            };
        }

        public static Voiture ToMetier(this VoitureDto input)
        {
            return new Voiture
            {
                Id = input.Id == null ? 0 : input.Id,
                ModeleId = Convert.ToInt32(input.ModeleId),
                Immatriculation = input.Immatriculation,
                Couleur = input.Couleur
            };
        }

        public static VoitureDto ToDto(this Voiture input)
        {
            return new VoitureDto
            {
                Id = input.Id,
                ModeleId = input.ModeleId.ToString(),
                Immatriculation = input.Immatriculation,
                Couleur = input.Couleur
            };
        }

        public static VoitureModeleDto MapperToVoitureModeleDto(this Voiture voiture, Modele modele)
        {
            return new VoitureModeleDto
            {
                VoitureId = voiture.Id,
                VoitureModeleId = voiture.ModeleId.ToString(),
                VoitureImmatriculation = voiture.Immatriculation,
                VoitureCouleur = voiture.Couleur,
                ModeleCategorieNom = modele.CategorieNom,
                ModeleMarque = modele.Marque,
                ModeleNom = modele.Nom,
                ModeleNbrSieges = modele.NbrSieges.ToString(),
            };
        }
        public static Voiture ToMetier(this VoitureModeleDto input)
        {
            return new Voiture
            {
                Id = input.VoitureId == null ? 0 : input.VoitureId,
                ModeleId = Convert.ToInt32(input.VoitureModeleId),
                Immatriculation = input.VoitureImmatriculation,
                Couleur = input.VoitureCouleur
            };
        }
    }
}
