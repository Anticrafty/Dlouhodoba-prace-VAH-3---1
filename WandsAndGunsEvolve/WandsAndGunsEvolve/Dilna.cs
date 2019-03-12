using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandsAndGunsEvolve
{
    class Dilna : Budova
    {
        public Dilna()
        {
            obr_odkaz = @"Dilna.png";
            Potreba_Na_Postaveni = 5;
            Crafting_exist = true;
            Uceni_exist = true;
            Vyvoj_exist = true;
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
