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
    class GestionCategorieVueModele : INotifyPropertyChanged
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

        // collection binding avec la liste affiché dans la vue
        ObservableCollection<CategorieDto> collectionCategories;
        public ObservableCollection<CategorieDto> CollectionCategories
        {
            get { return collectionCategories; }
            set
            {
                collectionCategories = value;
                OnPropertyChanged("collectionCategories");
            }
        }


        public CategorieDto categorie { get; set; }

        public string Nom
        {
            get { return this.categorie.Nom; }
            set
            {
                this.categorie.Nom = value;
                OnPropertyChanged("Nom");
            }
        }

        public string PrixJour
        {
            get { return this.categorie.PrixJour; }
            set
            {
                this.categorie.PrixJour = value;
                OnPropertyChanged("PrixJour");
            }
        }

        //Binding avec le btn ajouter
        public ICommand Click_Ajouter_Categories { get; set; }
        
        private AccesBD _accesBD;

        
        public GestionCategorieVueModele()
        {
            _accesBD = new AccesBD();
            this.categorie = new CategorieDto(); //Dto
            List<Categorie> listCategories; //Metier
            collectionCategories = new ObservableCollection<CategorieDto>(); //Dto

            listCategories = this._accesBD.ListCategories();

            if (listCategories != null)
            {
                while (listCategories.Count > 0)
                {
                    //Mapper to dto
                    collectionCategories.Add(new Categorie(listCategories[0]).ToDto());
                    listCategories.RemoveAt(0);
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

        //public void Modifier(int indexSelection)
        //{
        //    Id = collectionClients.ElementAt(indexSelection).Id;
        //    Nom = collectionClients.ElementAt(indexSelection).Nom;
        //    Prenom = collectionClients.ElementAt(indexSelection).Prenom;
        //    NumPermisConduire = collectionClients.ElementAt(indexSelection).NumPermisConduire;
        //    Adresse = collectionClients.ElementAt(indexSelection).Adresse;
        //    Pays = collectionClients.ElementAt(indexSelection).Pays;
        //    Region = collectionClients.ElementAt(indexSelection).Region;
        //    Ville = collectionClients.ElementAt(indexSelection).Ville;
        //    DateNaissance = collectionClients.ElementAt(indexSelection).DateNaissance;
        //    Cp = collectionClients.ElementAt(indexSelection).Cp;
        //    // Mise à jour du bouton d'action
        //    this.Effectuer = "Effectuer la modification";
        //}


        // Cette méthode met à vide les champs du client à modifier 
        private void Clear_categorie()
        {
            this.Nom = null;
            this.PrixJour = null;
        }



        // Cette méthode est utlisée lorsqu'on clique sur le bouton d'action
        public string EffectuerAction(int index)
        {
            CategorieValidator validator = new CategorieValidator(categorie);
            validator.Validate();

            //if (this.effectuer == "Effectuer la modification")
            //{
            //    // met à jour la personne dans la base de données.
            //    int resultat = this._accesBD.UpdateClient(client.ToMetier());

            //    if (resultat != 0)
            //    {
            //        // Mise à jour de la liste des personnes affichée
            //        collectionClients[index] = this.client;

            //        // initialisation des champs de la personne
            //        // à modifier ou à ajouter
            //        this.Clear_personne();

            //        this.Effectuer = "";

            //        return "La modification s'est bien déroulée.";
            //    }
            //    return "La modification ne s'est pas bien déroulée.";
            //}

            if (this.effectuer == "Effectuer l'ajout")
            {
                if (DoesExistInDB(this.categorie.Nom))
                    throw new Exception("Ce nom de catégorie existe déjà en DB.");

                int resultat = this._accesBD.AjouterCategorie(categorie.ToMetier());

                if (resultat != 0)
                {
                    // Mise à jour de la liste des personnes affichée ordonné par nom et prénom
                    List<Categorie> listCategories;
                    listCategories = this._accesBD.ListCategories();
                    collectionCategories.Clear();


                    // prépare la liste des personnes à afficher 
                    if (listCategories != null)
                    {
                        while (listCategories.Count > 0)
                        {
                            collectionCategories.Add(new Categorie(listCategories[0]).ToDto());
                            listCategories.RemoveAt(0);
                        }
                    }

                    // Initialisation à vide des champs de la personne à modifier ou à ajouter
                    this.Clear_categorie();
                    this.Effectuer = "";
                    return "L'ajout s'est bien déroulée.";
                }
            }
            return "Action inconnue ";
        }

        private bool DoesExistInDB(string catgorieNom)
        {
            if (this._accesBD.GetCategorie(catgorieNom).Nom != null)
                return true;
            return false;
        }

        // méthode utilisée lorsqu'on clique sur le bouton Ajouter
        public void Ajouter()
        {
            // Met les champs de la nouvelle personne à vide
            this.Clear_categorie();

            // Mise à jour du bouton d'action
            this.Effectuer = "Effectuer l'ajout";
        }
    }
}
