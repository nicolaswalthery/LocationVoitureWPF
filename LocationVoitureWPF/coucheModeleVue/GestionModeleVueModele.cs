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
    class GestionModeleVueModele : INotifyPropertyChanged
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

        ObservableCollection<ModeleDto> collectionModeles;
        public ObservableCollection<ModeleDto> CollectionModeles
        {
            get { return collectionModeles; }
            set
            {
                collectionModeles = value;
                OnPropertyChanged("collectionModeles");
            }
        }
        public ModeleDto modele { get; set; }

      

        public int? Id
        {
            get { return this.modele.Id; }
            set
            {
                this.modele.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string CategorieNom
        {
            get { return this.modele.CategorieNom; }
            set
            {
                this.modele.CategorieNom = value;
                OnPropertyChanged("CategorieNom");
            }
        }

        public string Marque
        {
            get { return this.modele.Marque; }
            set
            {
                this.modele.Marque = value;
                OnPropertyChanged("Marque");
            }
        }

        public string Nom
        {
            get { return this.modele.Nom; }
            set
            {
                this.modele.Nom = value;
                OnPropertyChanged("Nom");
            }
        }

        public string NbrSieges
        {
            get { return this.modele.NbrSieges; }
            set
            {
                this.modele.NbrSieges = value;
                OnPropertyChanged("NbrSieges");
            }
        }

        //Binding avec le btn ajouter
        public ICommand Click_Ajouter_Modele { get; set; }
        
        private AccesBD _accesBD;

        
        public GestionModeleVueModele()
        {
            _accesBD = new AccesBD();
            this.modele = new ModeleDto(); //Dto
            List<Modele> listModeles; //Metier
            collectionModeles = new ObservableCollection<ModeleDto>(); //Dto

            listModeles = this._accesBD.ListModeles();

            if (listModeles != null)
            {
                while (listModeles.Count > 0)
                {
                    //Mapper to dto
                    collectionModeles.Add(new Modele(listModeles[0]).ToDto());
                    listModeles.RemoveAt(0);
                }
            }
        }
        public static List<string> ListCategoriesNomsLoad()
        {
            AccesBD _accesBD = new AccesBD();
            CategorieDto categorie = new CategorieDto(); //Dto
            List<Categorie> listCategories; //Metier
            List<string> ListCategoriesNoms = new List<string>(); 

            listCategories = _accesBD.ListCategories();

            if (listCategories != null)
            {
                while (listCategories.Count > 0)
                {
                    ListCategoriesNoms.Add(new Categorie(listCategories[0]).Nom);
                    listCategories.RemoveAt(0);
                }
            }

            return ListCategoriesNoms;
        }

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

        private void Clear_categorie()
        {
            this.Id = null;
            this.CategorieNom = null;
            this.Marque = null;
            this.Nom = null;
            this.NbrSieges = null;
        }



        // Cette méthode est utlisée lorsqu'on clique sur le bouton d'action
        public string EffectuerAction(int index)
        {
            ModeleValidator validator = new ModeleValidator(modele);
            validator.Validate();

            if (this.effectuer == "Effectuer l'ajout")
            {
                DoesExistInDB(this.modele);

                int resultat = this._accesBD.AjouterModele(modele.ToMetier());

                if (resultat != 0)
                {
                    List<Modele> listModeles;
                    listModeles = this._accesBD.ListModeles();
                    collectionModeles.Clear();

                    if (listModeles != null)
                    {
                        while (listModeles.Count > 0)
                        {
                            collectionModeles.Add(new Modele(listModeles[0]).ToDto());
                            listModeles.RemoveAt(0);
                        }
                    }

                    this.Clear_categorie();
                    this.Effectuer = "";
                    return "L'ajout s'est bien déroulée.";
                }
            }
            return "Action inconnue ";
        }
        private void DoesExistInDB(ModeleDto modele)
        {
            ModeleDto modeleDto = this._accesBD.CheckModele(modele.ToMetier()).ToDto();
            if (modeleDto.Nom == modele.Nom && modeleDto.Marque == modele.Marque && modele.NbrSieges == modeleDto.NbrSieges)
                throw new Exception($"Le modele ({modele.Nom} / {modele.Marque}) est déjà encodé en DB.");
        }
        public void Ajouter()
        {
            this.Clear_categorie();
            this.Effectuer = "Effectuer l'ajout";
        }
    }
}
