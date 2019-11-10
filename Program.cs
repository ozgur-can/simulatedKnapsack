using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackTB
{
    class Program
    {
        static void Main(string[] args)
        {

            int kapasite = 102; // text dosyasindan okunup buraya atanacak 55
            double BASLANGIC_SICAKLIGI = 100; // konsoldan alinacak
            double DURDURMA_SICAKLIGI = 0;
            //double SOGUMA_DERECESI = 0;
            //int ADIM_SAYISI = 0;

            List<Eleman> elemanlar = new List<Eleman>();
            elemanlar.Add(new Eleman(22, 30, 0));
            elemanlar.Add(new Eleman(15, 20, 1));
            elemanlar.Add(new Eleman(11, 3, 2));
            elemanlar.Add(new Eleman(1, 2, 3));
            elemanlar.Add(new Eleman(19, 2, 4));
            elemanlar.Add(new Eleman(5, 10, 5));
            elemanlar.Add(new Eleman(8, 5, 6));

            elemanlar.Add(new Eleman(21, 2, 7));
            elemanlar.Add(new Eleman(12, 12, 8));
            elemanlar.Add(new Eleman(32, 29, 9));
            elemanlar.Add(new Eleman(32, 33, 10));
            elemanlar.Add(new Eleman(3, 2, 11));
            elemanlar.Add(new Eleman(1, 30, 12));
            elemanlar.Add(new Eleman(19, 55, 13));

            TavlamaBenzetimi tb = new TavlamaBenzetimi(elemanlar, kapasite, BASLANGIC_SICAKLIGI, DURDURMA_SICAKLIGI);

            tb.Tavlama(tb.IlkCozum());
            Console.ReadKey();
        }
    }
}
