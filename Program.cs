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
            double BASLANGIC_SICAKLIGI = 100; // konsoldan alinacak
            double DURDURMA_SICAKLIGI = 0;
            //double SOGUMA_DERECESI = 0;
            //int ADIM_SAYISI = 0;

            List<Eleman> elemanlar = new List<Eleman>();
            elemanlar.Add(new Eleman(22, 30, 0));
            elemanlar.Add(new Eleman(15, 20, 1));
            elemanlar.Add(new Eleman(11, 3, 2));
            elemanlar.Add(new Eleman(1, 2, 3));

            TavlamaBenzetimi tb = new TavlamaBenzetimi(elemanlar, kapasite, BASLANGIC_SICAKLIGI, DURDURMA_SICAKLIGI);

            //tb.IlkCozum();
            //Console.WriteLine(tb.Benzetim(BASLANGIC_SICAKLIGI, DURDURMA_SICAKLIGI, SOGUMA_DERECESI, ADIM_SAYISI, tb.CozumAra()));

            //tb.VaryasyonlariHesapla(deneme);

            //tb.SecilmemislerElemanlar(deneme);

            tb.Tavlama(tb.IlkCozum());

            //name_list2 = DeepCopy<List<string>>(name_list1);
            //List<int> ne = new List<int>();
            //List<int> ne2 = new List<int>();
            //ne2.Add(12);
            //ne = Deneme(ne2);
            //ne = tb.DeepCopy<List<int>>(ne2);

            Console.ReadKey();
        }

        static List<int> Deneme(List<int> dene)
        {
            return dene;
        }
    }
}
