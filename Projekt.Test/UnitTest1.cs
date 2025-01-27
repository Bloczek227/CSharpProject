

namespace Projekt.Test
{
    public class Tests
    {
        Stats stats;

        [TestCase(100, 200, false)]
        [TestCase(100, 80, true)]
        public void ClickBuyReturn(double tickMoney, double cost, bool expectedResult)
        {

            Account acc = new(stats) { TickMoney = tickMoney };


            ClickUpgrade clickUpgrade = new("", acc, cost, 1.0);
            var result = clickUpgrade.Buy();

            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [TestCase(100, 200, 100)]
        [TestCase(100, 80, 20)]
        public void ClickBuyMoneySpending(double tickMoney, double cost, double expectedResult)
        {

            Account acc = new(stats) { TickMoney = tickMoney };


            ClickUpgrade clickUpgrade = new("", acc, cost, 1.0);
            clickUpgrade.Buy();
            double result=acc.TickMoney;

            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [TestCase(100, 200, false)]
        [TestCase(100, 80, true)]
        public void TickBuyReturn(double clickMoney, double cost, bool expectedResult)
        {

            Account acc = new(stats) { ClickMoney = clickMoney };


            TickUpgrade tickUpgrade = new("", acc, cost, 1.0);
            var result = tickUpgrade.Buy();

            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [TestCase(100, 200, 100)]
        [TestCase(100, 80, 20)]
        public void TickBuyMoneySpending(double clickMoney, double cost, double expectedResult)
        {
            Account acc = new(stats) { ClickMoney = clickMoney };


            TickUpgrade tickUpgrade = new("", acc, cost, 1.0);
            tickUpgrade.Buy();
            double result = acc.ClickMoney;

            Assert.That(result, Is.EqualTo(expectedResult));
        }


        [TestCase(100, 200, 0)]
        [TestCase(100, 80, 1)]
        public void TickBuyCountChange(double clickMoney, double cost, double expectedResult)
        {

            Account acc = new(stats) { ClickMoney = clickMoney };


            TickUpgrade tickUpgrade = new("", acc, cost, 1.0) { BoughtCount=0,Count=0};
            tickUpgrade.Buy();
            double result = tickUpgrade.Count;
            double result2 = tickUpgrade.BoughtCount;

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(result2, Is.EqualTo(expectedResult));
        }

        [TestCase(100, 200, 0)]
        [TestCase(100, 80, 1)]
        public void ClickBuyCountChange(double tickMoney, double cost, double expectedResult)
        {

            Account acc = new(stats) { TickMoney = tickMoney };


            ClickUpgrade clickUpgrade = new("", acc, cost, 1.0) { BoughtCount = 0, Count = 0 };
            clickUpgrade.Buy();
            double result = clickUpgrade.Count;
            double result2 = clickUpgrade.BoughtCount;

            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(result2, Is.EqualTo(expectedResult));
        }
    }
}
