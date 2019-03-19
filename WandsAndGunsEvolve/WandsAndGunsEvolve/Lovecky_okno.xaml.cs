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
using System.Windows.Threading;

namespace WandsAndGunsEvolve
{
    /// <summary>
    /// Interakční logika pro Lovecky_okno.xaml
    /// </summary>
    public partial class Lovecky_okno : Page
    {
        // 0-3 nic  4-5 Pták, 6-8 Rabbit, 9-10 Jelen, 11 Medved, 12-13 Vlk, 14 - Drak, 15 - Jednorozec,

        new DispatcherTimer doba_akce;
        Frame VyvolavaciOkno;
        bool Speed = false;
        int ID_aktualniho_lovce = 0;
        string aktualni_loveny = null;

        public Lovecky_okno()
        {
            InitializeComponent();
        }

        public Lovecky_okno(Frame vyvolavac, bool speedy) : this()
        {
            VyvolavaciOkno = vyvolavac;
            Speed = speedy;

            VyvolavaciOkno.Width = 400;
            VyvolavaciOkno.Height = 450;
            if(speedy)
            {

            }
            else
            {
                postupny_boj();
            }
        }

        private void postupny_boj()
        {
            doba_akce = new DispatcherTimer();
            doba_akce.Tick += new EventHandler(TahNovy);
            doba_akce.Interval = new TimeSpan(0, 0, 2);
            doba_akce.Start();
        }
        private void Stop_boj()
        {
            doba_akce.Stop();
            Border okoli = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(3, 3, 3, 3), Width = 230, Height = 30, Margin = new Thickness(2, 2, 2, 2) };
            TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lov byl dokončen..." };
            okoli.Child = Vek;

        }

        public void Zpet_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Ukonci_podokno();
        }

        public void TahNovy(object sender, EventArgs e)
        {
            if (Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu < 1)
            {
                Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu = 0;
                Vesnice.Branana[ID_aktualniho_lovce].zivy = false;
                dalsi_lovec();
               
            }
            if(aktualni_loveny == null)
            {
                vytvor_novou_lovenou();
            }
        }

        public void vytvor_novou_lovenou()
        {
            // 0-3 nic  4-5 Pták, 6-8 Rabbit, 9-10 Jelen, 11 Medved, 12-13 Vlk, 14 - Drak, 15 - Jednorozec,

            int nahodna_lovena = Vesnice.rnd_s.Next(0, 16);
            if(nahodna_lovena < 4)
            {
                Border okoli = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(3, 3, 3, 3), Width = 230, Height = 30, Margin = new Thickness(2, 2, 2, 2) };
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + "nic nenašel." };
                okoli.Child = Vek;
                dalsi_lovec();

            }
            else if (nahodna_lovena < 6)
            {
                Border okoli = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(3, 3, 3, 3), Width = 230, Height = 30, Margin = new Thickness(2, 2, 2, 2) };
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec našel Ptáka." };
                okoli.Child = Vek;
            }
        }

        public void dalsi_lovec()
        {
            if (Vesnice.Branana.Count() - 1 != ID_aktualniho_lovce)
            {
                ID_aktualniho_lovce++;
            }
            else
            {
                Stop_boj();
            }
        }
    }
}
