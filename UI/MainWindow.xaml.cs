using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using Fynbus_Flexbus;
using Microsoft.Win32;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Lager lager = Lager.HentUdgave();
        Logik logik;
        string filPlaceringForTilbud = "";
        string filPlaceringForByder = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void VælgStamolysninger_Click(object sender, RoutedEventArgs e)
        {
            StamolysningerText.Text = "";
            OpenFileDialog fil = new OpenFileDialog();
            fil.Title = "Find & Vælg Stamoplysninger";
            fil.Filter = "Excel Files (*.csv)|*.csv";
            fil.ShowDialog();

            filPlaceringForByder = fil.FileName;
            StamolysningerText.Text = filPlaceringForByder;

        }

        private void VælgTilbudsblanket_Click(object sender, RoutedEventArgs e)
        {
            TilbudsblanketText.Text = "";
            OpenFileDialog fil = new OpenFileDialog();
            fil.Title = "Find & Vælg Tilbudsblanket";
            fil.Filter = "Excel Files (*.csv)|*.csv";
            fil.ShowDialog();

            filPlaceringForTilbud = fil.FileName;
            TilbudsblanketText.Text = filPlaceringForTilbud;
        }

        private void ImportOgFindVindere_Click(object sender, RoutedEventArgs e)
        {
            ImportOgFindVinderText.Text = "";
            if (filPlaceringForTilbud != "" && filPlaceringForByder != "")
            {
                logik = new Logik(filPlaceringForTilbud, filPlaceringForByder);
                ImportOgFindVinderText.Text = "Import og Udveldelse Fuldført, Se neden under";
            }
            else
            {
                ImportOgFindVinderText.Text = "Venligst vælg begge filer inden import";
            }
            


        }
        private void GemTilbudMedVindere_Click(object sender, RoutedEventArgs e)
        {
            GemTilbudMedVindereText.Text = "";
            SaveFileDialog gem = new SaveFileDialog();
            gem.Title = "Vælg Placering til Resultater";
            gem.ShowDialog();

            GemTilbudMedVindereText.Text = "Fil Skabt med ovenstående informationer";
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) // skal være her for der ikke er en fejl, ved ikke hvorfor
        {

        }
        
    }
}
