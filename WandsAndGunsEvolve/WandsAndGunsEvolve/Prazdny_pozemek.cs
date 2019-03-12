using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandsAndGunsEvolve
{
    class Prazdny_pozemek : Budova
    {
        
        public Prazdny_pozemek()
        {
            Potreba_Na_Postaveni = 0;
            Vyvoj_exist = false;
            Uceni_exist = false;
            Crafting_exist = false;
        }

        public override void Crafting()
        {
            throw new NotImplementedException();
        }

        public override void Do()
        {
            throw new NotImplementedException();
        }

        public override void Uceni()
        {
            throw new NotImplementedException();
        }

        public override void Vyvoj()
        {
            throw new NotImplementedException();
        }
    }
}
