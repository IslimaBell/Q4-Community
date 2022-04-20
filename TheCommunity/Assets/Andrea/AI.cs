using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public const int Time = 12000;
    [SerializeField]
    Transform player;

    [SerializeField]
    float agrorange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb;

    [SerializeField]
    Transform CastPoint;
    public bool isAgro;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    void Update()
    {
        Vector2 endPos = CastPoint.position + Vector3.right * agrorange;
        Debug.DrawLine(CastPoint.position, endPos, Color.blue);
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if(distToPlayer < agrorange)
        {
            isAgro = true;
            ChasePlayer();
        }
        else
        {
            if (isAgro)
            {
                Invoke("StopChasing", Time);
            }
        }

    }

    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 2);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 2);
        }
    }

    void StopChasing()
    {
        isAgro = false;
        rb.velocity = Vector2.zero; 
    }
}
