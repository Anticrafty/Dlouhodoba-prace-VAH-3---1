using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandsAndGunsEvolve
{
    public class Postava
    {
        public int ID;
        public string obr_odkaz;
        public int Max_Zivotu = 5;
        public int actual_zivotu = 5;
        public int Utok = 1;
        public int Vzdalenost_Utoku = 1;
        public int Rychlost = 2;
        public int Obrana = 0;
        public int Postava_za_Den = 1;
        public int vek = 0;
        public bool muzstvi;
        public bool zivy = true;
    }
}
