using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public float speed;
    public Transform target;
    public float MaxDistance;
    public Transform[] patrolPoints;
    public float waitTime;
    public int currentPointIndex;
    bool once = false;
    Rigidbody2D rb;
    [SerializeField]
    private float jumpPower = 1f;
    bool canJump;
    bool canClimb = false;
    [SerializeField]
    private float dirY;
    [SerializeField]
    private RefinedMovement player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(canClimb)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }

        if (Vector2.Distance(transform.position, target.position) < MaxDistance)
        {

            if (player.Hiding == false)
            {
                Debug.Log("Chasing");
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                
            }


        }

        else if (transform.position != patrolPoints[currentPointIndex].position)
        {

            Debug.Log("Movement");
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        }

        else if (once == false)
        {

            //{
            Debug.Log("once");
            once = true;
            StartCoroutine(Wait());
            //}

        }

    }

    IEnumerator Wait()
    {
        Debug.Log("Active");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Passed");
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
            Debug.Log("Movin");
        }
        else
        {
            currentPointIndex = 0;
            //currentPointIndex--;
        }
        once = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Climb")
        {

            if (other.GetComponent<VineScript>())
            {
                canClimb = true;

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<VineScript>())
        {
            canClimb = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
                if (other.gameObject.tag == "Obstical")
        {
            canJump = true;
            rb.AddForce(Vector2.up * jumpPower);


        }
    }
}