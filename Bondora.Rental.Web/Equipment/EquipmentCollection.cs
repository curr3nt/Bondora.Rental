using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bondora.Rental.Web.Equipment
{
    public class EquipmentCollection : IEnumerable<Equipment>
    {
        private readonly IEnumerable<Equipment> _collection;

        public EquipmentCollection(IEnumerable<Equipment> collection)
        {
            _collection = collection;
        }

        public IEnumerator<Equipment> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static EquipmentCollection FromSampleFile()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Equipment", "SampleEquipmentCollection.csv");
            var lines = File.ReadAllLines(path);
            var collection = lines
                .Select(l => l.Split(','))
                .Select(strs => new Equipment(strs[0], strs[1]))
                .ToList();
            return new EquipmentCollection(collection);
        }
    }
}
