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

        public Menu_budovy(Frame vyvolavac , int a, int b) : this()
        {
            VyvolavaciOkno = vyvolavac;

            A = a;
            B = b;

            VyvolavaciOkno.Width = 450;
            VyvolavaciOkno.Height = 450;

            foreach (Postava postava in Vesnice.Budovy[A][B].pracovnici)
            {

            }

            if(Vesnice.Budovy[A][B].Splneno_Na_Postaveni < Vesnice.Budovy[A][B].Potreba_Na_Postaveni)
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
                if(Vesnice.Budovy[A][B] is Domov)
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
    }
}
