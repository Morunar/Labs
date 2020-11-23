using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Picker_Mana : MonoBehaviour
{
    [SerializeField] private int _manaValue;

    private void OnTriggerEnter2D(Collider2D info)
    {
        info.GetComponent<Player_Controller>().RestroreMP(_manaValue);
        Destroy(gameObject);
    }
}
