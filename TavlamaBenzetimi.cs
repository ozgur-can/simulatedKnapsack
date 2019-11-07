using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackTB
{
    class TavlamaBenzetimi
    {
        private int kapasite;
        private List<Esya> esyalar = new List<Esya>();

        public int Kapasite { get => kapasite; set => kapasite = value; }
        internal List<Esya> Esyalar { get => esyalar; set => esyalar = value; }

        public TavlamaBenzetimi(List<Esya> esyalar, int kapasite)
        {
            Kapasite = kapasite;
            Esyalar = esyalar;
        }

        internal List<int> IlkCozum()
        {
            List<int> ilkCozum = new List<int>();
            List<int> tempCozum = new List<int>();
            List<int> indisler = new List<int>();
            int temp = 0;

            for (int i = 0; i < Esyalar.Count; i++)
                indisler.Add(i);

            while (indisler.Count > 0)
            {
                temp = RastgeleSayiGetir(indisler.Count);
                tempCozum.Add(indisler.ElementAt(temp));
                indisler.RemoveAt(temp);
                if (HacimHesapla(tempCozum) < Kapasite)
                    ilkCozum.Add(temp);
            }

            //foreach (var i in ilkCozum)
            //    Console.WriteLine(i);

            return ilkCozum;
        }

        private int HacimHesapla(List<int> list)
        {
            int hacim = 0;

            foreach (int esya in list)
                hacim += Esyalar.ElementAt(esya).Agirlik;
            return hacim;
        }

        private int DegerHesapla(List<int> list)
        {
            int deger = 0;

            foreach (int esya in list)
                deger += Esyalar.ElementAt(esya).Deger;
            return deger;
        }

        internal int RastgeleSayiGetir(int sinirDeger)
        {
            Random a = new Random();

            List<int> rastgeleSayilar = new List<int>();
            int rastgele = 0;

            rastgele = a.Next(0, sinirDeger);
            if (!rastgeleSayilar.Contains(rastgele))
                rastgeleSayilar.Add(rastgele);

            return rastgeleSayilar[0];
        }

    }
}
