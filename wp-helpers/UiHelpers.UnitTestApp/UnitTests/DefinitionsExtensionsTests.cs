using System;
using System.Linq;
using System.Windows;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using UiHelpers.Extensions;

namespace UiHelpers.TestApp.UnitTests
{
    [TestClass]
    public class DefinitionsExtensionsTests
    {
        [TestMethod]
        public void TestNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => DefinitionsExtensions.ParseFormat(null));
        }

        [TestMethod]
        public void TestEmpty()
        {
            Assert.AreEqual(0, DefinitionsExtensions.ParseFormat("").Count());
        }

        [TestMethod]
        public void TestWhitespace()
        {
            Assert.AreEqual(0, DefinitionsExtensions.ParseFormat("   ").Count());
        }

        [TestMethod]
        public void TestAuto()
        {
            var units = DefinitionsExtensions.ParseFormat("Auto").ToList();
            Assert.AreEqual(1,units.Count());
            Assert.AreEqual(GridUnitType.Auto, units[0].GridUnitType);
        }

        [TestMethod]
        public void TestStar()
        {
            var units = DefinitionsExtensions.ParseFormat("*").ToList();
            Assert.AreEqual(1,units.Count());
            Assert.AreEqual(GridUnitType.Star,units[0].GridUnitType);
            Assert.AreEqual(1,units[0].Value);
        }

        [TestMethod]
        public void TestStar2()
        {
            var units = DefinitionsExtensions.ParseFormat("2*").ToList();
            Assert.AreEqual(1, units.Count());
            Assert.AreEqual(GridUnitType.Star, units[0].GridUnitType);
            Assert.AreEqual(2, units[0].Value);
        }

        [TestMethod]
        public void TestPixel()
        {
            var units = DefinitionsExtensions.ParseFormat("13").ToList();
            Assert.AreEqual(units.Count(), 1);
            Assert.AreEqual(13, units[0].Value);
        }

        [TestMethod]
        public void TestComplex()
        {
            var units = DefinitionsExtensions.ParseFormat("Auto,Auto,*").ToList();
            Assert.AreEqual(units.Count(), 3);
            Assert.AreEqual(GridUnitType.Auto, units[0].GridUnitType);
            Assert.AreEqual(GridUnitType.Auto, units[1].GridUnitType);
            Assert.AreEqual(GridUnitType.Star, units[2].GridUnitType);
        }

        [TestMethod]
        public void TestComplex2()
        {
            var units = DefinitionsExtensions.ParseFormat("2*,3*,*").ToList();
            Assert.AreEqual(units.Count(), 3);
            Assert.AreEqual(GridUnitType.Star, units[0].GridUnitType);
            Assert.AreEqual(GridUnitType.Star, units[1].GridUnitType);
            Assert.AreEqual(GridUnitType.Star, units[2].GridUnitType);
            Assert.AreEqual(2, units[0].Value);
            Assert.AreEqual(3, units[1].Value);
            Assert.AreEqual(1, units[2].Value);
        }
    }
}
