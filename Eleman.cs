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

        public Eleman(int agirlik, int deger)
        {
            Agirlik = agirlik;
            Deger = deger;
        }

        public int Agirlik { get => agirlik; set => agirlik = value; }
        public int Deger { get => deger; set => deger = value; }
    }
}
