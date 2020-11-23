using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Picker_Armour : MonoBehaviour
{
    [SerializeField] private int _armourValue;

    private void OnTriggerEnter2D(Collider2D info)
    {
        info.GetComponent<Player_Controller>().RestoreArmour(_armourValue);
        Destroy(gameObject);
    }
}
