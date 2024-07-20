using Microsoft.VisualStudio.TestTools.UnitTesting;
using sumilation_prigram;
using System.Collections.Generic;

namespace TestProject1
{
    [TestClass]
    public class OzoneTest
    {
        private const double Tolerance = 1e-9;

        [TestMethod]
        public void OzoneTransformationTest()
        {
            Ozone ozone = new Ozone("Ozone", 10);
            ozone.Transform(Sunshine.Instance);
            Assert.AreEqual(10, ozone.GetThickness());

            ozone.Transform(Thunderstorm.Instance);
            Assert.AreEqual(10, ozone.GetThickness());

            Gases transformedGas = ozone.Transform(Other.Instance);
            Assert.IsInstanceOfType(transformedGas, typeof(Oxygen));
            Assert.AreEqual(0.5, transformedGas.GetThickness(), Tolerance);
        }
    }

    [TestClass]
    public class OxygenTest
    {
        private const double Tolerance = 1e-9;

        [TestMethod]
        public void OxygenTransformationTest()
        {
            Oxygen oxygen = new Oxygen("Oxygen", 10);
            Gases transformedGas = oxygen.Transform(Sunshine.Instance);
            Assert.IsInstanceOfType(transformedGas, typeof(Ozone));
            Assert.AreEqual(0.5, transformedGas.GetThickness(), Tolerance);

            transformedGas = oxygen.Transform(Thunderstorm.Instance);
            Assert.IsInstanceOfType(transformedGas, typeof(Ozone));
            Assert.AreEqual(4.75, transformedGas.GetThickness(), Tolerance);

            transformedGas = oxygen.Transform(Other.Instance);
            Assert.IsInstanceOfType(transformedGas, typeof(CarbonDioxide));
            Assert.AreEqual(0.475, transformedGas.GetThickness(), Tolerance);
        }
    }

    [TestClass]
    public class CarbonTest
    {
        private const double Tolerance = 1e-9;

        [TestMethod]
        public void CarbonDioxideTransformationTest()
        {
            CarbonDioxide carbonDioxide = new CarbonDioxide("Carbon", 10);
            carbonDioxide.Transform(Sunshine.Instance);
            Assert.AreEqual(9.5, carbonDioxide.GetThickness(), Tolerance);

            carbonDioxide.Transform(Thunderstorm.Instance);
            Assert.AreEqual(9.5, carbonDioxide.GetThickness(), Tolerance);

            Gases transformedGas = carbonDioxide.Transform(Other.Instance);
            Assert.IsInstanceOfType(transformedGas, typeof(CarbonDioxide));
            Assert.AreEqual(9.5, transformedGas.GetThickness(), Tolerance);
        }
    }

    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void ProgramFunctionalityTest()
        {
            List<Gases> gases = new List<Gases>
            {
                new Ozone("Ozone", 1),
                new Oxygen("Oxygen", 2),
                new CarbonDioxide("Carbon", 3)
            };

            string weatherPattern = "OST";
            int weatherCounter = 0;
            bool allAlive = true;

            while (allAlive)
            {
                foreach (Gases gas in gases)
                {
                    if (weatherCounter >= weatherPattern.Length)
                    {
                        weatherCounter = 0;
                    }

                    char condition = weatherPattern[weatherCounter++];
                    switch (condition)
                    {
                        case 'O':
                            gas.Transform(Sunshine.Instance);
                            break;
                        case 'S':
                            gas.Transform(Thunderstorm.Instance);
                            break;
                        case 'T':
                            gas.Transform(Other.Instance);
                            break;
                    }

                    if (gas.GetThickness() <= 0.5)
                    {
                        allAlive = false;
                    }
                }
            }

            Assert.IsFalse(allAlive);
        }
    }
}
