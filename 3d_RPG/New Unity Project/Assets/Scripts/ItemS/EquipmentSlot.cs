using UnityEngine;
using Player;
using UnityEngine.EventSystems;
using Items;

public class EquipmentSlot : ItemSlot, IPointerEnterHandler, IPointerExitHandler
{
    private bool _pointerOverSlot;
    private Color _defaultColor;
    private Sprite _defaultSprite;
    [SerializeField] private EquipmentSlotType _equipmentSlotType;

    public EquipmentSlotType EquipmentSlotType => _equipmentSlotType;

    private void Start()
    {
        _defaultSprite = _slotImage.sprite;
        _defaultColor = _slotImage.color;
        SlotInteractable = true;
    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        _slotImage.color = _defaultColor;
        _slotImage.sprite = _defaultSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Item item = PlayerCreature.PlayerInventory.MovingItem;

        //if(item!= null)
        //{
        //    if ((item is Equipment) || ItemHelper.CanBeEquiped((item as Equipment).EquipmentBase.EquipmentType, _equipmentSlotType))
        //        SetSlotInteractability(false);
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsEquiped)
            SetSlotInteractability(true);
    }

    protected override void OnLeftPoiterDown()
    {
        if (!SlotInteractable)
            return;
        base.OnLeftPoiterDown();
    }

    protected override void OnRightPointerDown()
    {
        if (!SlotInteractable)
            return;
        base.OnRightPointerDown();
    }
}
