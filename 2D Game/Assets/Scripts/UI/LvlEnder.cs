using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlEnder : MonoBehaviour
{
    [SerializeField] private Collider2D _LvlEnderCollider;
    [SerializeField] private SpriteRenderer _lvlEnderSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ServiceManager.Instanse.EndLevel();
    }
    private void Update()
    {
        if(Task._shard >= 3)
        {
            _lvlEnderSprite.enabled = true;
            _LvlEnderCollider.enabled = true;
           
        }
        
    }
}
