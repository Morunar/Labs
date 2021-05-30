using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreature : LivingCreature
{
    [SerializeField] private PlayerWindow _playerWidnow;
    [SerializeField] private PlayerEquipment _playerEquipment;

    public PlayerInventoryController PlayerInventory { get; private set; }
    public PlayerEquipmentController PlayerEquipmentController { get; private set; }

    public PlayerInventoryUI PlayerInventoryUI => _playerWidnow.PlayerInventory;
    public PlayerEquipmentUI PlayerEquipmentUI => _playerWidnow.PlayerEquipment;
    public PlayerEquipment PlayerEquipment => _playerEquipment;
    public PlayerWindow PlayerWindow => _playerWidnow;

    protected override void Start()
    {
        base.Start();
        ActionController = new PlayerActionController(this);
        _playerWidnow.InitComponents();
        PlayerInventory= new PlayerInventoryController(this);
        PlayerEquipmentController = new PlayerEquipmentController(this);

    }
}
