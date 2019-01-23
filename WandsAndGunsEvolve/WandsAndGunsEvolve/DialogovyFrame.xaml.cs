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

        public DialogovyFrame()
        {
            InitializeComponent();
        }
        public DialogovyFrame(Frame vyvolavac, Rozhovor dialog) : this()
        {
            VyvolavaciOkno = vyvolavac;
            Dialog = dialog;
        }
    }
}
