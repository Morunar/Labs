using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{   
    public abstract class Item
    {
        private ItemBase _itemBase;
        protected PlayerCreature _owner;
        public ItemId ItemId => _itemBase.ItemId;
        public int CurrentCost => _itemBase.Cost;
        public Sprite InventoryIcon => _itemBase.InventoryIcon;

        public int CurrentStackCount { get; set; }

        public Item(ItemBase itemBase)
        {
            _itemBase = itemBase;
        }

        public abstract bool Use();
        public abstract bool CanBeUsed();

        public void SetOwner(PlayerCreature player)
        {
            _owner = player;
        }

        public void ReleaseItem()
        {
            _owner = null;
        }

    }

    public class Readable : Item
    {
        public ReadableBase ReadableBase { get; private set; }


        public Readable(ReadableBase itemBase) : base(itemBase)
        {
            ReadableBase = itemBase;
        }

        public override bool Use()
        {
            Debug.Log("Reading text");
            return true;
        }
        public override bool CanBeUsed()
        {
            return true;
        }

    }

    public class Potion : Item
    {
        public PotionBase PotionBase { get; private set; }
        public int RestorationAmount => PotionBase.PotionLvl * 250;

        public Potion(PotionBase itemBase) : base(itemBase)
        {
            PotionBase = itemBase;
        }

        public override bool Use()
        {
            CurrentStackCount -= 1;
            Debug.Log("Restoreed " + RestorationAmount + "stat");
            if (CurrentStackCount <= 0)
            {
                return true;
            }
            return false;
        }
        public override bool CanBeUsed()
        {
            return true;
        }

    }

    public class Equipment : Item
    {
        public EquipmentBase EquipmentBase { get; private set; }

        public Equipment(EquipmentBase itemBase) : base(itemBase)
        {
            EquipmentBase = itemBase;
        }

        public override bool Use()
        {
            if (CanBeUsed())
            {
                _owner.PlayerEquipmentController.EquipItem(this);
                return true;
            }

            return false;
        }
        public override bool CanBeUsed()
        {
            return true;
        }

    }

    public class EquipmentComponent : Item
    {
    public EquipmentComponentBase EquipmentComponentBase { get; private set; }

    public EquipmentComponent(EquipmentComponentBase itemBase) : base(itemBase)
    {
        EquipmentComponentBase = itemBase;
    }

    public override bool Use()
    {
            if (!CanBeUsed())
            {
                return false;
            }

            CurrentStackCount -= 1;
            Debug.Log("Start adding component to equipment");
            if (CurrentStackCount <= 0)
            {
                return true;
            }

            return false;
    }
        public override bool CanBeUsed()
        {
            return true;
        }

    }


}