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

        public int Kapasite { get => kapasite; set => kapasite = value; }
        public List<Eleman> Elemanlar { get => elemanlar; set => elemanlar = value; }
        public double BaslangicISI { get => baslangicISI; set => baslangicISI = value; }
        public double DurdurmaISI { get => durdurmaISI; set => durdurmaISI = value; }
        public RastgeleSayi Rastgele { get => rastgele; set => rastgele = value; }

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

        public List<int> DeepCopy<List>(List<int> item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            List<int> result = (List<int>)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }

        public void Tavlama(List<int> cozum)
        {
            int fark;
            int guncelDeger;

            List<int> enIyiCozum = new List<int>(cozum);
            int enIyiDeger = DegerHesapla(enIyiCozum);

            List<int> yeniCozum = new List<int>(cozum);
            List<int> komsu = new List<int>();

            do
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
                    if (Math.Exp(-fark / BaslangicISI) > 0)
                    {
                        yeniCozum = komsu;
                    }
                }
                BaslangicISI -= 0.1;

            } while ((BaslangicISI > DurdurmaISI) && (guncelDeger < enIyiDeger));


            foreach (var i in SecilmisElemanlar(enIyiCozum))
                Console.Write(i + " ");

            Console.WriteLine("\nDegerler toplami = " + DegerHesapla(enIyiCozum));
            Console.WriteLine("Amac Fonksiyonu Degeri = " + DegerHesapla(enIyiCozum) * HacimHesapla(enIyiCozum));

        }

        public List<int> VaryasyonlariHesapla(List<int> tempCozum)
        {
            List<int> yedekTemp = new List<int>(tempCozum); // yedek tuttum cunku tutmazsan parametredeki listeyi guncelliyor
            //int rastgele = RastgeleSayiGetir(Elemanlar.Count);
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

        public int RastgeleSayiGetir(int sinirDeger)
        {
            return Rastgele.Between(0, sinirDeger - 1);
        }

    }
}
