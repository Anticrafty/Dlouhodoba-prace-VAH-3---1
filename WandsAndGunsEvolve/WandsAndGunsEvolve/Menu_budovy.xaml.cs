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

            pracovnici_zapis();
        }

        public void pracovnici_zapis()
        {

            Vesnice.Budovy[A][B].pracovnici = Vesnice.Obyvatele;

            foreach (Postava postava in Vesnice.Budovy[A][B].pracovnici)
            {
                Border okoli = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(3, 3, 3, 3), Width = 230, Height = 30, Margin = new Thickness(2, 2, 2, 2) };
                StackPanel obsah = new StackPanel() { Orientation = Orientation.Horizontal };
                Image obr = new Image() { Margin = new Thickness(5, 0, 5, 0), Height = 20, Width = 20 };
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri("img/" + postava.obr_odkaz, UriKind.Relative);
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
                Button button = new Button() { Margin = new Thickness(5, 0, 5, 0), Content = "X", Height = 20, FontWeight = FontWeights.Bold, Width = 20, Foreground = Brushes.Red, Name = "ID" + postava.ID };
                button.Click += new RoutedEventHandler(Odebrat_Click);
                obsah.Children.Add(button);

                okoli.Child = obsah;

                pracovnici.Children.Add(okoli);
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
                if (!Vesnice.Budovy[A][B].Vyvoj_exist)
                {
                    Vyvoj.IsEnabled = false;
                }
                if (!Vesnice.Budovy[A][B].Uceni_exist)
                {
                    Uceni.IsEnabled = false;
                }
                if (!Vesnice.Budovy[A][B].Crafting_exist)
                {
                    Craft.IsEnabled = false;
                }
                if (Vesnice.Budovy[A][B] is Domov)
                {
                    Mnozeni.Visibility = Visibility.Visible;
                }
            }
        }
    
        private void Stavba_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Budovy[A][B].Splneno_Na_Postaveni++;
            Vesnice.Ukonci_podokno();
        }

        private void Zpet_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Ukonci_podokno();
        }

        private void Odebrat_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int id = int.Parse(button.Name.Substring(2, button.Name.Length - 2));
            Vesnice.Budovy[A][B].pracovnici.RemoveAt(id);
            pracovnici.Children.Clear();
            pracovnici_zapis();
        }
    }
}
