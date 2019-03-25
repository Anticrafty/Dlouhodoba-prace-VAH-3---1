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
        static public Grid vesnicoid;
        static public TextBlock Obyvatel_count;
        static public TextBlock Mornin_wood;
        static public TextBlock Stone_Hard;
        static public TextBlock Juicy;

        static public List<Postava> Drevorub = new List<Postava>();
        static public List<Postava> Kamenolomec = new List<Postava>();
        static public List<Postava> Branana= new List<Postava>();

        public static List<List<Budova>> Budovy = new List<List<Budova>>();
        public static List<Postava> Obyvatele = new List<Postava>();
        static public int drevo = 50;
        static public int kamen = 20;
        static public int jidlo = 14;
        static public List<string> items = new List<string>();
        static public List<string> vynalezy = new List<string>();

        public bool staveni_bool = false;
        static public Random rnd_s = new Random();

        public Vesnice()
        {

            InitializeComponent();
            podokno = Podokno;
            vesnicoid = Vesnicoid;
            Obyvatel_count = postavy_count;
            Mornin_wood = Drevo_count;
            Stone_Hard = Kamen_count;
            Juicy = Jidlo_count;
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
            b.UriSource = new Uri("../img/nastaveni.png", UriKind.Relative);
            b.EndInit();

            nastaveni_img.Source = b;
            Nastav_Mista_Pro_Budovy();

            b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/hammer.png", UriKind.Relative);
            b.EndInit();

            staveni_img.Source = b;

            b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/Pokracovat.png", UriKind.Relative);
            b.EndInit();

            dalsi_okolo_img.Source = b;

            b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/panacek.png", UriKind.Relative);
            b.EndInit();

            postavy_img.Source = b;

            b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/wood.png", UriKind.Relative);
            b.EndInit();

            Drevo_img.Source = b;

            b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/sutr.png", UriKind.Relative);
            b.EndInit();

            Kamen_img.Source = b;

            b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/Bagl.png", UriKind.Relative);
            b.EndInit();

            invertar_img.Source = b;

            b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/food.png", UriKind.Relative);
            b.EndInit();

            Jidlo_img.Source = b;

            Vytvor_Lidi_Prvni();

            prepocitej_postavy();
            prepocitej_suroviny();
        }
        static public void Ukonci_podokno()
        {
            podokno.Navigate(null);
            prepocitej_postavy();
            prepocitej_suroviny();
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
                    bit.UriSource = new Uri(@"../img/" + novy.obr_odkaz, UriKind.Relative);
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
                if (Budovy[aa][bb] is Domov || Budovy[aa][bb] is Dilna)
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
                        drevo = drevo - nova_budova.potreba_dreva;
                        kamen = kamen - nova_budova.potreba_kamene;
                        BitmapImage bim = new BitmapImage();
                        bim.BeginInit();
                        bim.UriSource = new Uri("../img/" + Budovy[aa][bb].obr_odkaz, UriKind.Relative);
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
            else if(ceho == "konec")
            {
                System.Windows.Application.Current.Shutdown();
            }
            else if(ceho == "Speedy")
            {
                podokno.Navigate(new Lovecky_okno(podokno, true));
            }
            else if (ceho == "Normal Speed")
            {
                podokno.Navigate(new Lovecky_okno(podokno, false));
            }
        }

        public void Vytvor_Lidi_Prvni()
        {
            Postava Eidam = new Bezny_obyvatel() { muzstvi = true, ID = 0, vek = 1 };
            Postava Mozzarella = new Bezny_obyvatel() { muzstvi = false, ID = 1 , vek = 1};

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
                    if (budova.akce_budovy == "Stavba")
                    {
                        foreach (Postava pracovnik in budova.pracovnici)
                        {
                            budova.Splneno_Na_Postaveni = budova.Splneno_Na_Postaveni + pracovnik.Postava_za_Den;
                        }
                    }
                    else if (budova.akce_budovy != null)
                    {
                        budova.Do();
                    }

                    if (budova.Splneno_Na_Postaveni >= budova.Potreba_Na_Postaveni)
                    {
                        foreach (Object obj in vesnicoid.Children)
                        {
                            if (obj is Button)
                            {
                                Button butt = obj as Button;
                                if (butt.Name == "Budova" + budova.Y_sloupec + budova.X_radek)
                                {
                                    butt.Background = Brushes.White;
                                }
                            }
                        }
                    }
                    budova.pracovnici = new List<Postava>();

                }

            }
            foreach (Postava delnik in Drevorub)
            {
                drevo = drevo + rnd_s.Next(5, 20 * delnik.Postava_za_Den);
            }
            Drevorub = new List<Postava>();
            foreach (Postava delnik in Kamenolomec)
            {
                kamen = kamen + rnd_s.Next(5, 20 * delnik.Postava_za_Den);
            }
            Kamenolomec = new List<Postava>();

            // Loveni - Branana
            //podokno.Navigate(new potvrzeni(podokno, 0, 0, "Speedy"));
            if (Branana.Count() != 0)
            {
                podokno.Navigate(new Lovecky_okno(podokno, false));
            }
            
            foreach (Postava obyvatel in Obyvatele.AsEnumerable().Reverse())
            {
                obyvatel.vek++;
                if(jidlo != 0)
                {
                    if (obyvatel.zivy)
                    {
                        jidlo--;
                        if (obyvatel.actual_zivotu < obyvatel.Max_Zivotu)
                        {
                            obyvatel.actual_zivotu++;
                        }
                    }
                }
                else
                {
                   
                    if(obyvatel.zivy)
                    {
                        obyvatel.actual_zivotu--;
                        if (obyvatel.actual_zivotu == 0)
                        {
                            obyvatel.zivy = false;
                        }
                    }
                }
                if( obyvatel.vek > 50)
                {
                    obyvatel.zivy = false;
                }
            }
            prepocitej_postavy();
            prepocitej_suroviny();
        }

        static public void prepocitej_postavy()
        {
            int count_postav = 0;
            int count_mrtvich = 0;
            foreach (Postava obyvatel in Obyvatele)
            {
                if (obyvatel.zivy)
                {
                    bool nepracuje = true;
                    foreach (List<Budova> radek in Budovy)
                    {
                        foreach (Budova budova in radek)
                        {
                            foreach (Postava pracovnik in budova.pracovnici)
                            {
                                if (obyvatel == pracovnik)
                                {
                                    nepracuje = false;
                                }
                            }
                        }
                    }
                    foreach (Postava delnik in Drevorub)
                    {
                        if (obyvatel == delnik)
                        {
                            nepracuje = false;
                        }
                    }
                    foreach (Postava delnik in Kamenolomec)
                    {
                        if (obyvatel == delnik)
                        {
                            nepracuje = false;
                        }
                    }
                    foreach (Postava delnik in Branana)
                    {
                        if (obyvatel == delnik)
                        {
                            nepracuje = false;
                        }
                    }
                    if (nepracuje)
                    {
                        count_postav++;
                    }
                }
                else
                {
                    count_mrtvich++;
                }
            }
            Obyvatel_count.Text = count_postav + " / " + (Obyvatele.Count() - count_mrtvich);
            if (Obyvatele.Count() == count_mrtvich)
            {
                podokno.Navigate(new potvrzeni(podokno, 0, 0, "konec"));
            }
        }

        static public void prepocitej_suroviny()
        {
            Mornin_wood.Text = drevo.ToString();
            Stone_Hard.Text = kamen.ToString();
            Juicy.Text = jidlo.ToString();
        }
        private void Postava_Click(object sender, RoutedEventArgs e)
        {
            podokno.Navigate(new vyber(podokno, 0, 0, "Obyvatel"));
        }

        private void Tezeni_Click(object sender, RoutedEventArgs e)
        {
            Button butt = sender as Button;
            if(butt.Name == "drevorubec")
            {
                Podokno.Navigate(new Menu_budovy(Podokno, -1, -1));
            }
            else if (butt.Name == "kamenolom")
            {
                Podokno.Navigate(new Menu_budovy(Podokno, -2, -2));
            }
            else
            {
                Podokno.Navigate(new Menu_budovy(Podokno, -3, -3));
            }
        }
        private void Inventar_click(object sender, RoutedEventArgs e)
        {
            Podokno.Navigate(new Inventory(Podokno));
        }
    }    
}
