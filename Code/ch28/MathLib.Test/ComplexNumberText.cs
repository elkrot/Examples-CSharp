using System;
using NUnit.Framework;//add a reference to nunit.framework.dll

namespace MathLib.Test
{
    [TestFixture]
    public class ComplexNumberText
    {
        //setup/teardown for entire fixture
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
        }

        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
        }

        //setup/teardown for every test in the fixture
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [TestCase]
        public void MultiplyTest_RealsOnly()
        {
            ComplexNumber a = new ComplexNumber(2,0);
            ComplexNumber b = new ComplexNumber(3,0);
            ComplexNumber c = a * b;
            Assert.That(c.RealPart, Is.EqualTo(6.0));
            Assert.That(c.ImaginaryPart, Is.EqualTo(0.0));
        }

        [TestCase]
        public void MultiplyTest_RealAndImaginary()
        {
            ComplexNumber a = new ComplexNumber(2, 4);
            ComplexNumber b = new ComplexNumber(3, 5);
            ComplexNumber c = a * b;
            Assert.That(c.RealPart, Is.EqualTo(-14.0));
            Assert.That(c.ImaginaryPart, Is.EqualTo(22.0));
        }
    }
}
