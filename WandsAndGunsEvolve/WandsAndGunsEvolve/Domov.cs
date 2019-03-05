using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandsAndGunsEvolve
{
    public class Domov : Budova
    {

        public Domov()
        {
            obr_odkaz = @"domek.png";
            Potreba_Na_Postaveni = 2;
        }

        public override void Crafting()
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

        public void New_postava()
        {

        }
    }
}
