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


namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool DebugMode = false;
        string filPlaceringForTilbud = "";
        string filPlaceringForByder = "";
        public MainWindow()
        {
            InitializeComponent();

            if (DebugMode == true)
            {
                Debug.Visibility = Visibility.Visible;
            }
            else
            {
                Debug.Visibility = Visibility.Hidden;
            }
        }

        private void VælgStamolysninger_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VælgTilbudsblanket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImportOgFindVindere_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EksporterTilbudMedVindere_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VælgPlaceringTilEksport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Debug_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
