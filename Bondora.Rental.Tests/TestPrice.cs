using Bondora.Rental.Domain;
using System;
using Xunit;

namespace Bondora.Rental.Tests
{
    public class TestPrice
    {
        private class DummyCurrency : Currency
        {
            public bool PrintCalled { get; private set; }
            public string Print(int value)
            {
                PrintCalled = true;
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void TestConstructor()
        {
            var value = 1;
            var currency = new DummyCurrency();
            var price = new Price<DummyCurrency>(value, currency);

            Assert.Equal(value, price.Value);
            Assert.Equal(currency, price.Currency);
        }

        [Fact]
        public void TestAdd()
        {
            var currency = new DummyCurrency();
            var a = new Price<DummyCurrency>(1, currency);
            var b = new Price<DummyCurrency>(2, currency);

            Assert.Equal(3, a.Add(b).Value);
            Assert.Equal(currency, a.Add(b).Currency);
            Assert.Equal(3, b.Add(a).Value);
            Assert.Equal(3, (a + b).Value);
            Assert.Equal(3, (b + a).Value);
        }

        [Fact]
        public void TestMultiply()
        {
            var price = new Price<DummyCurrency>(2, new DummyCurrency());

            Assert.Equal(4, price.Multiply(2).Value);
            Assert.Equal(6, (price * 3).Value);
        }

        [Fact]
        public void TestPrint()
        {
            var price = new Price<DummyCurrency>(1, new DummyCurrency());

            Assert.ThrowsAny<Exception>(() => price.Print());
            Assert.True(price.Currency.PrintCalled);
        }
    }
}
