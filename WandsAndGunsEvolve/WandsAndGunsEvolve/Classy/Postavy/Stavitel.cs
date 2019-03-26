using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandsAndGunsEvolve.Classy.Postavy
{
    class Stavitel : Postava
    {
        public Stavitel()
        {
            obr_odkaz = "Stavitel.png";
            Max_Zivotu = 6;
            Utok = 2;
            Vzdalenost_Utoku = 1;
            Rychlost = 2;
            Obrana = 0;
            Postava_za_Den = 3;
        }
    }
}
