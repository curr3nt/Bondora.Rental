using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bondora.Rental.Web.Models
{
    public class EquipmentCollection : IEnumerable<Equipment>, WithSpec
    {
        private readonly IEnumerable<Equipment> _collection;

        public IEnumerator<Equipment> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ModelSpec Spec { get; }

        public EquipmentCollection(IEnumerable<Equipment> collection, ModelSpec spec)
        {
            _collection = collection;
            Spec = spec;
        }

        public static EquipmentCollection FromSampleFile(ModelSpec spec)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "Models", "SampleEquipmentCollection.csv");
            var lines = File.ReadAllLines(path);
            var collection = lines
                .Select(l => l.Split(','))
                .Select(strs => new Equipment(strs[0], strs[1]))
                .ToList();
            return new EquipmentCollection(collection, spec);
        }
    }
}
