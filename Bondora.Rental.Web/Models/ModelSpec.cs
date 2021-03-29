using System.Collections.Generic;

namespace Bondora.Rental.Web.Models
{
    public interface ModelSpec
    {
        string GetLocalizedString(string key);
    }

    public interface WithSpec
    {
        ModelSpec Spec { get; }
    }

    public class EnglishDictionary : ModelSpec
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>
        {
            { "Home", "Home" },
            { "Available equipment", "Available equipment" },
            { "Cart", "Cart" },
            { "welcome",
@"<h1>Welcome to <strong>Bondora.Rental</strong></h1>
<p>Here you can rent some <a href='/Equipment/Index'>construction equipment</a>.
Feel free to browse the catalogue and order what you need.
When finished, proceed to <a href='/Cart/Index'>virtual cart</a> to confirm your order.
Upon confirmation, you will get an invoice with prices for all equipment you have chosen.
As a bonus, you will also get customer loyalty points depending on amount of ordered equipment.</p>" },
            { "available equipment description",
@"<h1>Available equipment</h1>
<p>Here you can choose equipment you wish to order.
Before confirming your choice of an item, please specify amount of days you will be using this particular equipment.
After that, you should click ""Add to cart"". 
This confirms your selection and includes the item into your <a href='/Cart/Index'>virtual cart</a>.
Repeat the process until all items that you need are inside the cart, and then proceed to Cart.</p>"},
            { "Name", "Name" },
            { "Type", "Type" },
            { "Rental days", "Rental days" },
            { "Add to cart", "Add to cart" },
            { "added to cart", "Added to cart item" },
            { "empty cart", 
@"<p>Your cart is empty.
Fill it up by browsing our <a href='/Equipment/Index'>construction equipment</a>.</p>" },
            { "full cart",
@"<p>Here is the equipment you have chosen.
When ready, confirm your order by clicking ""Confirm"" link.
We will generate invoice with prices for all equipment, as well as some customer loyalty points.</p>" },
            { "confirm cart", "Confirm" },
            { "Clear cart", "Clear cart" },
        };

        public string GetLocalizedString(string key) =>
            _dictionary.ContainsKey(key) ? _dictionary[key] : key;
    }
}
