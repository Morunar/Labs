using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Player;
public class ItemBody : MonoBehaviour
{
    [SerializeField] private ItemBase _itemBase;
    private MeshRenderer _MeshRenderer;
    private MeshFilter _MeshFilter;
    private Collider _itemCollider;
    private Item _item;

    private void Start()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
        _MeshFilter = GetComponent<MeshFilter>();
        _itemCollider = GetComponent<Collider>();
        _item = new Equipment(_itemBase as EquipmentBase);
    }


    public void Init(Mesh itemMesh, Material itemMaterial, Item item)
    {
        _MeshRenderer.material = itemMaterial;
        _MeshFilter.mesh = itemMesh;
        _itemCollider = gameObject.AddComponent<Collider>();
        _item = item;
    }

    public void OnPickUp(PlayerCreature player)
    {
        if (player.PlayerInventory.AddItemToInventory(_item))
        {
            _item.SetOwner(player);
            Destroy(_itemCollider);
        }
    }
}
