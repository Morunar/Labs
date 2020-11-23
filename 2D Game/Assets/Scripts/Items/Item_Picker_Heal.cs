using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Picker_Heal : MonoBehaviour
{
    [SerializeField] private int _healValue;

    private void OnTriggerEnter2D(Collider2D info)
    {
        info.GetComponent<Player_Controller>().RestroreHP(_healValue); 
        Destroy(gameObject);  
    }
}
