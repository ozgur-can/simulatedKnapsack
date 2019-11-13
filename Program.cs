using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackTB
{
    class Program
    {
        static void Main(string[] args)
        {
            int kapasite = 0;
            double BASLANGIC_SICAKLIGI = 0;
            double DURDURMA_SICAKLIGI = 0;
            int ADIM_SAYISI = 0;

            VeriOkuma veri = new VeriOkuma();

            Console.Write("ADIM SAYISINI GİRİNİZ = ");
            ADIM_SAYISI = Convert.ToInt32(Console.ReadLine());

            Console.Write("BAŞLANGIÇ SICAKLIĞINI GİRİNİZ = ");
            BASLANGIC_SICAKLIGI = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= 10; i++)
            {
                TavlamaBenzetimi tb = new TavlamaBenzetimi(veri.elemanlarListesi("test" + i + ".txt"), veri.Kapasite, BASLANGIC_SICAKLIGI, DURDURMA_SICAKLIGI);

                Console.WriteLine("test" + i + ".txt");
                for (int j = 0; j < 10; j++)
                {
                    tb.Tavlama(ADIM_SAYISI, "test" + i + "_4_results.txt");
                    tb.CiktiVer(tb.EnIyiCozumlerListesi, tb.ZamanFarklariListesi, "test" + i + "_4_results.txt");
                }

            }
            Console.WriteLine("Program Calismayi Durdurdu.");
            Console.ReadKey();
        }
    }
}
