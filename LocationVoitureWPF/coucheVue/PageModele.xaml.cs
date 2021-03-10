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
    public partial class PageModele : Page
    {
        public PageModele()
        {
            InitializeComponent();
            this.DataContext = new GestionModeleVueModele();
            LoadListCategoriesNoms();
        }

        private void Ajout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionModeleVueModele vm = (GestionModeleVueModele)this.DataContext;
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

        private void Effectuer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionModeleVueModele vm = (GestionModeleVueModele)this.DataContext;
                string resultat = vm.EffectuerAction(GrilleModeles.SelectedIndex);
                this.DataContext = new GestionModeleVueModele();
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

        private void LoadListCategoriesNoms()
        {
            this.cmbListCategoriesNoms.ItemsSource = GestionModeleVueModele.ListCategoriesNomsLoad();
        }
    }
}
