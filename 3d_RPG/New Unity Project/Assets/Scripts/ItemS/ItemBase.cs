using System;
using UnityEngine;
using Player;
namespace Items
{
    public abstract class ItemBase : ScriptableObject
{
    [SerializeField] private ItemId _itemId;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private int _stackCount;
    [SerializeField] private Sprite _inventoryIcon;

    public ItemId ItemId => _itemId;
    public string Name => _name;
    public string Description => _description;
    public int StackCount => _stackCount;
    public int Cost => _cost;
    public Sprite InventoryIcon => _inventoryIcon;
}

    [CreateAssetMenu(fileName = "Consunmable", menuName = "Item/Consunmables")]
    public class ConsunmableBase : ItemBase
    {

    }
    [CreateAssetMenu(fileName = "Readable", menuName = "Item/Readables")]
    public class ReadableBase : ItemBase
    {
        [SerializeField] private string _text;
        public string Text => _text;
    }

    [CreateAssetMenu(fileName = "Potion", menuName = "Item/ItemPotions")]
    public class PotionBase : ItemBase
    {
        [SerializeField] private int _potionLvl;

        public int PotionLvl => _potionLvl;
    }

    public abstract class StatItemBase: ItemBase
    {
        [SerializeField] private int _requiredLvl;
        [SerializeField] private ItemStat[] _primaryStats;


        public int RequiredLvl => _requiredLvl;
        public ItemStat[] PrimaryStat => _primaryStats;
    }

    [CreateAssetMenu(fileName = "EquipmentComponent", menuName = "Item/EquipmentComponents")]
    public class EquipmentComponentBase : StatItemBase
    {
        [SerializeField] private EquipmentType[] _allowedEquipmentTypes;
        [SerializeField] private ComponentType _componentType;

        public EquipmentType[] AllowedEquipmentTypes => _allowedEquipmentTypes;
        public ComponentType ComponentType => _componentType;
    }
    [CreateAssetMenu(fileName = "EquipmentBase", menuName = "Item/EquipmentBase")]
    public class EquipmentBase : StatItemBase
    {
        [SerializeField] private Stat[] _requiredStats;
        [SerializeField] private EquipmentType _equipment;
        [SerializeField] private RarityLvl _rarityLvl;
        [SerializeField] private ItemStat[] _additionalStats;

        public Stat[] RequiredStats => _requiredStats;
        public EquipmentType EquipmentType => _equipment;
        public RarityLvl RarityLvl => _rarityLvl;
        public ItemStat[] AdditionalStats => _additionalStats;


    }
    
    

}


