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
        public int potreba_dreva;
        public int potreba_kamene;

        public List<Postava> pracovnici = new List<Postava>();
        public string craft_ceho;

        public bool Vyvoj_exist = false;
        public bool Uceni_exist = false;
        public bool Crafting_exist = false;

        public string akce_budovy;

        public abstract void Do();

        public abstract void Vyvoj();
        public abstract void Uceni();
        public abstract void Crafting();

    }
}
