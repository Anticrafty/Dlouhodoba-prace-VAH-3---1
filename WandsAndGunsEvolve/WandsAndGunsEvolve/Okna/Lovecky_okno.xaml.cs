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
        int health_lovenyho = 0;

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
            if (Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu == 0)
            {
                Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu++;
            }
        }

        private void postupny_boj()
        {
            doba_akce = new DispatcherTimer();
            doba_akce.Tick += new EventHandler(TahNovy);
            doba_akce.Interval = new TimeSpan(0, 0, 3);
            doba_akce.Start();
            TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lov začal..." };
            akce_loveni.Children.Add(Vek);
        }
        private void Stop_boj()
        {
            Ustup.IsEnabled = false;
            doba_akce.Stop();
            Ustup.IsEnabled = false;
            TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lov byl dokončen..." };
            Ustup.IsEnabled = false;
            akce_loveni.Children.Add(Vek);
            Ustup.IsEnabled = false;

        }

        public void Zpet_Click(object sender, RoutedEventArgs e)
        {
            
            Vesnice.Ukonci_podokno();
            doba_akce.Stop();
            Vesnice.Branana = new List<Postava>();
            Vesnice.prepocitej_postavy();
            Vesnice.prepocitej_suroviny();
        }

        public void TahNovy(object sender, EventArgs e)
        {
            if (Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu < 1)
            {
                Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu = 0;
                Vesnice.Branana[ID_aktualniho_lovce].zivy = false;
                dalsi_lovec();
               
            }
            else
            {
                if (aktualni_loveny == null)
                {
                    vytvor_novou_lovenou();
                }
                else
                {
                    boj_o_korist();
                }
            }
            

        }

        public void vytvor_novou_lovenou()
        {
            //   3       5             4         3               2              3         1              1
            // 0-3 nic  4-9 Pták, 10-14 zajíc, 15-18 Jelen, 19-20 Medved, 21-24 Vlk, 25 - Drak, 26 - Jednorozec,

            int nahodna_lovena = Vesnice.rnd_s.Next(0, 27);
            if(nahodna_lovena < 4)
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " nic nenašel." };
                akce_loveni.Children.Add(Vek);
                int bobule = Vesnice.rnd_s.Next(0, 3);
                if (bobule != 0)
                {
                    Vesnice.jidlo = Vesnice.jidlo + bobule;
                    Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + "  alespoň přinesl " + bobule + "bobulí." };
                    akce_loveni.Children.Add(Vek);
                }
                dalsi_lovec();

            }
            else if (nahodna_lovena < 10)
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " našel Ptáka." };
                aktualni_loveny = "Ptak";
                health_lovenyho = 1;
                akce_loveni.Children.Add(Vek);
            }
            else if ( nahodna_lovena < 15)
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " našel Zajíce." };
                aktualni_loveny = "Zajic";
                health_lovenyho = 1;
                akce_loveni.Children.Add(Vek);
            }
            else if ( nahodna_lovena < 19)
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " našel Jelena." };
                aktualni_loveny = "Jelen";
                health_lovenyho = 3;
                akce_loveni.Children.Add(Vek);
            }
            else if ( nahodna_lovena < 21)
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " našel Medvěda." };
                aktualni_loveny = "Medved";
                health_lovenyho = 4;
                akce_loveni.Children.Add(Vek);
            }
            else if (nahodna_lovena < 25)
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " našel Vlka." };
                aktualni_loveny = "Vlk";
                health_lovenyho = 2;
                akce_loveni.Children.Add(Vek);
            }
            else if( nahodna_lovena == 25)
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " našel Draka." };
                aktualni_loveny = "Drak";
                health_lovenyho = 7;
                akce_loveni.Children.Add(Vek);
            }
            else if (nahodna_lovena == 26)
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " našel Jednorožce." };
                aktualni_loveny = "Jednorozec";
                health_lovenyho = 4;
                akce_loveni.Children.Add(Vek);
            }
        }

        public void dalsi_lovec()
        {
            if (Vesnice.Branana.Count() - 1 != ID_aktualniho_lovce)
            {
                ID_aktualniho_lovce++;
                if(Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu < 1)
                {
                    Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu++;
                }
            }
            else
            {
                Stop_boj();
            }
        }

        public void boj_o_korist()
        {
            int sance_lovce = Vesnice.rnd_s.Next(0, Vesnice.Branana[ID_aktualniho_lovce].Vzdalenost_Utoku + Vesnice.Branana[ID_aktualniho_lovce].Rychlost + 1);
            int sance_lovenyho = 0;
            int Co_se_bude_dit = Vesnice.rnd_s.Next(0, 3);
            // vzdalenost utoku 
            // utoko - obrana
            if (aktualni_loveny == "Ptak")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 7);
            }
            else if (aktualni_loveny == "Zajic")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 6);
            }
            else if (aktualni_loveny == "Jelen")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 5);
            }
            else if (aktualni_loveny == "Medved")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 5);
            }
            else if (aktualni_loveny == "Vlk")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 6);
            }
            else if (aktualni_loveny == "Drak")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 7);
            }
            else if (aktualni_loveny == "Jednorozec")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 8);
            }

            if (Co_se_bude_dit == 0)
            {
                if (sance_lovenyho > sance_lovce )
                {
                    aktualni_loveny = null;
                    TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + "  Ztratil svou kořist" };                   
                    akce_loveni.Children.Add(Vek);
                    int bobule = Vesnice.rnd_s.Next(0, 3);
                    if(bobule != 0)
                    {
                        Vesnice.jidlo = Vesnice.jidlo + bobule;
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + "  alespoň přinesl " + bobule + "bobulí."};
                        akce_loveni.Children.Add(Vek);
                    }
                    dalsi_lovec();
                }          
                else
                {
                    TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " se pokusil utéct." };
                    akce_loveni.Children.Add(Vek);
                }
            }
            if (Co_se_bude_dit == 1)
            {           
                if ( sance_lovce > sance_lovenyho)
                {
                    int utok_lovce = Vesnice.rnd_s.Next(0, Vesnice.Branana[ID_aktualniho_lovce].Utok + 2);
                    health_lovenyho = health_lovenyho - utok_lovce;
                    if(utok_lovce == 0)
                    {
                        TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + " sotva škrábnul " + aktualni_loveny };
                        akce_loveni.Children.Add(Vek);
                    }
                    else if (utok_lovce > Vesnice.Branana[ID_aktualniho_lovce].Utok )
                    {
                        TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + " dal kritický zásah " + aktualni_loveny };
                        akce_loveni.Children.Add(Vek);
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " má " + health_lovenyho + " životů." };
                        akce_loveni.Children.Add(Vek);
                    }
                    else
                    {
                        TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + " trefil " + aktualni_loveny };
                        akce_loveni.Children.Add(Vek);
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " má " + health_lovenyho + " životů." };
                        akce_loveni.Children.Add(Vek);
                    }
                    
                }
                else
                {
                    TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + " se netrefil " };
                    akce_loveni.Children.Add(Vek);
                }
            
                if (health_lovenyho < 1)
                {
                    TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " Zemřel" };
                    akce_loveni.Children.Add(Vek);

                    int ziskany_jidlo = 0;
                    if (aktualni_loveny == "Ptak")
                    {
                        ziskany_jidlo = Vesnice.rnd_s.Next(1, 3);
                        Vesnice.items.Add("Pirko");
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " bylo z něj získáno pírko" };
                        akce_loveni.Children.Add(Vek);
                    }
                    else if (aktualni_loveny == "Zajic")
                    {
                        ziskany_jidlo = Vesnice.rnd_s.Next(1, 4);
                        Vesnice.items.Add("Kuze");
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " bylo z něj získáno 1 kuze" };
                        akce_loveni.Children.Add(Vek);
                    }
                    else if (aktualni_loveny == "Jelen")
                    {
                        ziskany_jidlo = Vesnice.rnd_s.Next(3, 7);
                        for (int x = 0; x > 4; x++)
                        {
                            Vesnice.items.Add("Kuze");
                        }
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " bylo z něj získáno 3 kůže" };
                        akce_loveni.Children.Add(Vek);
                    }
                    else if (aktualni_loveny == "Medved")
                    {
                        
                        ziskany_jidlo = Vesnice.rnd_s.Next(5, 11);
                        for (int x = 0; x > 7; x++)
                        {
                            Vesnice.items.Add("Kuze");
                        }
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " bylo z něj 6 kůže" };
                        akce_loveni.Children.Add(Vek);
                    }
                    else if (aktualni_loveny == "Vlk")
                    {
                        ziskany_jidlo = Vesnice.rnd_s.Next(2, 5);
                        for (int x = 0; x > 3; x++)
                        {
                            Vesnice.items.Add("Kuze");
                        }
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " bylo z něj získáno 2 kůže" };
                        akce_loveni.Children.Add(Vek);
                    }
                    else if (aktualni_loveny == "Drak")
                    {
                        ziskany_jidlo = Vesnice.rnd_s.Next(10, 31);
                        Vesnice.items.Add("DraciSupina");
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " bylo z něj získáno Dračí šupina" };
                        akce_loveni.Children.Add(Vek);
                    }
                    else if (aktualni_loveny == "Jednorozec")
                    {
                        ziskany_jidlo = Vesnice.rnd_s.Next(5, 21);
                        Vesnice.items.Add("Jednorozci_zine");
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " bylo z něj získáno jednorožčí žíně" };
                        akce_loveni.Children.Add(Vek);
                    }
                    Vesnice.jidlo = Vesnice.jidlo + ziskany_jidlo;
                    Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = " a " + ziskany_jidlo + " jídla" };
                    akce_loveni.Children.Add(Vek);
                    aktualni_loveny = null;
                    dalsi_lovec();
                }
            }
            if (Co_se_bude_dit == 2)
            {
                if (sance_lovenyho > sance_lovce)
                {
                    int utok_lovenyho = 0;
                    if (aktualni_loveny == "Ptak")
                    {
                        utok_lovenyho = Vesnice.rnd_s.Next(0, 2);
                    }
                    else if (aktualni_loveny == "Zajic")
                    {
                        utok_lovenyho = Vesnice.rnd_s.Next(0, 2);
                    }
                    else if (aktualni_loveny == "Jelen")
                    {
                        utok_lovenyho = Vesnice.rnd_s.Next(0, 4);
                    }
                    else if (aktualni_loveny == "Medved")
                    {
                        utok_lovenyho = Vesnice.rnd_s.Next(0, 6);
                    }
                    else if (aktualni_loveny == "Vlk")
                    {
                        utok_lovenyho = Vesnice.rnd_s.Next(0, 5);
                    }
                    else if (aktualni_loveny == "Drak")
                    {
                        utok_lovenyho = Vesnice.rnd_s.Next(0, 20);
                    }
                    else if (aktualni_loveny == "Jednorozec")
                    {
                        utok_lovenyho = Vesnice.rnd_s.Next(0,5) - Vesnice.rnd_s.Next(0, 3);
                    }
                    if (utok_lovenyho == 0)
                    {
                        TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " sotva škrábl " + (ID_aktualniho_lovce + 1) };
                    }
                    else if (utok_lovenyho < 0 )
                    {
                        Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu = Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu - (utok_lovenyho + Vesnice.Branana[ID_aktualniho_lovce].Obrana);
                        TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " Vyléčil " + (ID_aktualniho_lovce + 1) };
                        akce_loveni.Children.Add(Vek);
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " má " + Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu + " životů." };
                        akce_loveni.Children.Add(Vek);
                    }
                    else if( utok_lovenyho - Vesnice.Branana[ID_aktualniho_lovce].Obrana < Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu)
                    {
                        Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu = Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu - (utok_lovenyho - Vesnice.Branana[ID_aktualniho_lovce].Obrana);
                        TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " Trefil lovce " + (ID_aktualniho_lovce + 1) };
                        akce_loveni.Children.Add(Vek);
                        Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec " + (ID_aktualniho_lovce + 1) + " má " + Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu + " životů." };
                        akce_loveni.Children.Add(Vek);
                    }
                    else
                    {
                        Vesnice.Branana[ID_aktualniho_lovce].actual_zivotu = 0;
                        TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " Zabil lovce " + (ID_aktualniho_lovce + 1) };
                        akce_loveni.Children.Add(Vek);
                    }
                }
                else
                {
                    TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = aktualni_loveny + " netrefil lovce " + (ID_aktualniho_lovce + 1) };
                    akce_loveni.Children.Add(Vek);
                }
            }
            Ustup.IsEnabled = true;
        }
        public void Ustup_Click(object sender, RoutedEventArgs e)
        {
            int sance_lovce = Vesnice.rnd_s.Next(0, Vesnice.Branana[ID_aktualniho_lovce].Vzdalenost_Utoku + Vesnice.Branana[ID_aktualniho_lovce].Rychlost + 1);
            int sance_lovenyho = 0;
            if (aktualni_loveny == "Ptak")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 7);
            }
            else if (aktualni_loveny == "Zajic")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 6);
            }
            else if (aktualni_loveny == "Jelen")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 5);
            }
            else if (aktualni_loveny == "Medved")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 5);
            }
            else if (aktualni_loveny == "Vlk")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 6);
            }
            else if (aktualni_loveny == "Drak")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 7);
            }
            else if (aktualni_loveny == "Jednorozec")
            {
                sance_lovenyho = Vesnice.rnd_s.Next(0, 8);
            }

            if (sance_lovenyho < sance_lovce)
            {
                aktualni_loveny = null;
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + " utekl" };
                akce_loveni.Children.Add(Vek);
                dalsi_lovec();
            }
            else
            {
                TextBlock Vek = new TextBlock() { Margin = new Thickness(5, 0, 5, 0), Text = "Lovec  " + (ID_aktualniho_lovce + 1) + " se pokusil utéct." };
                akce_loveni.Children.Add(Vek);
                Ustup.IsEnabled = false;
            }
        }
    }
}
