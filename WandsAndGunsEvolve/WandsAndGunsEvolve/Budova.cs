﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WandsAndGunsEvolve
{
    public abstract class Budova
    {
        public string obr_odkaz;
        public int Y_sloupec;
        public int X_radek;
        public int Potreba_Na_Postaveni;
        public int Splneno_Na_Postaveni = 0;
        List<Postava> pracovnici = new List<Postava>();

        public abstract void Vyvoj();
        public abstract void Uceni();
    }
}
