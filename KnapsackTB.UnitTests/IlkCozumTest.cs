﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnapsackTB.UnitTests
{
    [TestClass]
    public class IlkCozumTest
    {
        [TestMethod]
        public void Test()
        {
            int kapasite = 38;
            List<Eleman> elemanlar = new List<Eleman>();
            elemanlar.Add(new Eleman(22, 30, 0));
            elemanlar.Add(new Eleman(15, 20, 1));
            elemanlar.Add(new Eleman(11, 3, 2));
            elemanlar.Add(new Eleman(1, 2, 3));

            double BASLANGIC_SICAKLIGI = 100; // konsoldan alinacak
            double DURDURMA_SICAKLIGI = 0;

            TavlamaBenzetimi sa = new TavlamaBenzetimi(elemanlar, kapasite, BASLANGIC_SICAKLIGI, DURDURMA_SICAKLIGI);
            int toplam = 0;
            foreach (var eleman in sa.IlkCozum())
                toplam += eleman;

            Assert.IsTrue(toplam < kapasite);
        }

    }
}
