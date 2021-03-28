using Bondora.Rental.Domain;
using System;
using Xunit;

namespace Bondora.Rental.Tests
{
    public class TestEquipmentType
    {
        private class DummyCurrency : Currency
        {
            public string Print(int value) => throw new NotImplementedException();
            public static Price<DummyCurrency> CreatePrice(int value) => new Price<DummyCurrency>(value, new DummyCurrency());
            public override bool Equals(object obj) => obj is DummyCurrency;
        }

        [Theory]
        [InlineData(10, 1, 2, 1, 11)]
        [InlineData(10, 1, 2, 2, 12)]
        [InlineData(100, 2, 3, 3, 106)]
        public void TestHeavyEquipment(int oneTime, int premiumDaily, int regularDaily, int days, int expected)
        {
            var heavy = new HeavyEquipment();
            Assert.Equal(2, heavy.LoyaltyPoints);

            var prices = new RentalFees<DummyCurrency>(
                DummyCurrency.CreatePrice(oneTime),
                DummyCurrency.CreatePrice(premiumDaily),
                DummyCurrency.CreatePrice(regularDaily));
            var expectedPrice = DummyCurrency.CreatePrice(expected);
            Assert.Equal(expectedPrice, heavy.CalculatePrice(prices, days));
        }

        [Theory]
        [InlineData(10, 1, 2, 1, 11)]
        [InlineData(10, 1, 2, 2, 12)]
        [InlineData(10, 1, 2, 3, 14)]
        [InlineData(100, 3, 2, 4, 110)]
        public void TestRegularEquipment(int oneTime, int premiumDaily, int regularDaily, int days, int expected)
        {
            var regular = new RegularEquipment();
            Assert.Equal(1, regular.LoyaltyPoints);

            var prices = new RentalFees<DummyCurrency>(
                DummyCurrency.CreatePrice(oneTime),
                DummyCurrency.CreatePrice(premiumDaily),
                DummyCurrency.CreatePrice(regularDaily));
            var expectedPrice = DummyCurrency.CreatePrice(expected);
            Assert.Equal(expectedPrice, regular.CalculatePrice(prices, days));
        }

        [Theory]
        [InlineData(10, 1, 2, 1, 1)]
        [InlineData(10, 1, 2, 2, 2)]
        [InlineData(10, 1, 2, 3, 3)]
        [InlineData(10, 1, 2, 4, 5)]
        [InlineData(10, 1, 2, 5, 7)]
        public void TestSpecializedEquipment(int oneTime, int premiumDaily, int regularDaily, int days, int expected)
        {
            var specialized = new SpecializedEquipment();
            Assert.Equal(1, specialized.LoyaltyPoints);

            var prices = new RentalFees<DummyCurrency>(
                DummyCurrency.CreatePrice(oneTime),
                DummyCurrency.CreatePrice(premiumDaily),
                DummyCurrency.CreatePrice(regularDaily));
            var expectedPrice = DummyCurrency.CreatePrice(expected);
            Assert.Equal(expectedPrice, specialized.CalculatePrice(prices, days));
        }
    }
}
