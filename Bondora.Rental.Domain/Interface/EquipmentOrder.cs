using System;
using System.Collections.Generic;

namespace Bondora.Rental.Domain.Interface
{
    public class EquipmentOrder
    {
        public readonly string Type;
        public readonly int RentalDays;

        public EquipmentOrder(string type, int days)
        {
            Type = type;
            RentalDays = days;
        }


        private static readonly Dictionary<string, EquipmentType> EquipmentTypeIndex =
            new Dictionary<string, EquipmentType>
            {
                { "Regular", new RegularEquipment() },
                { "Heavy", new HeavyEquiment() },
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
}
