using Bondora.Rental.Domain;
using System;
using Xunit;

namespace Bondora.Rental.Tests
{
    public class TestRentalFees
    {
        private class DummyCurrency : Currency
        {
            public string Print(int value) => throw new NotImplementedException();
        }

        [Fact]
        public void TestConstructor()
        {
            var oneTime = new Price<DummyCurrency>(1, new DummyCurrency());
            var premiumDaily = new Price<DummyCurrency>(2, new DummyCurrency());
            var regularDaily = new Price<DummyCurrency>(3, new DummyCurrency());
            var prices = new RentalFees<DummyCurrency>(oneTime, premiumDaily, regularDaily);
            // if properties would have its own price type (class), these asserts would become irrelevant,
            // as compiler would ensure incorrect price type won't be assigned to a property.
            Assert.Equal(oneTime, prices.OneTime);
            Assert.Equal(premiumDaily, prices.PremiumDaily);
            Assert.Equal(regularDaily, prices.RegularDaily);
        }
    }
}
