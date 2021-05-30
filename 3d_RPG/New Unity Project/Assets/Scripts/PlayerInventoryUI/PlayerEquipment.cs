using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Items;
using Player;

[Serializable]
public class PlayerEquipment
{
    [SerializeField] private EquipmentParams _rightHand;
    [SerializeField] private EquipmentParams _leftHand;

    public EquipmentParams RightHand => _rightHand;
    public EquipmentParams LeftHadn => _leftHand;

    public EquipmentParams GetParamsByType(EquipmentSlotType type)
    {
        switch (type)
        {
            case EquipmentSlotType.ItemRight:
                return _rightHand;
            case EquipmentSlotType.ItemLeft:
                return _leftHand;
            default:
                Debug.LogError(type + " is not supported yet");
                return null;
        }
    }
}

[Serializable]
public class EquipmentParams
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter _meshFilter;

    public MeshRenderer MeshRendere => _meshRenderer;
    public MeshFilter MeshFilter => _meshFilter;
}
