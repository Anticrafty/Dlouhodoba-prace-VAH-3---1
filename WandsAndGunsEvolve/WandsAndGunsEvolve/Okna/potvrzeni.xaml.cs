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
    /// Interakční logika pro potvrzeni.xaml
    /// </summary>
    public partial class potvrzeni : Page
    {
        private Frame VyvolavaciOkno;
        int A;
        int B;
        string Co;
        public potvrzeni()
        {
           InitializeComponent();

            
        }

        public potvrzeni(Frame vyvolavac, int a, int b,string co) : this()
        {
            VyvolavaciOkno = vyvolavac;
            VyvolavaciOkno.Width = 300;
            VyvolavaciOkno.Height = 100;

            A = a;
            B = b;
            Co = co;

            if(co == "Stavba")
            {
                Nadpis.Text = "Chcete zbořit budovu na tomto pozemku?";
            }
            else if (co == "Kolo")
            {
                Nadpis.Text = "Chcete nové kolo?";
            }
            else if (co == "konec")
            {
                Nadpis.Text = "Cela vesnice vymřela.";
                Ne.Visibility = Visibility.Collapsed;
            }
            else if ( co == "Speedy")
            {
                Nadpis.Text = "Chceš rychlou verzy lovu/boje? Upozornění! Jednotky nebudou moct ustoupit pro svoji záchranu!";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button butt = sender as Button;
            string content = butt.Content.ToString();
            if (content == "Ano")
            {

                Vesnice.Ukonci_podokno();
                Vesnice.Potvrzeni(A,B,Co);

            }
            else
            {

                Vesnice.Ukonci_podokno();
                if (Co == "Speedy")
                {
                    Vesnice.Potvrzeni(A, B, "Normal Speed");
                }
            }
        }
    }
}
