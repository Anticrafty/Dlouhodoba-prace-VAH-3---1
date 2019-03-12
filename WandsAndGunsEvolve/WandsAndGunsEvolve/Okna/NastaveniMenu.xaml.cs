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
    /// Interakční logika pro NastaveniMenu.xaml
    /// </summary>
    public partial class NastaveniMenu : Page
    {
        private Frame VyvolavaciOkno;

        public NastaveniMenu()
        {
            InitializeComponent();
        }

        public NastaveniMenu(Frame vyvolavac) : this()
        {
            VyvolavaciOkno = vyvolavac;

            VyvolavaciOkno.Width =  290;
            VyvolavaciOkno.Height = 220;
        }
        private void Konec(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Zpet_Click(object sender, RoutedEventArgs e)
        {
            Vesnice.Ukonci_podokno();
        }
    }
}
