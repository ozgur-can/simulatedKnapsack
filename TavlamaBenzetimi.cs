using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        RastgeleSayi rastgele = new RastgeleSayi();
        List<List<int>> enIyiCozumlerListesi = new List<List<int>>();
        List<TimeSpan> zamanFarklariListesi = new List<TimeSpan>();
        public int Kapasite { get => kapasite; set => kapasite = value; }
        public List<Eleman> Elemanlar { get => elemanlar; set => elemanlar = value; }
        public double BaslangicISI { get => baslangicISI; set => baslangicISI = value; }
        public double DurdurmaISI { get => durdurmaISI; set => durdurmaISI = value; }
        public RastgeleSayi Rastgele { get => rastgele; set => rastgele = value; }
        public List<List<int>> EnIyiCozumlerListesi { get => enIyiCozumlerListesi; set => enIyiCozumlerListesi = value; }
        public List<TimeSpan> ZamanFarklariListesi { get => zamanFarklariListesi; set => zamanFarklariListesi = value; }

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
        public void Tavlama(int adimSayisi, string dosyaAdi)
        {
            RastgeleSayi rastgele = new RastgeleSayi();

            //sure baslangic
            DateTime sureBas = DateTime.Now;

            List<int> cozum = new List<int>(IlkCozum());

            int fark;
            int guncelDeger;

            List<int> enIyiCozum = new List<int>(cozum);
            int enIyiDeger = DegerHesapla(enIyiCozum);

            List<int> yeniCozum = new List<int>(cozum);
            List<int> komsu = new List<int>();

            do
            {
                guncelDeger = DegerHesapla(enIyiCozum);
                for (int i = 0; i <= adimSayisi; i++)
                {
                    komsu = VaryasyonlariHesapla(yeniCozum);
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
                        if (Math.Exp(-fark / BaslangicISI) > rastgele.BetweenDouble(0, 1))
                        {
                            yeniCozum = komsu;
                        }
                    }
                    BaslangicISI -= 0.1;
                }
            } while ((BaslangicISI > DurdurmaISI) && (guncelDeger < enIyiDeger));

            //sure bitis
            TimeSpan zamanFarki = DateTime.Now - sureBas;

            EnIyiCozumlerListesi.Add(enIyiCozum);
            ZamanFarklariListesi.Add(zamanFarki);

            Console.WriteLine(DegerHesapla(enIyiCozum) + " " + HacimHesapla(enIyiCozum) + " " + zamanFarki.TotalMilliseconds);
        }

        public void CiktiVer(List<List<int>> enIyiCozum, List<TimeSpan> zamanFarki, string dosyaAdi)
        {
            Dictionary<double, TimeSpan> ciktilar = new Dictionary<double, TimeSpan>();
            double toplamDeger = 0, ortalamaDeger;

            for (int i = 0; i < enIyiCozum.Count; i++)
            {
                ciktilar.Add(DegerHesapla(enIyiCozum[i]), zamanFarki[i]);
            }

            for (int i = 0; i < ciktilar.Count; i++)
                toplamDeger += ciktilar.ElementAt(i).Key;

            ortalamaDeger = toplamDeger / ciktilar.Count;

            double enIyiCiktiDegeri = ciktilar.Keys.Max();
            TimeSpan enIyiCiktiSuresi = ciktilar[enIyiCiktiDegeri];

            DosyayaYazdir dosya = new DosyayaYazdir();
            dosya.Yaz(dosyaAdi, ortalamaDeger, ciktilar.Keys.Max(), enIyiCiktiSuresi);
        }

        public List<int> VaryasyonlariHesapla(List<int> tempCozum)
        {
            List<int> yedekTemp = new List<int>(tempCozum);
            int rast = 0;
            List<int> baslangicCozum = new List<int>(yedekTemp);
            List<int> secilmemis = SecilmemislerElemanlar(yedekTemp);
            List<int> yeniCozum = new List<int>();
            int agirlik = 0;

            for (int i = 0; i < 3; i++)
            {
                rast = RastgeleSecim(secilmemis);
                yedekTemp.Add(rast);
                secilmemis.Remove(rast);
                agirlik = HacimHesapla(yedekTemp);

                if (agirlik > Kapasite)
                {
                    while (agirlik > Kapasite)
                    {
                        rast = RastgeleSecim(yedekTemp);
                        yedekTemp.Remove(rast);
                        secilmemis.Add(rast);
                        yeniCozum = new List<int>(yedekTemp);
                        agirlik = HacimHesapla(yedekTemp);
                    }

                    var fark = yeniCozum.Except(baslangicCozum).Count();
                    if (yeniCozum.Any() && (fark != 0))
                        break;
                }
            }

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

            return secilmisler;
        }

        public List<int> SecilmemislerElemanlar(List<int> cozum)
        {
            List<int> secilmemisler = new List<int>();
            for (int i = 0; i < Elemanlar.Count; i++)
                if (!cozum.Contains(Elemanlar[i].Indis))
                    secilmemisler.Add(Elemanlar[i].Indis);

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

        public int RastgeleSayiGetir(int sinirDeger)
        {
            return Rastgele.Between(0, sinirDeger - 1);
        }

    }
}