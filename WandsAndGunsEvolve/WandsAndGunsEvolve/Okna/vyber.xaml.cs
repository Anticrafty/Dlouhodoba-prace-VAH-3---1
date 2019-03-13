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
    /// Interakční logika pro vyber.xaml
    /// </summary>
    public partial class vyber : Page
    {
        private Frame VyvolavaciOkno;
        int X;
        int Y;
        string What;

        public vyber()
        {
            InitializeComponent();

        }

        public vyber(Frame vyvolavac, int x, int y, string what) : this()
        {
            VyvolavaciOkno = vyvolavac;

            VyvolavaciOkno.Width = 400;
            VyvolavaciOkno.Height = 400;

            X = x;
            Y = y;
            What = what;

            
            if (what == "Budova")
            {
                povoleny_budovy();
                Vyber_jmeno.Text = "Vyber Budovu";
            }
            else
            {
                povoleny_postavy();
                if (What == "Obyvatel")
                {
                    Vyber_jmeno.Text = "Obyvatelé";
                }
                else
                {
                    Vyber_jmeno.Text = "Vyber Postavu";
                }
            }
        }

        public void povoleny_budovy()
        {
            bool je_dum = false;
            foreach (List<Budova> radek in Vesnice.Budovy)
            {
                foreach(Budova budova in radek)
                {
                    if( budova is Domov && budova.Potreba_Na_Postaveni == budova.Splneno_Na_Postaveni)
                    {
                        je_dum = true;
                    }
                }
            }
            bool dostatek_dreva = false;
            if (Vesnice.drevo > 49)
            {
                dostatek_dreva = true;
            }
            bool dostatek_kamene = false;
            if(Vesnice.kamen > 19 )
            {
                dostatek_kamene = true;
            }
            vytvor_butt("Domov", "domek.png", 50 , 20 , dostatek_dreva, dostatek_kamene);

            //
            if (je_dum)
            {
                dostatek_dreva = false;
                if (Vesnice.drevo > 49)
                {
                    dostatek_dreva = true;
                }
                dostatek_kamene = false;
                if (Vesnice.kamen > 99)
                {
                    dostatek_kamene = true;
                }
                vytvor_butt("Dilna","Dilna.png", 50 , 100, dostatek_dreva, dostatek_kamene);
               
            }
            
        }

        public void povoleny_postavy()
        {
            foreach (Postava postava in Vesnice.Obyvatele)
            {
                bool alredy_in_somewhere = false;
                foreach (List<Budova> radek in Vesnice.Budovy)
                {
                    foreach (Budova budova in radek)
                    {
                        foreach (Postava pracovnik in budova.pracovnici)
                        {
                            if (postava == pracovnik)
                            {
                                alredy_in_somewhere = true;
                            }
                        }
                    }
                }
                foreach (Postava delnik in Vesnice.Drevorub)
                {
                    if (postava == delnik)
                    {
                        alredy_in_somewhere = true;
                    }
                }
                foreach (Postava delnik in Vesnice.Kamenolomec)
                {
                    if (postava == delnik)
                    {
                        alredy_in_somewhere = true;
                    }
                }
                foreach (Postava delnik in Vesnice.Branana)
                {
                    if (postava == delnik)
                    {
                        alredy_in_somewhere = true;
                    }
                }

                Button Postava = new Button();
                Postava.Name = "ID" + postava.ID;
                Postava.Height = 50;
                Postava.BorderThickness = new Thickness(5, 5, 5, 5);
                Postava.Margin = new Thickness(5, 5, 5, 5);
                if (What == "Obyvatel")
                {
                    Postava.Click += new RoutedEventHandler(Rodiny_strom_Click);
                }
                else
                {
                    Postava.Click += new RoutedEventHandler(Vyber_Click);
                }
                if (What == "Obyvatel" && alredy_in_somewhere)
                {
                    Postava.BorderBrush = Brushes.DarkRed;
                }

                StackPanel vnitrek = new StackPanel();
                vnitrek.Orientation = Orientation.Horizontal;

                Image new_image = new Image();
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.UriSource = new Uri("../img/" + postava.obr_odkaz, UriKind.Relative);
                b.EndInit();

                new_image.Source = b;
                new_image.Height = 40;
                new_image.Width = 40;

                vnitrek.Children.Add(new_image);

                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Věk: " + postava.vek };
                vnitrek.Children.Add(Vek);
                string pohlav = "pohlaví";
                if (postava.muzstvi)
                {
                    pohlav = "muž";
                }
                else
                {
                    pohlav = "žena";
                }
                TextBlock Pohlavi = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " Pohlaví: " + pohlav };
                vnitrek.Children.Add(Pohlavi);

                TextBlock zivot = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " životy: " + postava.actual_zivotu + " / " + postava.Max_Zivotu };
                vnitrek.Children.Add(zivot);

                Postava.Content = vnitrek;
                if ((alredy_in_somewhere && (What == "Postava" || What == "Mnozeni")) || !postava.zivy)
                {
                    
                }
                else
                {
                    Seznam.Children.Add(Postava);
                }
            }
        }

        private void Vyber_Click(object sender, RoutedEventArgs e)
        {
            Budova novy;
            Button butt = sender as Button;
            if (butt.Name == "Domov")
            {
                novy = new Domov() { X_radek = X, Y_sloupec = Y };
                Vesnice.postaveni_do_vesnice(novy);
                Vesnice.Ukonci_podokno();
            }
            else if (butt.Name == "Dilna")
            {
                novy = new Dilna() { X_radek = X, Y_sloupec = Y };
                Vesnice.postaveni_do_vesnice(novy);
                Vesnice.Ukonci_podokno();

            }
            else if (butt.Name.Substring(0, 2) == "ID")
            {
                if (X == -1 || Y == -1)
                {
                    Vesnice.Drevorub.Add(Vesnice.Obyvatele[int.Parse(butt.Name.Substring(2))]);
                }
                else if(X == -2 || Y == -2)
                {
                    Vesnice.Kamenolomec.Add(Vesnice.Obyvatele[int.Parse(butt.Name.Substring(2))]);
                }
                else if (X == -3 || Y == -3)
                {
                    Vesnice.Branana.Add(Vesnice.Obyvatele[int.Parse(butt.Name.Substring(2))]);
                }
                else
                {
                    Vesnice.Budovy[X][Y].pracovnici.Add(Vesnice.Obyvatele[int.Parse(butt.Name.Substring(2))]);
                }
                int ID_obj = 0;
                foreach(Object obj in Seznam.Children)
                {
                    if (obj is Button)
                    {
                        butt = obj as Button;
                        if (butt.Content.ToString() != "Zpět")
                        {
                            Seznam.Children[ID_obj].Visibility = Visibility.Collapsed;
                        }
                                
                    }
                    ID_obj++;
                }
                    povoleny_postavy();
            }
        }

        private void Rodiny_strom_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Ukonci_podokno();
        }

        private void Zpet_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Ukonci_podokno();
        }

        private void vytvor_butt(string name, string odkaz_img, int drevo, int kamen, bool dostatek_dreva, bool dostatek_kamene)
        {
            Button Domov = new Button();
            Domov.Name = name;
            Domov.Height = 50;
            Domov.BorderThickness = new Thickness(5, 5, 5, 5);
            Domov.Margin = new Thickness(5, 5, 5, 5);
            Domov.Click += new RoutedEventHandler(Vyber_Click);
            if (!dostatek_kamene || !dostatek_dreva )
            {
                Domov.IsEnabled = false;
            }

            StackPanel vnitrek = new StackPanel();
            vnitrek.Orientation = Orientation.Horizontal;

            Image new_image = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/" + odkaz_img, UriKind.Relative);
            b.EndInit();

            new_image.Source = b;
            new_image.Height = 40;
            new_image.Width = 40;

            vnitrek.Children.Add(new_image);

            TextBlock new_txt = new TextBlock();
            new_txt.FontSize = 24;
            new_txt.Text = name;

            vnitrek.Children.Add(new_txt);

            new_txt = new TextBlock();
            new_txt.FontSize = 12;
            new_txt.Text = "drevo: " + drevo;
            new_txt.Height = 20;
            new_txt.Margin = new Thickness(10, 0, 0, 0);
            new_txt.VerticalAlignment = VerticalAlignment.Bottom;
            if (!dostatek_dreva)
            {
                new_txt.Foreground = Brushes.Red;
            }

            vnitrek.Children.Add(new_txt);

            new_txt = new TextBlock();
            new_txt.FontSize = 12;
            new_txt.Text = "kamen: " + kamen;
            new_txt.Height = 20;
            new_txt.Margin = new Thickness(10, 0, 0, 0);
            new_txt.VerticalAlignment = VerticalAlignment.Bottom;
            if (!dostatek_kamene)
            {
                new_txt.Foreground = Brushes.Red;
            }

            vnitrek.Children.Add(new_txt);

            Domov.Content = vnitrek;

            Seznam.Children.Add(Domov);
        }
    }
}
