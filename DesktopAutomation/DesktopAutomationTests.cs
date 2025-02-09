using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium.Service;

namespace DesktopAutomation
{
    [TestFixture]
    public class CalculatorTests
    {
        private WindowsBase _windowsBase;
        private static WindowsDriver driver;


        [SetUp]
        public void Setup()
        {
            _windowsBase = new WindowsBase();
            driver = _windowsBase.StartAppium();

        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
            _windowsBase?.StopServices();
        }


        [Test]
        public void AdditionTest()
        {
            // Find the buttons and perform addition operation (5 + 3)
            driver.FindElement(MobileBy.Name("Five")).Click();
            driver.FindElement(MobileBy.Name("Plus")).Click();
            driver.FindElement(MobileBy.Name("Three")).Click();
            driver.FindElement(MobileBy.Name("Equals")).Click();

            // Verify that the result is 8
            var result = driver.FindElement(MobileBy.AccessibilityId("CalculatorResults")).Text;
            Assert.That(result.Trim(), Is.EqualTo("Display is 8"));
        }

        [Test]
        public void SubtractionTest()
        {
            // Perform subtraction operation (9 - 4)
            driver.FindElement(MobileBy.Name("Nine")).Click();
            driver.FindElement(MobileBy.Name("Minus")).Click();
            driver.FindElement(MobileBy.Name("Four")).Click();
            driver.FindElement(MobileBy.Name("Equals")).Click();

            // Verify that the result is 5
            var result = driver.FindElement(MobileBy.AccessibilityId("CalculatorResults")).Text;
            Assert.That(result.Trim(), Is.EqualTo("Display is 5"));
        }


    }
}