using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.EventSystems;

public class PlayerInventoryController
{
    private PlayerCreature _player;
    private Item _movingItem;
    private ItemSlot _lastClickedSlot;
    private ItemSlot _newClickedSlot;
    private List<ItemSlot> _inventoryItems = new List<ItemSlot>();
    private int _inventoryCapacity;
    private float _clickTime;
    public Item MovingItem => _movingItem;

    public PlayerInventoryController(PlayerCreature player)
    {
        _player = player;
        _player.DestroyHandler += OnDestroy;
        _player.ServiceManager.UpdateHandler += OnUpdate;

        _inventoryCapacity = _player.PlayerInventoryUI.BaseInventorySlots.Length;
        for (int i = 0; i < _inventoryCapacity; i++)
        {
            _player.PlayerInventoryUI.BaseInventorySlots[i].LeftPointerClicked += OnMoveStarted;
            _player.PlayerInventoryUI.BaseInventorySlots[i].PlayerCreature = _player;
        }
    }

    public bool AddItemToInventory(Item item)
    {
        if (_inventoryItems.Count < _inventoryCapacity)
        {
            ItemSlot slot = _player.PlayerInventoryUI.GetFreeSlot();
            if (slot == null)
                return false;
            slot.AddItemToSlot(item);
            slot.RightPointerClicked += OnItemUsed;
            _inventoryItems.Add(slot);
            return true;
        }
        else
            return false;
    }

    public void OnItemUsed(ItemSlot slot)
    {
        if (slot.SlotItem.Use())
        {
            slot.RightPointerClicked -= OnItemUsed;
            slot.RemoveItem();
        }
    }

    private void OnUpdate()
    {
        if (_movingItem != null)
        {
            _player.PlayerInventoryUI.MovingImage.transform.position = Input.mousePosition;
            if (Time.time - _clickTime > 0.02f)
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                    EndMove(true);
                else if (Input.GetKeyUp(KeyCode.Mouse1))
                    EndMove(false);
            }
        }
    }

    private void OnDestroy()
    {
        _player.DestroyHandler -= OnDestroy;
        _player.ServiceManager.UpdateHandler -= OnUpdate;
        for (int i = 0; i < _inventoryCapacity; i++)
        {
            _player.PlayerInventoryUI.BaseInventorySlots[i].LeftPointerClicked -= OnMoveStarted;
        }
    }

    public void OnMoveStarted(ItemSlot slot)
    {
        EquipmentSlot equipmentSlot = slot as EquipmentSlot;
        if (equipmentSlot == null && slot.SlotItem != null)
            slot.RightPointerClicked -= OnItemUsed;

        if (_lastClickedSlot != null)
        {
            _newClickedSlot = slot;
            return;
        }
        if (slot.SlotItem != null)
        {
            _clickTime = Time.time;
            _lastClickedSlot = slot;
            SetNewMovingItem(slot.SlotItem);

            if (equipmentSlot != null)
                _player.PlayerEquipmentController.TryToRemoveEquipment(equipmentSlot.EquipmentSlotType, false);
            else
                slot.RemoveItem();
        }
    }


    private void SetNewMovingItem(Item item)
    {
        _movingItem = item;
        _player.PlayerInventoryUI.MovingImage.color = Color.white;
        _player.PlayerInventoryUI.MovingImage.sprite = _movingItem.InventoryIcon;
    }

    private void EndMove(bool tryToMove)
    {
        Item newItem = null;
        if (tryToMove)
        {
            if (_player.PlayerWindow.PointerOverWindow)
            {
                if (_newClickedSlot != null)
                {
                    EquipmentSlot equipmentSlot = _newClickedSlot as EquipmentSlot;
                    if (equipmentSlot != null)
                    {
                        if (_newClickedSlot.SlotInteractable)
                        {
                            newItem = equipmentSlot.SlotItem;
                            _player.PlayerEquipmentController.EquipItem(equipmentSlot.EquipmentSlotType, _movingItem as Equipment, false);
                        }
                    }
                    else
                    {
                        newItem = _newClickedSlot.SlotItem;
                        _newClickedSlot.RightPointerClicked += OnItemUsed;
                        _newClickedSlot.AddItemToSlot(_movingItem);
                    }
                }
                else
                {
                    return;
                }
                if (newItem != null)
                {
                    SetNewMovingItem(newItem);
                    return;
                }
            }
            else
            {
                Debug.Log("Drop");
            }
        }
        else
        {
            _lastClickedSlot.AddItemToSlot(_movingItem);
            _lastClickedSlot.RightPointerClicked += OnItemUsed;
        }
        _lastClickedSlot = null;
        _newClickedSlot = null;
        _movingItem = null;
        _player.PlayerInventoryUI.MovingImage.color = Color.clear;
        _player.PlayerInventoryUI.MovingImage.sprite = null;
    }
}
