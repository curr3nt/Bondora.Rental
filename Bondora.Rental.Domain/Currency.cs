namespace Bondora.Rental.Domain
{
    public interface Currency
    {
        string Print(int value);
    }

    public class Euro : Currency
    {
        public Euro() { }

        public string Print(int value)
        {
            return value + "€";
        }

        public static Price<Euro> CreatePrice(int value)
        {
            return new Price<Euro>(value, new Euro());
        }
    }
}
