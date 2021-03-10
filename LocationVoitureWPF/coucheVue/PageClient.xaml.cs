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
    public partial class PageClient : Page
    {
        public PageClient()
        {
            InitializeComponent();
            this.DataContext = new GestionClientVueModele();
        }

        private void Ajout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionClientVueModele vm = (GestionClientVueModele)this.DataContext;
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
                GestionClientVueModele vm = (GestionClientVueModele)this.DataContext;
                string resultat = vm.EffectuerAction(GrilleClients.SelectedIndex);
                this.DataContext = new GestionClientVueModele();
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

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionClientVueModele vm = (GestionClientVueModele)this.DataContext;
                int index = GrilleClients.SelectedIndex;
                if (index < 0)
                {
                    MessageBox.Show("Sélectionner une ligne à modifier.");
                    return;
                }
                vm.Modifier(index);
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
    }
}
