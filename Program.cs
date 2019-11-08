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

            int kapasite = 38; // text dosyasindan okunup buraya atanacak
            double BASLANGIC_SICAKLIGI = 0; // konsoldan alinacak
            double DURDURMA_SICAKLIGI = 0;
            double SOGUMA_DERECESI = 0;
            int ADIM_SAYISI = 0;

            List<Eleman> elemanlar = new List<Eleman>();
            elemanlar.Add(new Eleman(22, 30));
            elemanlar.Add(new Eleman(15, 20));
            elemanlar.Add(new Eleman(11, 3));
            elemanlar.Add(new Eleman(1, 2));

            TavlamaBenzetimi tb = new TavlamaBenzetimi(elemanlar, kapasite);

            tb.IlkCozum();
            //Console.WriteLine(tb.Benzetim(BASLANGIC_SICAKLIGI, DURDURMA_SICAKLIGI, SOGUMA_DERECESI, ADIM_SAYISI, tb.CozumAra()));
            Console.ReadKey();
        }
    }
}
