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
        public List<List<Budova>> Budovy = new List<List<Budova>>();

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

            nastaveni_img.Source = b;
            Nastav_Mista_Pro_Budovy();

            b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("img/hammer.png", UriKind.Relative);
            b.EndInit();

            staveni_img.Source = b;
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

        private void Nastav_Mista_Pro_Budovy()
        {

            for (int a = 1; a < 9; a++)
            {
                List<Budova> novy_sloupec = new List<Budova>();
                if (a == 3 || a == 6)
                {
                    a++;
                }
                for (int b = 1; b < 15; b++)
                {
                    if (b == 3 || b == 6 || b == 9 || b == 12)
                    {
                        b++;
                    }
                    Budova novy = new Budova();
                    novy.X_radek = b;
                    novy.Y_sloupec = a;

                    Button buttova = new Button();
                    buttova.Height = 50;
                    buttova.Width = 50;
                    buttova.BorderBrush = Brushes.Green;
                    buttova.BorderThickness = new Thickness(3, 3, 3, 3);
                    buttova.HorizontalAlignment = HorizontalAlignment.Right;
                    buttova.VerticalAlignment = VerticalAlignment.Bottom;
                    buttova.Name = "Budova" + a + b;
                    buttova.Click += new RoutedEventHandler(Pridej_baracek);
                    Grid.SetColumn(buttova, b);
                    Grid.SetRow(buttova, a);

                    BitmapImage bit = new BitmapImage();
                    bit.BeginInit();
                    bit.UriSource = new Uri(@"img/" + novy.obr_odkaz, UriKind.Relative);
                    bit.EndInit();

                    Image obrazecek = new Image();
                    obrazecek.Source = bit;

                    buttova.Content = obrazecek;
                    Vesnicoid.Children.Add(buttova);

                    novy_sloupec.Add(novy);
                }
                Budovy.Add(novy_sloupec);
            }
        }
        private void Pridej_baracek(object sender, RoutedEventArgs e)
        {
            Button butt = sender as Button;
            string ae = butt.Name.Substring(6, 1);
            string be;
            if (butt.Name.Length == 8)
            {
                 be = butt.Name.Substring(7, 1);
            }
            else
            {
                 be = butt.Name.Substring(7, 2);
            }
            int a = int.Parse(ae);
            int b = int.Parse(be);
            int ad = 0;
            int bd = 0;
            int aa = 0;
            int bb = 0;
            Image novy_img = new Image();
            foreach (List<Budova> budovas in Budovy)
            {
                bd = 0;
                /*try
                */
                    foreach (Budova budovan in budovas)
                    {
                    
                        if(budovan.X_radek == b && budovan.Y_sloupec == a)
                        {
                        aa = ad;
                        bb = bd;
                        }
                        bd++;
                   
                    }
                /*}
                catch
                {

                }*/
                ad++;
            }
            Budovy[aa][bb] = new Domov() {X_radek = b , Y_sloupec = a };
            BitmapImage bim = new BitmapImage();
            bim.BeginInit();
            bim.UriSource = new Uri("img/" + Budovy[aa][bb].obr_odkaz, UriKind.Relative);
            bim.EndInit();
            novy_img.Source = bim;

            butt.Content = novy_img;
        }
        private void Staveni_Click(object sender, RoutedEventArgs e)
        {
            /*Podokno.Navigate(new NastaveniMenu(Podokno));
            podokno = Podokno;*/
        }
    }
}
