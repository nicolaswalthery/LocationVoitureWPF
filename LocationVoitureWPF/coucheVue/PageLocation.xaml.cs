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
    public partial class PageLocation : Page
    {
        public PageLocation()
        {
            InitializeComponent();
            this.DataContext = new GestionLocationVueModele();
            LoadListClients();
        }

        private void Louer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionLocationVueModele vm = (GestionLocationVueModele)this.DataContext;
                int index = GrilleVoituresModelesDisponibles.SelectedIndex;
                if (index < 0)
                {
                    MessageBox.Show("Sélectionner une ligne dans le tableau des voitures disponible.");
                    return;
                }
                vm.Louer(index);
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
        private void Restituer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionLocationVueModele vm = (GestionLocationVueModele)this.DataContext;
                int index = GrilleVoituresModelesLouees.SelectedIndex;
                if (index < 0)
                {
                    MessageBox.Show("Sélectionner une ligne à modifier.");
                    return;
                }
                vm.Restituer(index);
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
                GestionLocationVueModele vm = (GestionLocationVueModele)this.DataContext;
                string resultat = vm.EffectuerAction();
                this.DataContext = new GestionLocationVueModele();
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

        private void LoadListClients()
        {
            this.cmbListClients.DisplayMemberPath = "NumPermisConduire";
            this.cmbListClients.SelectedValuePath = "Id";
            this.cmbListClients.ItemsSource = GestionLocationVueModele.ListClientLoad();
        }
    }
}
