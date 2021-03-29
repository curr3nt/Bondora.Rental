namespace Bondora.Rental.Web.Models
{
    public class Equipment
    {
        public readonly string Name;
        public readonly string Type;

        public Equipment(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
