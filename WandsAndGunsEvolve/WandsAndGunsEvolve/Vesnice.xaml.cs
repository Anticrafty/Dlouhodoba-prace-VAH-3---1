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
        public static List<List<Budova>> Budovy = new List<List<Budova>>();
        public static List<Postava> Obyvatele = new List<Postava>();
        public bool staveni_bool = false;
        static public Grid vesnicoid;


        public Vesnice()
        {

            InitializeComponent();
            podokno = Podokno;
            vesnicoid = Vesnicoid;
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

            Vytvor_Lidi_Prvni();
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
                    Budova novy = new Prazdny_pozemek();
                    novy.X_radek = b;
                    novy.Y_sloupec = a;

                    Button buttova = new Button();
                    buttova.Height = 47;
                    buttova.Width = 47;
                    buttova.BorderBrush = Brushes.LightGreen;
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
            if (staveni_bool)
            {


                if (butt.BorderBrush == Brushes.Green)
                {

                    Podokno.Navigate(new vyber(Podokno, b, a, "Budova"));
                }
                else
                {
                    Podokno.Navigate(new potvrzeni(Podokno, a, b, "Stavba"));
                }
                OdStaveni();
                podokno = Podokno;
            }
            else
            {
                int ad = 0;
                int bd = 0;
                int aa = 0;
                int bb = 0;
                foreach (List<Budova> budovas in Budovy)
                {
                    bd = 0;
                    foreach (Budova budovan in budovas)
                    {

                        if (budovan.X_radek == b && budovan.Y_sloupec == a)
                        {
                            aa = ad;
                            bb = bd;
                        }
                        bd++;

                    }
                    ad++;
                }
                if (Budovy[aa][bb] is Domov)
                {
                    Podokno.Navigate(new Menu_budovy(Podokno, aa, bb));
                }

                //podokno = Podokno;
            }

        }
        private void Staveni_Click(object sender, RoutedEventArgs e)
        {
            if (staveni_bool)
            {
                OdStaveni();
            }
            else
            {
                staveni_bool = true;
                staveni.Background = Brushes.LightGreen;
                foreach (Object obj in Vesnicoid.Children)
                {
                    if (obj is Button)
                    {
                        Button butt = obj as Button;
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

                        foreach (List<Budova> radek in Budovy)
                        {
                            foreach (Budova budova in radek)
                            {
                                if (budova.X_radek == b && budova.Y_sloupec == a)
                                {
                                    if (budova is Domov)
                                    {
                                        butt.BorderBrush = Brushes.Red;
                                    }
                                    else
                                    {
                                        butt.BorderBrush = Brushes.Green;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        private void OdStaveni()
        {
            staveni_bool = false;
            staveni.Background = Brushes.LightGray;
            foreach (Object obj in vesnicoid.Children)
            {
                if (obj is Button)
                {
                    Button butt = obj as Button;
                    butt.BorderBrush = Brushes.LightGreen;
                }
            }
        }
        static public void postaveni_do_vesnice(Budova nova_budova)
        {
            string name = "Budova" + nova_budova.Y_sloupec + nova_budova.X_radek; ;
            foreach (Object obj in vesnicoid.Children)
            {
                if (obj is Button)
                {
                    Button butt = obj as Button;
                    if (butt.Name == name)
                    {
                        int ad = 0;
                        int bd = 0;
                        int aa = 0;
                        int bb = 0;
                        Image novy_img = new Image();
                        foreach (List<Budova> budovas in Budovy)
                        {
                            bd = 0;
                            foreach (Budova budovan in budovas)
                            {

                                if (budovan.X_radek == nova_budova.X_radek && budovan.Y_sloupec == nova_budova.Y_sloupec)
                                {
                                    aa = ad;
                                    bb = bd;
                                }
                                bd++;

                            }
                            ad++;
                        }
                        Budovy[aa][bb] = nova_budova;
                        BitmapImage bim = new BitmapImage();
                        bim.BeginInit();
                        bim.UriSource = new Uri("img/" + Budovy[aa][bb].obr_odkaz, UriKind.Relative);
                        bim.EndInit();
                        novy_img.Source = bim;

                        butt.Content = novy_img;
                        butt.Background = Brushes.IndianRed;

                    }
                }
            }
        }
        public static void Potvrzeni(int a, int b, string ceho)
        {
            if (ceho == "Stavba")
            {
                podokno.Navigate(new vyber(podokno, b, a, "Budova"));
            }
            else if (ceho == "Kolo")
            {
                Dalsi_kolo();
            }
        }

        public void Vytvor_Lidi_Prvni()
        {
            Postava Eidam = new Bezny_obyvatel() { muzstvi = true, ID = 0 };
            Postava Mozzarella = new Bezny_obyvatel() { muzstvi = false, ID = 1 };

            Obyvatele.Add(Eidam);
            Obyvatele.Add(Mozzarella);
        }
        private void Dalsi_kolo_Click(object sender, RoutedEventArgs e)
        {
            Podokno.Navigate(new potvrzeni(podokno, 0, 0, "Kolo"));
        }
        static private void Dalsi_kolo()
        {
            foreach (List<Budova> radek in Budovy)
            {
                foreach (Budova budova in radek)
                {
                    if (budova.nastavena_akce == "Stavba")
                    {
                        foreach (Postava pracovnik in budova.pracovnici)
                        {
                            budova.Splneno_Na_Postaveni = budova.Splneno_Na_Postaveni + pracovnik.Postava_za_Den;
                        }
                    }
                    if (budova.Splneno_Na_Postaveni >= budova.Potreba_Na_Postaveni)
                    {
                        foreach (Object obj in vesnicoid.Children)
                        {
                            if (obj is Button)
                            {
                                Button butt = obj as Button;
                                if (butt.Name == "Budova" + budova.Y_sloupec + budova.X_radek )
                                {
                                    butt.Background = Brushes.White;
                                }
                            }
                        }
                    }
                    budova.pracovnici = new List<Postava>();
                }
            }
        }
    }    
}
