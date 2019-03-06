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
        
        public vyber()
        {
            InitializeComponent();
            
        }

        public vyber(Frame vyvolavac,int x,int y,string what) : this()
        {
            VyvolavaciOkno = vyvolavac;

            VyvolavaciOkno.Width = 400;
            VyvolavaciOkno.Height = 400;
            
            X = x;
            Y = y;
            if (what == "Budova")
            {
                povoleny_budovy();
                Vyber_jmeno.Text = "Vyber Budovu";
            }
            else
            {
                povoleny_postavy();
                Vyber_jmeno.Text = "Vyber Postavu";
            }
        }

        public void povoleny_budovy()
        {
            Button Domov = new Button();
            Domov.Name = "Domov";
            Domov.Height = 50;
            Domov.BorderThickness = new Thickness(5, 5, 5, 5);
            Domov.Margin = new Thickness(5,5,5,5);
            Domov.Click += new RoutedEventHandler(Vyber_Click);

            StackPanel vnitrek = new StackPanel();
            vnitrek.Orientation = Orientation.Horizontal; 

            Image new_image = new Image();
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("img/domek.png", UriKind.Relative);
            b.EndInit();

            new_image.Source = b;
            new_image.Height = 40;
            new_image.Width = 40;

            vnitrek.Children.Add(new_image);

            TextBlock new_txt = new TextBlock();
            new_txt.FontSize = 24;
            new_txt.Text = "Domov";
            
            vnitrek.Children.Add(new_txt);
            
            Domov.Content = vnitrek;

            Seznam.Children.Add(Domov);
        }

        public void povoleny_postavy()
        {
            foreach(Postava postava in Vesnice.Obyvatele)
            { 
                Button Postava = new Button();
                Postava.Name = "ID" + postava.ID;
                Postava.Height = 50;
                Postava.BorderThickness = new Thickness(5, 5, 5, 5);
                Postava.Margin = new Thickness(5, 5, 5, 5);
                Postava.Click += new RoutedEventHandler(Vyber_Click);

                StackPanel vnitrek = new StackPanel();
                vnitrek.Orientation = Orientation.Horizontal;

                Image new_image = new Image();
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.UriSource = new Uri("img/domek.png", UriKind.Relative);
                b.EndInit();

                new_image.Source = b;
                new_image.Height = 40;
                new_image.Width = 40;

                vnitrek.Children.Add(new_image);

                TextBlock new_txt = new TextBlock();
                new_txt.FontSize = 24;
                new_txt.Text = "Domov";

                vnitrek.Children.Add(new_txt);

                Domov.Content = vnitrek;

                Seznam.Children.Add(Domov);
            }
        }

        private void Vyber_Click(object sender, RoutedEventArgs e)
        {
            Budova novy;
            Button butt = sender as Button;
            if (butt.Name == "Domov")
            {
                novy = new Domov() {X_radek = X , Y_sloupec = Y };
                Vesnice.postaveni_do_vesnice(novy);
                Vesnice.Ukonci_podokno();
            }
            if
        }
    }
}
