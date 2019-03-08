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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button butt = sender as Button;
            string content = butt.Content.ToString();
            if (content == "Ano")
            {
                
                    Vesnice.Potvrzeni(A,B,Co);
                    Vesnice.Ukonci_podokno();

            }
            else
            {
                Vesnice.Ukonci_podokno();
            }
        }
    }
}
