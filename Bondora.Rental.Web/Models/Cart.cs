using Bondora.Rental.Web.Equipment;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bondora.Rental.Web.Models
{
    public class CartFullItem
    {
        public readonly string Name;
        public readonly string Type;
        public readonly int RentalDays;

        public CartFullItem(string name, string type, int rentalDays)
        {
            Name = name;
            Type = type;
            RentalDays = rentalDays;
        }

        // DANGER!!! This method returns "null" when it cannot correctly restore a cart item. 
        // This should be fixed in a real environment.
        // Unfortunately, C# does not have a built-in implementation for option types: 
        //  https://en.wikipedia.org/wiki/Option_type
        public static CartFullItem Restore(CartCompactItem item, 
            Dictionary<string, string> equipmentTypeIndex)
        {
            int days;
            var isParsed = int.TryParse(item.Days, out days);
            if (!isParsed) return null;
            if (!equipmentTypeIndex.ContainsKey(item.Name)) return null;

            return new CartFullItem(item.Name, equipmentTypeIndex[item.Name], days);
        }
    }

    public class Cart : IEnumerable<CartFullItem>
    {
        private readonly IEnumerable<CartFullItem> _items;

        public Cart(IEnumerable<CartFullItem> items)
        {
            _items = items;
        }

        public static Cart Validate(IEnumerable<CartCompactItem> compactItems,
            EquipmentCollection equipment)
        {
            var equipmentTypeIndex = equipment.ToDictionary(e => e.Name, e => e.Type);
            return new Cart(compactItems
                .Select(i => CartFullItem.Restore(i, equipmentTypeIndex))
                // this filters out only properly restored items
                // please check comment above
                .Where(item => item != null)
                .ToList());
        }

        public IEnumerator<CartFullItem> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
