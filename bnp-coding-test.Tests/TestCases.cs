using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bnp_coding_test.Tests
{
    [TestClass]
    public class TestCases
    {
        [TestMethod]
        public void Tenor0D()
        {
            var classUnderTest = new PortfolioCurve(10, "0D", 100);
            var calculatedDays = 0;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void Tenor1D()
        {
            var classUnderTest = new PortfolioCurve(10, "1D", 100);
            var calculatedDays = 1;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void Tenor3w()
        {
            var classUnderTest = new PortfolioCurve(10, "3w", 100);
            var calculatedDays = 3 * 7;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void Tenor6M()
        {
            var classUnderTest = new PortfolioCurve(10, "6M", 100);
            var calculatedDays = 6 * 30;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void Tenor5Y()
        {
            var classUnderTest = new PortfolioCurve(10, "5Y", 100);
            var calculatedDays = 5 * 365;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void Tenor2D3W()
        {
            var classUnderTest = new PortfolioCurve(10, "2d3w", 100);
            var calculatedDays = 2 + (3 * 7);

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void Tenor3W2D()
        {
            var classUnderTest = new PortfolioCurve(10, "3w2d", 100);
            var calculatedDays = (3 * 7) + 2;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void AllTenorUnits()
        {
            var classUnderTest = new PortfolioCurve(10, "1y6m1w2d", 100);
            var calculatedDays = (1 * 365) + (6 * 30) + (1 * 7) + 2;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void AllTenorUnitsRandomOrder()
        {
            var classUnderTest = new PortfolioCurve(10, "1w6m2d1y", 100);
            var calculatedDays = (1 * 365) + (6 * 30) + (1 * 7) + 2;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void AllTenorUnitsWithZeroes()
        {
            var classUnderTest = new PortfolioCurve(10, "1y6m0w2d", 100);
            var calculatedDays = (1 * 365) + (6 * 30) + (0 * 7) + 2;

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void InvalidTenorDuplicateUnit()
        {
            var classUnderTest = new PortfolioCurve(10, "1d1d", 100);
            var calculatedDays = -1; // invalid tenor

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void InvalidTenorMissingTenorValue()
        {
            var classUnderTest = new PortfolioCurve(10, "1dw", 100);
            var calculatedDays = -1; // invalid tenor

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void InvalidTenorWrongOrder()
        {
            var classUnderTest = new PortfolioCurve(10, "d2", 100);
            var calculatedDays = -1; // invalid tenor

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void InvalidTenorGarbageInput()
        {
            var classUnderTest = new PortfolioCurve(10, "something", 100);
            var calculatedDays = -1; // invalid tenor

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }

        [TestMethod]
        public void InvalidTenorNegativeValues()
        {
            var classUnderTest = new PortfolioCurve(10, "-4d", 100);
            var calculatedDays = -1; // invalid tenor

            var tenorDays = classUnderTest.TenorInDays;

            Assert.AreEqual(tenorDays, calculatedDays);
        }
    }
}
