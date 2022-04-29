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
    public bool isChasing;
    public AudioSource nomralTheme;
    public AudioSource chaseTheme;
    Rigidbody2D rb;
    [SerializeField]
    private float jumpPower = 1f;
    bool canJump;
    bool canClimb = false;
    [SerializeField]
    private float dirY;
    [SerializeField]
    private RefinedMovement player;
    [SerializeField]
    private SceneCollision enemy;
    

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

        if (player.Hiding == false && Vector2.Distance(transform.position, target.position) < MaxDistance)
        {

            enemy.enabled = true;
            Debug.Log("Chasing");
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            bool isChasing = true;

        }
        else if (transform.position != patrolPoints[currentPointIndex].position)
        {
            enemy.enabled = false;
            Debug.Log("Movement");
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            bool isChasing = false;

        }

        if(isChasing = true)
        {
            Debug.Log("changing songs");
            nomralTheme.Pause();
            chaseTheme.Play();
        }
        else
        {
            nomralTheme.Play();
            chaseTheme.Pause();
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
        if (other.gameObject.tag == "Obstical")
        {
            canJump = true;
            rb.AddForce(Vector2.up * jumpPower);


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<VineScript>())
        {
            canClimb = false;
        }
    }
}