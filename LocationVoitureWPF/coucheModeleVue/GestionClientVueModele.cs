using LocationVoitureWPF.classeMetier;
using LocationVoitureWPF.coucheAccesBD;
using LocationVoitureWPF.coucheModeleVue.DataTransferObjects;
using LocationVoitureWPF.coucheModeleVue.Mappers;
using LocationVoitureWPF.coucheModeleVue.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LocationVoitureWPF.coucheModeleVue
{
    class GestionClientVueModele : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        ObservableCollection<ClientDto> collectionClients;
        public ObservableCollection<ClientDto> CollectionClients
        {
            get { return collectionClients; }
            set
            {
                collectionClients = value;
                OnPropertyChanged("collectionClients");
            }
        }

        public ClientDto client { get; set; }

        public int Id
        {
            get { return this.client.Id; }
            set
            {
                this.client.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Nom
        {
            get { return this.client.Nom; }
            set
            {
                this.client.Nom = value;
                OnPropertyChanged("Nom");
            }
        }

        public string Prenom
        {
            get { return this.client.Prenom; }
            set
            {
                this.client.Prenom = value;
                OnPropertyChanged("Prenom");
            }
        }

        public string NumPermisConduire
        {
            get { return this.client.NumPermisConduire; }
            set
            {
                this.client.NumPermisConduire = value;
                OnPropertyChanged("NumPermisConduire");
            }
        }

        public string Adresse
        {
            get { return this.client.Adresse; }
            set
            {
                this.client.Adresse = value;
                OnPropertyChanged("Adresse");
            }
        }

        // Propriété 
        public string Cp
        {
            get { return this.client.Cp; }
            set
            {
                this.client.Cp = value;
                OnPropertyChanged("Cp");
            }
        }

        // Propriété 
        public DateTime DateNaissance
        {
            get { return this.client.DateNaissance; }
            set
            {
                this.client.DateNaissance = value;
                OnPropertyChanged("DateNaissance");
            }
        }

        // Propriété 
        public string Pays
        {
            get { return this.client.Pays; }
            set
            {
                this.client.Pays = value;
                OnPropertyChanged("Pays");
            }
        }

        // Propriété 
        public string Region
        {
            get { return this.client.Region; }
            set
            {
                this.client.Region = value;
                OnPropertyChanged("Region");
            }
        }

        // Propriété 
        public string Ville
        {
            get { return this.client.Ville; }
            set
            {
                this.client.Ville = value;
                OnPropertyChanged("Ville");
            }
        }

        //Binding avec le btn ajouter
        public ICommand Click_Ajouter_Client { get; set; }
        
        private AccesBD _accesBD;

        
        public GestionClientVueModele()
        {
            _accesBD = new AccesBD();
            this.client = new ClientDto(); //Dto
            List<Client> listClients; //Metier
            collectionClients = new ObservableCollection<ClientDto>(); //Dto
            
            listClients = this._accesBD.ListClients();

            // prépare la liste des personnes à afficher 
            if (listClients != null)
            {
                while (listClients.Count > 0)
                {
                    //Mapper to dto
                    collectionClients.Add(new Client(listClients[0]).ToDto());
                    listClients.RemoveAt(0);
                }
            }
        }

        // propriété effectuer: contenu du bouton d'action

        public string effectuer;
        public string Effectuer
        {
            get { return this.effectuer; }
            set
            {
                this.effectuer = value;
                OnPropertyChanged("Effectuer");
            }
        }

        public void Modifier(int indexSelection)
        {
            Id = collectionClients.ElementAt(indexSelection).Id;
            Nom = collectionClients.ElementAt(indexSelection).Nom;
            Prenom = collectionClients.ElementAt(indexSelection).Prenom;
            NumPermisConduire = collectionClients.ElementAt(indexSelection).NumPermisConduire;
            Adresse = collectionClients.ElementAt(indexSelection).Adresse;
            Pays = collectionClients.ElementAt(indexSelection).Pays;
            Region = collectionClients.ElementAt(indexSelection).Region;
            Ville = collectionClients.ElementAt(indexSelection).Ville;
            DateNaissance = collectionClients.ElementAt(indexSelection).DateNaissance;
            Cp = collectionClients.ElementAt(indexSelection).Cp;

            this.Effectuer = "Effectuer la modification";
        }


        // Cette méthode met à vide les champs du client à modifier 
        private void Clear_personne()
        {
            this.Id = -1;
            this.Nom = null;
            this.Prenom = null;
            this.NumPermisConduire = null;
            this.Pays = null;
            this.Region = null;
            this.Ville = null;
            this.Cp = null;
            this.DateNaissance = DateTime.Today;
            this.Adresse = null;
        }



        // Cette méthode est utlisée lorsqu'on clique sur le bouton d'action
        public string EffectuerAction(int index)
        {
            ClientValidator validator = new ClientValidator(client);
            validator.Validate();

            if (this.effectuer == "Effectuer la modification")
            {
                // met à jour la personne dans la base de données.
                int resultat = this._accesBD.UpdateClient(client.ToMetier());

                if (resultat != 0)
                {
                    // Mise à jour de la liste des personnes affichée
                    collectionClients[index] = this.client;

                    // initialisation des champs de la personne
                    // à modifier ou à ajouter
                    this.Clear_personne();

                    this.Effectuer = "";

                    return "La modification s'est bien déroulée.";
                }
                return "La modification ne s'est pas bien déroulée.";
            }

            if (this.effectuer == "Effectuer l'ajout")
            {
                if (DoesExistInDB(client.NumPermisConduire))
                    throw new Exception("Ce numéro de permis existe déjà en DB.");

                int resultat = this._accesBD.AjouterClient(client.ToMetier());

                if (resultat != 0)
                {
                    // Mise à jour de la liste des personnes affichée ordonné par nom et prénom
                    List<Client> listClients;
                    listClients = this._accesBD.ListClients();
                    collectionClients.Clear();


                    // prépare la liste des personnes à afficher 
                    if (listClients != null)
                    {
                        while (listClients.Count > 0)
                        {
                            collectionClients.Add(new Client(listClients[0]).ToDto());
                            listClients.RemoveAt(0);
                        }
                    }

                    // Initialisation à vide des champs de la personne à modifier ou à ajouter
                    this.Clear_personne();
                    this.Effectuer = "";
                    return "L'ajout s'est bien déroulée.";
                }
            }
            return "Action inconnue ";
        }

        private bool DoesExistInDB(string numPermis)
        {
            if (this._accesBD.GetClient(numPermis).NumPermisConduire != null)
                return true;
            return false;
        }
       
        public void Ajouter()
        {
            this.Clear_personne();

            this.Effectuer = "Effectuer l'ajout";
        }
    }
}
