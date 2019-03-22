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
    /// Interakční logika pro Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        Frame VyvolavaciOkno;

        public Inventory()
        {
            InitializeComponent();
        }
        public Inventory(Frame vyvolavac) : this()
        {
            VyvolavaciOkno = vyvolavac;

            VyvolavaciOkno.Width = 600;
            VyvolavaciOkno.Height = 450;

            int aktualni_sloupec = 1;
            foreach (string item in Vesnice.items)
            {
                StackPanel obsah = new StackPanel() { Orientation = Orientation.Horizontal };
                Image obr = new Image() {  Height = 50, Width = 50 };
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri("../img/" + item + ".png", UriKind.Relative);
                bit.EndInit();
                obr.Source = bit;

                TextBlock Vek = new TextBlock() { Margin = new Thickness(-20, 0, 0, 0), Text = "1", FontWeight = FontWeights.Bold, Foreground = Brushes.Red, FontSize = 24 };
                obsah.Children.Add(obr);
                obsah.Children.Add(Vek);
                
                Border button = new Border() {  Height = 50, Width = 50, Child = obsah, BorderThickness = new Thickness(2,2,2,2), BorderBrush = Brushes.Black};

                string jmeno_Radku = "Radek" + aktualni_sloupec;
                foreach (object obj in Radek.Children)
                {
                    if (obj is StackPanel)
                    {
                        StackPanel radek = obj as StackPanel;
                        if(radek.Name == jmeno_Radku)
                        {
                            radek.Children.Add(button);
                        }
                    }
                }
                aktualni_sloupec++;
                if(aktualni_sloupec == 9)
                {
                    aktualni_sloupec = 1;
                }
            }
        }
        public void Zpet_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Ukonci_podokno();
        }
    }
}
