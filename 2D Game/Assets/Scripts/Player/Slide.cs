using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    public float distance = 2f;
    Movement_controller player;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Movement_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
        if(!player._grounded && hit.collider != null)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < speed)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
           
        }

    }
}
