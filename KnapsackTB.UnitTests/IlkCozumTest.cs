using System;
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
            elemanlar.Add(new Eleman(22, 30));
            elemanlar.Add(new Eleman(15, 20));
            elemanlar.Add(new Eleman(11, 3));
            elemanlar.Add(new Eleman(1, 2));

            TavlamaBenzetimi sa = new TavlamaBenzetimi(elemanlar, kapasite);
            int toplam = 0;
            foreach (var eleman in sa.IlkCozum())
                toplam += eleman;

            Assert.IsTrue(toplam < kapasite);
        }
    }
}
