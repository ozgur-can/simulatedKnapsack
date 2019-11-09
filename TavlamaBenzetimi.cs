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
        private double baslangicISI;
        private double durdurmaISI;

        public int Kapasite { get => kapasite; set => kapasite = value; }
        public List<Eleman> Elemanlar { get => elemanlar; set => elemanlar = value; }
        public double BaslangicISI { get => baslangicISI; set => baslangicISI = value; }
        public double DurdurmaISI { get => durdurmaISI; set => durdurmaISI = value; }

        public TavlamaBenzetimi(List<Eleman> elemanlar, int kapasite, double baslangicISI, double durdurmaISI)
        {
            Kapasite = kapasite;
            Elemanlar = elemanlar;
            BaslangicISI = baslangicISI;
            DurdurmaISI = durdurmaISI;
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

        public void Tavlama(List<int> cozum)
        {
            int fark;
            int guncelDeger;

            List<int> enIyiCozum = new List<int>(cozum);
            int enIyiDeger = DegerHesapla(enIyiCozum);

            List<int> yeniCozum = new List<int>();
            List<int> komsu = new List<int>();

            do
            {
                komsu = VaryasyonlariHesapla(cozum);
                guncelDeger = DegerHesapla(komsu);
                fark = DegerHesapla(komsu) - enIyiDeger;
                if (fark >= 0)
                {
                    enIyiCozum = komsu;
                    enIyiDeger = DegerHesapla(enIyiCozum);
                    yeniCozum = komsu;
                }
                else
                {
                    if (Math.Exp(-fark / BaslangicISI) > 0)
                    {
                        yeniCozum = komsu;
                    }
                }
                BaslangicISI -= 10;

            } while ((BaslangicISI > DurdurmaISI) && (guncelDeger < enIyiDeger));

            foreach (var i in enIyiCozum)
            {
                Console.WriteLine(i);
            }

        }

        public List<int> VaryasyonlariHesapla(List<int> tempCozum)
        {
            //int rastgele = RastgeleSayiGetir(Elemanlar.Count);
            int rast = 0;
            List<int> secilmemis = SecilmemislerElemanlar(tempCozum); // 2
            List<int> secilmis = new List<int>();
            List<int> yeniCozum = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                rast = RastgeleSecim(secilmemis);
                tempCozum.Add(rast);
                secilmemis.Remove(rast);

                if (AgirlikFazla(HacimHesapla(tempCozum)))
                {
                    secilmis = SecilmisElemanlar(tempCozum);
                    while (AgirlikFazla(HacimHesapla(tempCozum)))
                    {
                        rast = RastgeleSecim(tempCozum);
                        tempCozum.Remove(rast);
                        yeniCozum = new List<int>(tempCozum);
                    }
                    if (!AgirlikFazla(HacimHesapla(tempCozum)))
                    {
                        secilmemis.Add(rast);
                    }
                }//else {yeniCozum = tempCozum; break; }
            }

            foreach (var i in yeniCozum)
                Console.WriteLine(i);

            return yeniCozum;

        }

        public bool AgirlikFazla(int sayi)
        {
            if (sayi > Kapasite)
                return true;
            else return false;
        }

        public int RastgeleSecim(List<int> cozum)
        {
            return cozum[RastgeleSayiGetir(cozum.Count)];
        }

        public List<int> SecilmisElemanlar(List<int> cozum)
        {
            List<int> secilmisler = new List<int>();
            for (int i = 0; i < Elemanlar.Count; i++)
                if (cozum.Contains(Elemanlar[i].Indis))
                    secilmisler.Add(Elemanlar[i].Indis);

            //Elemanlar 0 1 2 3
            //cozum 0 1 3
            //secilmemisler 2

            //foreach (var i in secilmemisler)
            //    Console.WriteLine(i);

            return secilmisler;
        }

        public List<int> SecilmemislerElemanlar(List<int> cozum)
        {
            List<int> secilmemisler = new List<int>();
            for (int i = 0; i < Elemanlar.Count; i++)
                if (!cozum.Contains(Elemanlar[i].Indis))
                    secilmemisler.Add(Elemanlar[i].Indis);

            //Elemanlar 0 1 2 3
            //cozum 0 1 3
            //secilmemisler 2

            //foreach (var i in secilmemisler)
            //    Console.WriteLine(i);

            return secilmemisler;
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
