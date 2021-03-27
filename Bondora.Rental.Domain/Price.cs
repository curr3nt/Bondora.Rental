using System.Collections.Generic;
using System.Linq;

namespace Bondora.Rental.Domain
{
    public class Price<TCurrency> where TCurrency : Currency
    {
        public int Value { get; }
        public TCurrency Currency { get; }

        public Price(int value, TCurrency currency)
        {
            Value = value;
            Currency = currency;
        }

        public Price<TCurrency> Add(Price<TCurrency> b)
        {
            return new Price<TCurrency>(Value + b.Value, Currency);
        }

        public Price<TCurrency> Multiply(int multiplier)
        {
            return new Price<TCurrency>(Value * multiplier, Currency);
        }

        public static Price<TCurrency> operator +(Price<TCurrency> a, Price<TCurrency> b) => a.Add(b);
        public static Price<TCurrency> operator *(Price<TCurrency> a, int multiplier) => a.Multiply(multiplier);
        public static Price<TCurrency> operator *(int multiplier, Price<TCurrency> a) => a.Multiply(multiplier);

        public string Print()
        {
            return Currency.Print(Value);
        }


        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj)
        {
            var p = obj as Price<TCurrency>;
            return this == obj
                || p != null && p.Value.Equals(Value) && p.Currency.Equals(Currency);
        }
    }
}
