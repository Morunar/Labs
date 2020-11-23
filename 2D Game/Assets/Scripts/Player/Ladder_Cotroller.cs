using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_Cotroller : MonoBehaviour
{
    private float inputVertical;
    [SerializeField] private float speed;
    Rigidbody2D player;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatisLadder;
    private bool Climbing;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatisLadder);

        if (hitinfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Climbing = true;
            }
        }
        else
        {
            Climbing = false;
        }

        if (Climbing == true && hitinfo.collider != null)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            player.velocity = new Vector2(player.position.x, inputVertical * speed);
            player.gravityScale = 0;
        }
        else
        {
            player.gravityScale = 3;
        }
    }
}