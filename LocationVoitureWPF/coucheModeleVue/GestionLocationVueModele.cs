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
    class GestionLocationVueModele : INotifyPropertyChanged
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
        LocationDto location { get; set; }
        public ClientDto client { get; set; }
        public VoitureModeleDto voitureModeleDto { get; set; }

        ObservableCollection<VoitureModeleDto> collectionVoituresModelesDisponibles;
        public ObservableCollection<VoitureModeleDto> CollectionVoituresModelesDisponibles
        {
            get { return collectionVoituresModelesDisponibles; }
            set
            {
                collectionVoituresModelesDisponibles = value;
                OnPropertyChanged("CollectionVoituresModelesDisponibles");
            }
        }

        ObservableCollection<VoitureModeleDto> collectionVoituresModelesLouees;
        public ObservableCollection<VoitureModeleDto> CollectionVoituresModelesLouees
        {
            get { return collectionVoituresModelesLouees; }
            set
            {
                collectionVoituresModelesLouees = value;
                OnPropertyChanged("CollectionVoituresModelesLouees");
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


        public ClientDto ClientPicked
        {
            get { return this.client; }
            set
            {
                this.client = value;
                this.location.ClientId = value.Id;
                OnPropertyChanged("ClientPicked");
            }
        }

        public DateTime DateLocation
        {
            get { return this.location.DateLocation; }
            set
            {
                this.location.DateLocation = value;
                OnPropertyChanged("DateLocation");
            }
        }

        public DateTime DateRetour
        {
            get { return this.location.DateRetour; }
            set
            {
                this.location.DateRetour = value;
                OnPropertyChanged("DateRetour");
            }
        }

        private AccesBD _accesBD;
        public GestionLocationVueModele()
        {
            GetListVoituresModelesDto();
        }

        private void GetListVoituresModelesDto()
        {
            _accesBD = new AccesBD();

            this.voitureModeleDto = new VoitureModeleDto();
            this.location = new LocationDto();

            List<Voiture> listVoitures = new List<Voiture>();
            List<Modele> listModeles = new List<Modele>();
            List<Voiture> listVoituresLouees = new List<Voiture>();
            List<VoitureModeleDto> listVoituresModelesDto = new List<VoitureModeleDto>();


            listVoitures = this._accesBD.ListVoitures();
            listModeles = this._accesBD.ListModeles();
            listVoituresLouees = this._accesBD.ListVoituresLouees();
            
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
                            listVoituresModelesDto.Add(voitureModeleDto);
                            found = true;
                        }
                    }
                }
                ObservableCollectionLoad(listVoituresModelesDto, listVoituresLouees);
            }
        }

        private void ObservableCollectionLoad(List<VoitureModeleDto> listVoituresModeles, List<Voiture> voituresLouees)
        {
            collectionVoituresModelesDisponibles = new ObservableCollection<VoitureModeleDto>();
            collectionVoituresModelesLouees = new ObservableCollection<VoitureModeleDto>();

            if (listVoituresModeles != null)
            {
                bool rent;
                foreach (var v in listVoituresModeles)
                {
                    rent = false;
                    if (voituresLouees != null)
                        for (int i = 0; i < voituresLouees.Count() && !rent; i++)
                        {
                            if (v.VoitureId == voituresLouees[i].Id)
                            {
                                collectionVoituresModelesLouees.Add(v);
                                rent = true;
                            }
                            else
                            {
                                collectionVoituresModelesDisponibles.Add(v);
                            }
                        }
                    else
                        collectionVoituresModelesDisponibles.Add(v);
                }
            }
        }
        
        public static List<string> ListEmplacementsLibresLoad()
        {
            AccesBD _accesBD = new AccesBD();

            List<string> listInitulesLibres = InituleesEmplacements.ListInituleesEmplacements();
            List<Emplacement> emplacementsPris; 

            emplacementsPris = _accesBD.ListEmplacementPris();
            if(emplacementsPris != null)
                foreach(var e in emplacementsPris)
                {
                    if (listInitulesLibres.Contains(e.Intitule))
                    {
                        listInitulesLibres.Remove(e.Intitule);
                    }
                }
            return listInitulesLibres;
        }

        public static List<ClientDto> ListClientLoad()
        {
            AccesBD _accesBD = new AccesBD();
            List<ClientDto> listClients = _accesBD.ListClients().Select(c => c.ToDto()).ToList();
            return listClients;
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

        public string EffectuerAction()
        {
            if (this.effectuer == "Louer")
            {
                LocationValidator validator = new LocationValidator(location);
                validator.Validate();

                decimal prix = this._accesBD.AjouterLocation(voitureModeleDto.ToMetier(), client.ToMetier(), location.DateRetour);

                if (prix != -1)
                {
                    //Supprimme entrée en table emplacement.
                    _accesBD.SupprimerEmplacement((int)voitureModeleDto.ToMetier().Id);

                    GetListVoituresModelesDto();

                    this.Clear_voiture();
                    this.Effectuer = "";
                    return $"La location a bien été ajoutée. Acquitez-vous de {prix} euro.";
                }
            }

            if (this.effectuer == "Restituer la voiture")
            {
                int voitureId = location.VoitureId == null ? -1 : (int)location.VoitureId;

                int clientId = IsRent(voitureId);
                if (clientId == -1)
                    throw new Exception($"La voiture que vous voulez restituer n'est pas en cours de location. Restitution impossible !");
                
                if (clientId != this.client.Id)
                    throw new Exception("Vous avez selectionné le mauvais client pour la restitution de la voiture.");

                int resultat = this._accesBD.UpdateLocation((int)location.VoitureId);

                if (resultat != 0)
                {
                    //Ajout dans la table EMPLACEMENT et lui attribue une place automatiquement.
                    string emplacement = FindEmplacementAndAddVoiture();

                    GetListVoituresModelesDto();

                    this.Clear_voiture();
                    this.Effectuer = "";
                    return $"La restitution s'est bien déroulée. Voiture parquée à l'emplacement: {emplacement}";
                }
                
            }
            return "Action inconnue ";
        }

        private int IsRent(int voitureId)
        {
            //VoitureDto voitureFromDb = this._accesBD.GetVoitureLouee(voitureId).ToDto();
            int resulat = this._accesBD.GetVoitureLouee(voitureId);
            //if (voitureFromDb.Id != null)
            //    return true;
            return resulat;
        }

        private string FindEmplacementAndAddVoiture()
        {
            List<string> listEmplacementsLibres = ListEmplacementsLibresLoad();

            VoitureDto voiture = _accesBD.GetVoiture(this.voitureModeleDto.VoitureImmatriculation).ToDto();
            AjouterVoitureDansEmplacement(voiture.Id, listEmplacementsLibres[0]);
            return listEmplacementsLibres[0];
        }

        private void AjouterVoitureDansEmplacement(int? voitureId, string intituleEmplacement)
        {
            Emplacement emplacement = new Emplacement(intituleEmplacement, Convert.ToInt32(voitureId));
            int resultat = this._accesBD.AjouterEmplacement(emplacement);
            if (resultat != 1)
                throw new Exception("L'ajout de l'emplacement s'est mal produit.");
        }
        public void Restituer(int indexSelection)
        {
            this.location.VoitureId = collectionVoituresModelesLouees.ElementAt(indexSelection).VoitureId;
            this.voitureModeleDto.VoitureId = collectionVoituresModelesLouees.ElementAt(indexSelection).VoitureId;
            this.voitureModeleDto.VoitureImmatriculation = collectionVoituresModelesLouees.ElementAt(indexSelection).VoitureImmatriculation;

            this.Effectuer = "Restituer la voiture";
        }


        public void Louer(int indexSelection)
        {
            this.location.VoitureId = collectionVoituresModelesDisponibles.ElementAt(indexSelection).VoitureId;

            this.VoitureId = collectionVoituresModelesDisponibles.ElementAt(indexSelection).VoitureId;
            this.VoitureModeleId = collectionVoituresModelesDisponibles.ElementAt(indexSelection).VoitureModeleId;
            this.VoitureImmatriculation = collectionVoituresModelesDisponibles.ElementAt(indexSelection).VoitureImmatriculation;
            this.VoitureCouleur = collectionVoituresModelesDisponibles.ElementAt(indexSelection).VoitureCouleur;

            this.Effectuer = "Louer";
        }
    }
}
