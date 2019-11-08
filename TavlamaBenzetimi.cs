using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackTB
{
    public class TavlamaBenzetimi
    {
        private int kapasite;
        private List<Eleman> elemanlar = new List<Eleman>();

        public int Kapasite { get => kapasite; set => kapasite = value; }
        public List<Eleman> Elemanlar { get => elemanlar; set => elemanlar = value; }

        public TavlamaBenzetimi(List<Eleman> elemanlar, int kapasite)
        {
            Kapasite = kapasite;
            Elemanlar = elemanlar;
        }

        public List<int> IlkCozum()
        {
            List<int> ilkCozum = new List<int>();
            List<int> tempCozum = new List<int>();
            List<int> indisler = new List<int>();
            int temp = 0;

            for (int i = 0; i < Elemanlar.Count; i++)
                indisler.Add(i);

            while (indisler.Count > 0)
            {
                temp = RastgeleSayiGetir(indisler.Count);
                tempCozum.Add(indisler.ElementAt(temp));

                if (HacimHesapla(tempCozum) < Kapasite)
                    ilkCozum.Add(indisler.ElementAt(temp));

                indisler.RemoveAt(temp);
            }
            
            return ilkCozum;
        }

        private int HacimHesapla(List<int> list)
        {
            int hacim = 0;

            foreach (int esya in list)
                hacim += Elemanlar.ElementAt(esya).Agirlik;
            return hacim;
        }

        private int DegerHesapla(List<int> list)
        {
            int deger = 0;

            foreach (int esya in list)
                deger += Elemanlar.ElementAt(esya).Deger;
            return deger;
        }

        private int RastgeleSayiGetir(int sinirDeger)
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
