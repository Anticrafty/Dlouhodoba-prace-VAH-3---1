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
    /// Interakční logika pro DialogovyFrame.xaml
    /// </summary>
    public partial class DialogovyFrame : Page
    {
        private Frame VyvolavaciOkno;
        Rozhovor Dialog;
        int kolikaty_z_listu = 0;

        public DialogovyFrame()
        {
            InitializeComponent();
        }
        public DialogovyFrame(Frame vyvolavac, Rozhovor dialog) : this()
        {
            VyvolavaciOkno = vyvolavac;

            VyvolavaciOkno.Width = 600;
            VyvolavaciOkno.Height = 350;

            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("../img/" + dialog.obr_odkaz,UriKind.Relative);
            b.EndInit();

            Dialog = dialog;
            odesilatel.Source = b;
            Pokracovani(null, null);
        }

        private void Pokracovani(object sender, RoutedEventArgs e)
        {
            if (kolikaty_z_listu + 1 == Dialog.text.Count())
            {
                Text.Text = Dialog.text[kolikaty_z_listu];
                kolikaty_z_listu++;
            }
            else
            {
                MainMenu.Ukonci_podokno();
            }
            

        }
    }
}
