using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoitureWPF.classeMetier
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string NumPermisConduire { get; set; }
        public string Pays { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public string Adresse { get; set; }
        public int? Cp { get; set; }
        public Client()
        {

        }
        public Client(int id, string nom, string prenom, DateTime? dateNaissance,string numPermisConduire, 
                        string pays, string region, string ville, string adresse, int? cp)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            NumPermisConduire = numPermisConduire;
            Pays = pays;
            Region = region;
            Ville = ville;
            Adresse = adresse;
            Cp = cp;
        }

        public Client(Client client)
        {
            Id = client.Id;
            Nom = client.Nom;
            Prenom = client.Prenom;
            DateNaissance = client.DateNaissance;
            NumPermisConduire = client.NumPermisConduire;
            Pays = client.Pays;
            Region = client.Region;
            Ville = client.Ville;
            Adresse = client.Adresse;
            Cp = client.Cp;
        }

    }
}
