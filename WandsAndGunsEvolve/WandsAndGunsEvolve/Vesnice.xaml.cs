using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WandsAndGunsEvolve
{
    /// <summary>
    /// Interakční logika pro Vesnice.xaml
    /// </summary>
    public partial class Vesnice : Page
    {
        private Frame PredchoziOkno;
        public static Frame podokno;

        public Vesnice()
        {
            podokno = Podokno;
            InitializeComponent();
        }

        public Vesnice(Frame Window) : this()
        {
            Application.Current.MainWindow.Height = 489;
            Application.Current.MainWindow.Width = 820;
            PredchoziOkno = Window;

            // menu nasataveni
            //nastaveni
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("img/nastaveni.png", UriKind.Relative);
            b.EndInit();
            
            nastaveni.Source = b;
        }
        static public void Ukonci_podokno()
        {
            podokno.Navigate(null);
        }

        private void Nastaveni_Click(object sender, RoutedEventArgs e)
        {
            Podokno.Navigate(new NastaveniMenu(Podokno));
            podokno = Podokno;
        }
    }
}
