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
    public partial class PageCategorie : Page
    {
        public PageCategorie()
        {
            InitializeComponent();
            this.DataContext = new GestionCategorieVueModele();
        }

        private void Ajout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GestionCategorieVueModele vm = (GestionCategorieVueModele)this.DataContext;
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
                GestionCategorieVueModele vm = (GestionCategorieVueModele)this.DataContext;
                string resultat = vm.EffectuerAction(GrilleCategories.SelectedIndex);
                this.DataContext = new GestionCategorieVueModele();
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

        //private void Modifier_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        GestionClientVueModele vm = (GestionClientVueModele)this.DataContext;
        //        int index = GrilleCategories.SelectedIndex;
        //        if (index < 0)
        //        {
        //            MessageBox.Show("Sélectionner une ligne à modifier.");
        //            return;
        //        }
        //        vm.Modifier(index);
        //    }

        //    catch (ExceptionAccesBD err)
        //    {
        //        MessageBox.Show(err.details);
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message);
        //    }
        //}
    }
}
