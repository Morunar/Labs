using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System;
using Player;

public class PlayerEquipmentController
{
    private PlayerCreature _player;

    private Dictionary<EquipmentSlotType, Equipment> _currentEquipment = new Dictionary<EquipmentSlotType, Equipment>();


    public PlayerEquipmentController(PlayerCreature playerCreature)
    {
        _player = playerCreature;
        _player.DestroyHandler += OnDestroy;
        foreach (EquipmentSlotType type in Enum.GetValues(typeof(EquipmentSlotType)))
        {
            if (type == EquipmentSlotType.None)
                continue;
            _currentEquipment.Add(type, null); //to-do from save
        }
        foreach (EquipmentSlot slot in _player.PlayerEquipmentUI.EquipmentSlots)
        {
            slot.PlayerCreature = _player;
            slot.LeftPointerClicked += _player.PlayerInventory.OnMoveStarted;
        }
    }

    public void EquipItem(Equipment equipment)
    {
        if (equipment == null)
            return;
        EquipmentSlotType slotType = GetSlotForItem(equipment.EquipmentBase.EquipmentType);

        EquipItem(slotType, equipment);
    }

    public void EquipItem(EquipmentSlotType slotType, Equipment equipment, bool removeToInventory = true)
    {
        if (slotType == EquipmentSlotType.None || equipment == null)
            return;

        TryToRemoveEquipment(slotType, removeToInventory);
        _currentEquipment[slotType] = equipment;
        EquipmentSlot slot = _player.PlayerEquipmentUI.GetEquipmentSlotByType(slotType);
        slot.RightPointerClicked += RemoveItem;
        slot.AddItemToSlot(equipment);
    }

    public void TryToRemoveEquipment(EquipmentSlotType slotType, bool toInventory)
    {
        Equipment curretnEquipment = _currentEquipment[slotType];
        if (curretnEquipment == null)
            return;

        if (toInventory)
            _player.PlayerInventory.AddItemToInventory(curretnEquipment);
        EquipmentSlot slot = _player.PlayerEquipmentUI.GetEquipmentSlotByType(slotType);
        slot.RemoveItem();

        _currentEquipment[slotType] = null;
    }

    private void RemoveItem(ItemSlot slot)
    {
        slot.RightPointerClicked -= RemoveItem;
        TryToRemoveEquipment((slot as EquipmentSlot).EquipmentSlotType, true);
    }

    private EquipmentSlotType GetSlotForItem(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Weapon:
                return EquipmentSlotType.ItemRight;
            case EquipmentType.Shield:
                return EquipmentSlotType.ItemLeft;
            case EquipmentType.Chest:
                return EquipmentSlotType.Chest;
            default:
                Debug.LogError("Requested type of " + type + " is not supported yet");
                return EquipmentSlotType.None;
        }
    }

    private void OnDestroy()
    {
        _player.DestroyHandler -= OnDestroy;
        foreach (EquipmentSlot slot in _player.PlayerEquipmentUI.EquipmentSlots)
        {
            slot.LeftPointerClicked -= _player.PlayerInventory.OnMoveStarted;
        }
    }
}
