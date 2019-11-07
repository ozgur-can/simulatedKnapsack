using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackTB
{
    class Esya
    {
        private int agirlik;
        private int deger;

        internal Esya(int agirlik, int deger)
        {
            Agirlik = agirlik;
            Deger = deger;
        }

        public int Agirlik { get => agirlik; set => agirlik = value; }
        public int Deger { get => deger; set => deger = value; }
    }
}
