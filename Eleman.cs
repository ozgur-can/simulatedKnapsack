using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackTB
{
    public class Eleman
    {
        private int agirlik;
        private int deger;
        private int indis;

        public Eleman(int agirlik, int deger, int indis)
        {
            Agirlik = agirlik;
            Deger = deger;
            Indis = indis;
        }

        public int Agirlik { get => agirlik; set => agirlik = value; }
        public int Deger { get => deger; set => deger = value; }
        public int Indis { get => indis; set => indis = value; }
    }
}
