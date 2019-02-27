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
        Frame VyvolavaciOkno;
        public Menu_budovy()
        {
            InitializeComponent();
        }

        public Menu_budovy(Frame vyvolavac , int a, int b) : this()
        {
            VyvolavaciOkno = vyvolavac;

            VyvolavaciOkno.Width = 450;
            VyvolavaciOkno.Height = 450;
        }


    }
}
