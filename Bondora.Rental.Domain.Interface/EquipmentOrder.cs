using NServiceBus;
using System;
using System.Collections.Generic;

namespace Bondora.Rental.Domain.Interface
{
    public class EquipmentOrder
    {
        public string Type { get; set; }
        public int RentalDays { get; set; }

        public EquipmentOrder() { }

        public EquipmentOrder(string type, int days)
        {
            Type = type;
            RentalDays = days;
        }


        private static readonly Dictionary<string, EquipmentType> EquipmentTypeIndex =
            new Dictionary<string, EquipmentType>
            {
                { "Regular", new RegularEquipment() },
                { "Heavy", new HeavyEquipment() },
                { "Specialized", new SpecializedEquipment() },
            };

        public static EquipmentType ParseEquipmentType(string equipmentType)
        {
            if (EquipmentTypeIndex.ContainsKey(equipmentType))
                return EquipmentTypeIndex[equipmentType];
            throw new Exception("Unknown equipment type: " + equipmentType);
        }


        public Price<TCurrency> CalculatePrice<TCurrency>(RentalFees<TCurrency> fees) where TCurrency : Currency =>
            ParseEquipmentType(Type).CalculatePrice(fees, RentalDays);

        public int CalculateLoyaltyPoints() => 
            ParseEquipmentType(Type).LoyaltyPoints;
    }

    public class Order : IMessage
    {
        public List<EquipmentOrder> Equipment { get; set; } 
    }
}
