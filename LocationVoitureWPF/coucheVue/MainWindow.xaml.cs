using System;
using System.Windows;

namespace LocationVoitureWPF.coucheVue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GestionClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.Content = new PageClient();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue pendant l'ouverture de la fenêtre :\n" + ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GestionCategorie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.Content = new PageCategorie();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue pendant l'ouverture de la fenêtre :\n" + ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GestionModele_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.Content = new PageModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue pendant l'ouverture de la fenêtre :\n" + ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GestionVoiture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.Content = new PageVoiture();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue pendant l'ouverture de la fenêtre :\n" + ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GestionLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.Content = new PageLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue pendant l'ouverture de la fenêtre :\n" + ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
