﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackTB
{
    class VeriOkuma
    {
        int kapasite;
        public int Kapasite { get => kapasite; set => kapasite = value; }

        public List<Eleman> elemanlarListesi(string dosyaIsmi)
        {
            List<Eleman> elemanlar = new List<Eleman>();
            List<int> agirlik = new List<int>();
            List<int> deger = new List<int>();
            StreamReader oku = new StreamReader(dosyaIsmi);
            string[] okunanlar = oku.ReadToEnd().Split('\r', '\n', ' ');
            Kapasite = Convert.ToInt32(okunanlar[0]);
            int i = 1, control = 0;
            do
            {
                if (okunanlar[i] == string.Empty)
                {
                    control++;
                }
                else if (control < 6)
                {
                    agirlik.Add(Convert.ToInt32(okunanlar[i]));
                }
                else
                {
                    deger.Add(Convert.ToInt32(okunanlar[i]));
                }
                i++;
            } while (i < okunanlar.Length);

           

            for (int j = 0; j < agirlik.Count; j++)
            {
                elemanlar.Add(new Eleman(agirlik[j], deger[j], j));
            }
            return elemanlar;
        }
    }
}
