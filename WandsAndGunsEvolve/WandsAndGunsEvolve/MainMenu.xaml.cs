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
    /// Interakční logika pro MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public static Frame prologovac;
        private Frame PredchoziOkno;

        public MainMenu()
        {
            prologovac = Prolog;
            InitializeComponent();
        }
        public MainMenu( Frame Window ) : this()
        {
            this.PredchoziOkno = Window;
            Application.Current.MainWindow.Height = 500;
            Application.Current.MainWindow.Width = 540;
            and.Text = "" + (char)0X26;
        }
        private void Nova_hra(object sender, RoutedEventArgs e)
        {
            List<string> mluva = new List<string>();
            string vesnican_obr_odkaz = "wallpaper-for-facebook-profile-photo.jpg";
            Rozhovor prolog = new Rozhovor() { text = mluva, obr_odkaz = vesnican_obr_odkaz };
            PredchoziOkno.Navigate(new DialogovyFrame(PredchoziOkno,prolog));
        }
        
        private void Nacist_Hru(object sender, RoutedEventArgs e)
        {

        }

        private void Konec(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        static public void EndProlog()
        {
            prologovac = null;
        }

    }
}
