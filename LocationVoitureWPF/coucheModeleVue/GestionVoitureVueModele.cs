using LocationVoitureWPF.classeMetier;
using LocationVoitureWPF.coucheAccesBD;
using LocationVoitureWPF.coucheModeleVue.Datas;
using LocationVoitureWPF.coucheModeleVue.DataTransferObjects;
using LocationVoitureWPF.coucheModeleVue.Enum;
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
    class GestionVoitureVueModele : INotifyPropertyChanged
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
        public ModeleDto modele { get; set; }
        public VoitureModeleDto voitureModeleDto { get; set; }
        public string intituleEmplacement { get; set; }

        ObservableCollection<VoitureModeleDto> collectionVoituresModeles;
        public ObservableCollection<VoitureModeleDto> CollectionVoituresModeles
        {
            get { return collectionVoituresModeles; }
            set
            {
                collectionVoituresModeles = value;
                OnPropertyChanged("CollectionVoituresModeles");
            }
        }

        public int? VoitureId
        {
            get { return this.voitureModeleDto.VoitureId; }
            set
            {
                this.voitureModeleDto.VoitureId = value;
                OnPropertyChanged("VoitureId");
            }
        }

        public string VoitureModeleId
        {
            get { return this.voitureModeleDto.VoitureModeleId; }
            set
            {
                this.voitureModeleDto.VoitureModeleId = value;
                OnPropertyChanged("VoitureModeleId");
            }
        }

        public string VoitureImmatriculation
        {
            get { return this.voitureModeleDto.VoitureImmatriculation; }
            set
            {
                this.voitureModeleDto.VoitureImmatriculation = value;
                OnPropertyChanged("VoitureImmatriculation");
            }
        }

        public string VoitureCouleur
        {
            get { return this.voitureModeleDto.VoitureCouleur; }
            set
            {
                this.voitureModeleDto.VoitureCouleur = value;
                OnPropertyChanged("VoitureCouleur");
            }
        }

        public string ModeleNom
        {
            get { return this.voitureModeleDto.ModeleNom; }
            set
            {
                this.voitureModeleDto.ModeleNom = value;
                OnPropertyChanged("ModeleNom");
            }
        }

        public string ModeleCategorieNom
        {
            get { return this.voitureModeleDto.ModeleCategorieNom; }
            set
            {
                this.voitureModeleDto.ModeleCategorieNom = value;
                OnPropertyChanged("ModeleCategorieNom");
            }
        }

        public string ModeleMarque
        {
            get { return this.voitureModeleDto.ModeleMarque; }
            set
            {
                this.voitureModeleDto.ModeleMarque = value;
                OnPropertyChanged("ModeleMarque");
            }
        }

        public string ModeleNbrSieges
        {
            get { return this.voitureModeleDto.ModeleNbrSieges; }
            set
            {
                this.voitureModeleDto.ModeleNbrSieges = value;
                OnPropertyChanged("ModeleNbrSieges");
            }
        }


        public ModeleDto ModeleDtoPicked
        {
            get { return this.modele; }
            set
            {
                this.modele = value;
                this.VoitureModeleId = value.Id.ToString();
                OnPropertyChanged("ModeleId");
            }
        }

        public string IntituleEmplacement
        {
            get { return this.intituleEmplacement; }
            set
            {
                this.intituleEmplacement = value;
                OnPropertyChanged("IntituleEmplacement");
            }
        }

        private AccesBD _accesBD;
        public GestionVoitureVueModele()
        {
            GetListVoituresModelesDto();
        }

        private void GetListVoituresModelesDto()
        {
            _accesBD = new AccesBD();
            
            this.voitureModeleDto = new VoitureModeleDto();

            List<Voiture> listVoitures = new List<Voiture>(); 
            List<Modele> listModeles = new List<Modele>();
            List<VoitureModeleDto> listVoituresModelesDto = new List<VoitureModeleDto>();
            collectionVoituresModeles = new ObservableCollection<VoitureModeleDto>(); 

            listVoitures = this._accesBD.ListVoitures();
            listModeles = this._accesBD.ListModeles();
            if(listVoitures != null && listModeles != null)
            {
                bool found;
                foreach (var v in listVoitures)
                {
                    found = false;
                    for (int i = 0; i < listModeles.Count() && !found; i++)
                    {
                        if (v.ModeleId == listModeles[i].Id)
                        {
                            voitureModeleDto = v.MapperToVoitureModeleDto(listModeles[i]);
                            collectionVoituresModeles.Add(voitureModeleDto);
                            found = true;
                        }
                    }
                }
            }
        }

        public static List<ModeleDto> ListModelesLoad()
        {
            AccesBD _accesBD = new AccesBD();
            List<Modele> listModeles; //Metier
            List<ModeleDto> ListModelesDto = new List<ModeleDto>(); //Dto

            listModeles = _accesBD.ListModeles();

            if (listModeles != null)
            {
                while (listModeles.Count > 0)
                {
                    ListModelesDto.Add(new Modele(listModeles[0]).ToDto());
                    listModeles.RemoveAt(0);
                }
            }
            return ListModelesDto;
        }

        public static List<string> ListEmplacementsLibresLoad()
        {
            AccesBD _accesBD = new AccesBD();

            List<string> listInitulesLibres = InituleesEmplacements.ListInituleesEmplacements();
            List<Emplacement> emplacementsPris;

            emplacementsPris = _accesBD.ListEmplacementPris();
            if(emplacementsPris != null)
                foreach (var e in emplacementsPris)
                {
                    if (listInitulesLibres.Contains(e.Intitule))
                    {
                        listInitulesLibres.Remove(e.Intitule);
                    }
                }
            return listInitulesLibres;
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

        private void Clear_voiture()
        {
            this.VoitureId = null;
            this.VoitureImmatriculation = null;
            this.VoitureModeleId = null;
            this.VoitureCouleur = null;
        }

        public string EffectuerAction(int index)
        {
            if (this.effectuer == "Effectuer l'ajout")
            {
                VoitureValidator validator = new VoitureValidator(voitureModeleDto);
                validator.Validate();

                if (!IsEmplacementFree(this.IntituleEmplacement))
                    throw new Exception($"L'emplacement sélectionné n'est pas libre.");
                

                if (DoesImmatriculationExistInDB(this.voitureModeleDto.VoitureImmatriculation))
                    throw new Exception($"L'immatriculation existe déjà en DB.");

                int resultat = this._accesBD.AjouterVoiture(this.voitureModeleDto.ToMetier());

                if (resultat != 0)
                {
                    VoitureDto voiture = _accesBD.GetVoiture(this.voitureModeleDto.VoitureImmatriculation).ToDto();
                    AjouterVoitureDansEmplacement(voiture.Id, this.IntituleEmplacement);
                }

                if (resultat != 0)
                {
                    GetListVoituresModelesDto();
                    collectionVoituresModeles.Clear();

                    this.Clear_voiture();
                    this.Effectuer = "";
                    return "L'ajout s'est bien déroulée.";
                }
            }

            if (this.effectuer == "Effectuer la suppression")
            {
                if (DoesImmatriculationExistInDB(this.voitureModeleDto.VoitureImmatriculation))
                {
                    int voitureId = this.voitureModeleDto.VoitureId == null ? -1 : (int)this.voitureModeleDto.VoitureId;

                    if(IsRent(voitureId))
                        throw new Exception($"La voiture que vous voulez supprimer est en cous de location. Suppression impossible !");

                    int resultat = this._accesBD.SupprimerVoiture(voitureId);

                    if (resultat != 0)
                    {
                        collectionVoituresModeles.Clear();
                        GetListVoituresModelesDto();

                        this.Clear_voiture();
                        this.Effectuer = "";
                        return "La suppression s'est bien déroulée.";
                    }
                }
            }
            return "Action inconnue ";
        }
        private void AjouterVoitureDansEmplacement(int? voitureId, string intituleEmplacement)
        {
            Emplacement emplacement = new Emplacement(intituleEmplacement, Convert.ToInt32(voitureId));
            int resultat = this._accesBD.AjouterEmplacement(emplacement);
            if (resultat != 1)
                throw new Exception("L'ajout de l'emplacement s'est mal produit.");
        }
        private bool DoesImmatriculationExistInDB(string immatriculation)
        {
            VoitureDto voitureFromDb = this._accesBD.GetVoiture(immatriculation).ToDto();
            if (voitureFromDb.Immatriculation == immatriculation)
                return true;
            return false;
        }

        private bool IsRent(int voitureId)
        {
            //VoitureDto voitureFromDb = this._accesBD.GetVoitureLouee(voitureId).ToDto();
            int resulat = this._accesBD.GetVoitureLouee(voitureId);
            //if (voitureFromDb.Id != null)
            //    return true;
            if (resulat != -1)
                return true;
            return false;
        }

        private bool IsEmplacementFree(string intituleEmplacement)
        {
            AccesBD _accesBD = new AccesBD();
            List<Emplacement> emplacementsPris;
            emplacementsPris = _accesBD.ListEmplacementPris();
            if(emplacementsPris != null)
                for (int i = 0; i < emplacementsPris.Count; i++)
                {
                    if (intituleEmplacement == emplacementsPris[i].Intitule)
                        return false;
                }
            return true;
        }
        public void Ajouter()
        {
            this.Clear_voiture();
            this.Effectuer = "Effectuer l'ajout";
        }

        public void Supprimer(int indexSelection)
        {
            VoitureId = collectionVoituresModeles.ElementAt(indexSelection).VoitureId;
            VoitureImmatriculation = collectionVoituresModeles.ElementAt(indexSelection).VoitureImmatriculation;
            this.Effectuer = "Effectuer la suppression";
        }
    }
}
