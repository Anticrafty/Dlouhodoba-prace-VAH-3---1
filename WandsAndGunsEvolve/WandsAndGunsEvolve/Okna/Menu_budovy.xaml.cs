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
    /// Interakční logika pro Menu_budovy.xaml
    /// </summary>
    public partial class Menu_budovy : Page
    {
        int A;
        int B;
        Frame VyvolavaciOkno;
        public Menu_budovy()
        {
            InitializeComponent();
        }

        public Menu_budovy(Frame vyvolavac, int a, int b) : this()
        {
            VyvolavaciOkno = vyvolavac;

            A = a;
            B = b;

            VyvolavaciOkno.Width = 450;
            VyvolavaciOkno.Height = 450;
            if (a < 0 || b < 0)
            {
                delnici_zapis();
            }
            else
            {
                pracovnici_zapis();
            }
        }

        public void pracovnici_zapis()
        {
            int number_in_list = 0;
            foreach (Postava postava in Vesnice.Budovy[A][B].pracovnici)
            {
                vypis_postavu(postava, number_in_list);
                number_in_list++;
            }

            if (Vesnice.Budovy[A][B].Splneno_Na_Postaveni < Vesnice.Budovy[A][B].Potreba_Na_Postaveni)
            {
                Vyvoj.IsEnabled = false;
                Uceni.IsEnabled = false;
                Craft.IsEnabled = false;
            }
            else
            {
                Stavba.IsEnabled = false;
                if (!Vesnice.Budovy[A][B].Vyvoj_exist && (Vesnice.Budovy[A][B].akce_budovy != null || Vesnice.Budovy[A][B].akce_budovy != "Stavba"))
                {
                    Vyvoj.IsEnabled = false;
                }
                if (!Vesnice.Budovy[A][B].Uceni_exist && (Vesnice.Budovy[A][B].akce_budovy != null || Vesnice.Budovy[A][B].akce_budovy != "Uceni"))
                {
                    Uceni.IsEnabled = false;
                }
                if (!Vesnice.Budovy[A][B].Crafting_exist && (Vesnice.Budovy[A][B].akce_budovy != null || Vesnice.Budovy[A][B].akce_budovy != "Craft"))
                {
                    Craft.IsEnabled = false;
                }
                if (Vesnice.Budovy[A][B] is Domov)
                {
                    Mnozeni.Visibility = Visibility.Visible;
                }
            }
        }

        public void delnici_zapis()
        {
            if (A == -1 || B == -1)
            {
                int number_in_list = 0;
                foreach (Postava postava in Vesnice.Drevorub)
                {
                    vypis_postavu(postava, number_in_list);
                    number_in_list++;
                }
                Stavba.Content = "Těžba";
            }
            else if (A == -2 || B == -2)
            {
                int number_in_list = 0;
                foreach (Postava postava in Vesnice.Kamenolomec)
                {
                    vypis_postavu(postava, number_in_list);
                    number_in_list++;
                }
                Stavba.Content = "Těžba";
            }
            else if (A == -3 || B == -3)
            {
                int number_in_list = 0;
                foreach (Postava postava in Vesnice.Branana)
                {
                    vypis_postavu(postava, number_in_list);
                    number_in_list++;
                }

                Stavba.Content = "Přidat jednotku";
            }
            Vyvoj.Visibility = Visibility.Collapsed;
            Uceni.Visibility = Visibility.Collapsed;
            Craft.Visibility = Visibility.Collapsed;
        }
            

        private void Stavba_Click(object sender, RoutedEventArgs e)
        {
            //Vesnice.Budovy[A][B].Splneno_Na_Postaveni++;
            if (A > -1 || B > -1)
            {
                Vesnice.Budovy[A][B].nastavena_akce = "Stavba";
            }
            VyvolavaciOkno.Navigate(new vyber(VyvolavaciOkno, A, B, "Postava"));
        }

        public void Mnozeni_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Budovy[A][B].nastavena_akce = "Mnozeni";
            VyvolavaciOkno.Navigate(new vyber(VyvolavaciOkno, A, B, "Mnozeni"));
        }

        private void Zpet_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Ukonci_podokno();
        }

        private void Odebrat_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int id = int.Parse(button.Name.Substring(2, button.Name.Length - 2));
            if (A == -1 || B == -1)
            {
                Vesnice.Drevorub.RemoveAt(id);
                pracovnici.Children.Clear();
                delnici_zapis();
            }
            else if (A == -2 || B == -2)
            {
                Vesnice.Kamenolomec.RemoveAt(id);
                pracovnici.Children.Clear();
                delnici_zapis();
            }
            else if (A == -3 || B == -3)
            {
                Vesnice.Branana.RemoveAt(id);
                pracovnici.Children.Clear();
                delnici_zapis();
            }
            else
            {
                Vesnice.Budovy[A][B].pracovnici.RemoveAt(id);
                pracovnici.Children.Clear();
                pracovnici_zapis();
            }
            
        }

        public void vypis_postavu(Postava postava, int number_in_list)
        {
            Border okoli = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(3, 3, 3, 3), Width = 230, Height = 30, Margin = new Thickness(2, 2, 2, 2) };
            StackPanel obsah = new StackPanel() { Orientation = Orientation.Horizontal };
            Image obr = new Image() { Margin = new Thickness(5, 0, 5, 0), Height = 20, Width = 20 };
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            bit.UriSource = new Uri("../img/" + postava.obr_odkaz, UriKind.Relative);
            bit.EndInit();
            obr.Source = bit;
            obsah.Children.Add(obr);
            TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Věk: " + postava.vek };
            obsah.Children.Add(Vek);
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
            obsah.Children.Add(Pohlavi);
            Button button = new Button() { Margin = new Thickness(5, 0, 5, 0), Content = "X", Height = 20, FontWeight = FontWeights.Bold, Width = 20, Foreground = Brushes.Red, Name = "ID" + number_in_list };
            button.Click += new RoutedEventHandler(Odebrat_Click);
            obsah.Children.Add(button);

            okoli.Child = obsah;

            pracovnici.Children.Add(okoli);
        }
    }
}
