using LocationVoitureWPF.coucheAccesBD;
using LocationVoitureWPF.coucheModeleVue;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LocationVoitureWPF.coucheVue
{
    /// <summary>
    /// Logique d'interaction pour PagePersonne.xaml
    /// </summary>
    public partial class PageVoiture : Page
    {
        public PageVoiture()
        {
            InitializeComponent();
            this.DataContext = new GestionVoitureVueModele();
            LoadListModeles();
            LoadListEmplacements();
        }
        
        private void Ajout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionVoitureVueModele vm = (GestionVoitureVueModele)this.DataContext;
                vm.Ajouter();
            }
            catch (ExceptionAccesBD err)
            {
                MessageBox.Show(err.details);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        private void Suppression_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionVoitureVueModele vm = (GestionVoitureVueModele)this.DataContext;
                int index = GrilleVoituresModeles.SelectedIndex;
                if (index < 0)
                {
                    MessageBox.Show("Sélectionner une ligne à modifier.");
                    return;
                }
                vm.Supprimer(index);
            }

            catch (ExceptionAccesBD err)
            {
                MessageBox.Show(err.details);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void Effectuer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionVoitureVueModele vm = (GestionVoitureVueModele)this.DataContext;
                string resultat = vm.EffectuerAction(GrilleVoituresModeles.SelectedIndex);
                this.DataContext = new GestionVoitureVueModele();
                MessageBox.Show(resultat);
            }

            catch (ExceptionAccesBD err)
            {
                MessageBox.Show(err.details);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void LoadListModeles()
        {
            this.cmbListModeles.DisplayMemberPath = "Nom";
            this.cmbListModeles.SelectedValuePath = "Id";
            this.cmbListModeles.ItemsSource = GestionVoitureVueModele.ListModelesLoad();
        }

        private void LoadListEmplacements()
        {
            this.cmbListEmplacements.ItemsSource = GestionVoitureVueModele.ListEmplacementsLibresLoad();
        }
    }
}
