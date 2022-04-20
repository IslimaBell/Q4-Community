using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public const int Time = 120;
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
       /* float distToPlayer = Vector2.Distance(transform.position, player.position);
        
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
                isAgro = false;
            }
        }
        */


    }
     
}
