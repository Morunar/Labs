using System;
using Player;
using UnityEngine;
namespace Items
{

    public static class ItemHelper
    {
        public static bool CanBeEquiped(EquipmentType type, EquipmentSlotType slotType)
        {
            switch (type)
            {
                case EquipmentType.Weapon:
                    return slotType == EquipmentSlotType.ItemRight;
                case EquipmentType.Shield:
                    return slotType == EquipmentSlotType.ItemLeft;
                default:
                    Debug.Log("Not suported type");
                    return false;
            }
        }
    }

    [Serializable]
    public class ItemStat : Stat
    {
        IncreaserType IncreaserType;
    }

    public enum ItemId
    {
        Gold = 1,
        Scrap = 2,
        Shard = 3,
        HP_Potion = 4,
        MP_Potion = 5,
        Emerald = 6,
        SoldierSword = 7,
        Axe = 8,
    }

    public enum IncreaserType
    {
        Value,
        Percent,
    }

    public enum EquipmentType
    {
        Weapon,
        Shield,
        Helmet,
        Chest,
        Shoulders,
        Gloves,
        Leggins,
        Boots,
        Belt,
        Ring,
        Medal,
        Amulet,
        Rune,
    }
    public enum RarityLvl
    {
        Common = 0,
        Uncommon = 1,
        Magical = 2,
        Rare = 3,
        Epic = 4,
        Legendary = 5,
    }

    public enum ComponentType
    {
        QuaterComponent,
        HalfComponent,
        HoleComponent,
    }



}