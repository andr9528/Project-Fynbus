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
using System.Data;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Lager lager = Lager.HentUdgave();
        Logik logik;
        List<Tilbud> tilbudene = new List<Tilbud>();

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
                UdtrækTilbud();
                IndsætIndhold();
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
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {

        }
        private void UdtrækTilbud()
        {
            foreach (var rute in lager.Ruter)
            {
                foreach (var tilbud in rute.Tilbud)
                {
                    if (tilbud.VinderStatus == true)
                    {
                        tilbudene.Add(tilbud);
                    }
                }
            }
            foreach (var rute in lager.Ruter)
            {
                foreach (var tilbud in rute.Tilbud)
                {
                    if (tilbud.VinderStatus == false)
                    {
                        tilbudene.Add(tilbud);
                    }
                }
            }
        }
        private void IndsætIndhold()
        {
            DataTable view = new DataTable();
            view.Columns.Add("TilbudID");
            view.Columns.Add("RuteNr");
            view.Columns.Add("TimePris");
            view.Columns.Add("ByderNavn");
            view.Columns.Add("ByderFirma");
            view.Columns.Add("ByderMail");
            view.Columns.Add("RutePrioritet");
            view.Columns.Add("ByderPrioritet");
            view.Columns.Add("VinderStatus");

            foreach (var tilbud in tilbudene)
            {
                string vinderStatus = "";
                if (tilbud.VinderStatus == true)
                {
                    vinderStatus = "Vandt";
                }
                else
                {
                    vinderStatus = "Tabte";
                }
                //string[] række = 
                //    { tilbud.TilbudID, tilbud.RuteNummer.ToString(),
                //    tilbud.TimePris.ToString(), tilbud.Byder.Navn,
                //    tilbud.Byder.Firma, tilbud.Byder.Mail,
                //    tilbud.RutePrioritet.ToString(), tilbud.ByderPrioritet.ToString(),
                //    vinderStatus};

                view.Rows.Add(tilbud.TilbudID, tilbud.RuteNummer.ToString(),
                    tilbud.TimePris.ToString(), tilbud.Byder.Navn,
                    tilbud.Byder.Firma, tilbud.Byder.Mail,
                    tilbud.RutePrioritet.ToString(), tilbud.ByderPrioritet.ToString(),
                    vinderStatus);
                
            }
            View.ItemsSource = view.AsDataView();
        }

        private void ÅbenManual_Click(object sender, RoutedEventArgs e)
        {
            string manual = "Manual.pdf";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, manual);
            Process.Start(path);
        }
    }
}
