using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public float speed;
    public Transform target;
    public Transform self;
    public float MaxDistance;
    public Transform[] patrolPoints;
    public float waitTime;
    public int currentPointIndex;
    bool once = false;
    public bool isChasing;
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
    private Vector3 spawnPoint;
    private Vector3 distance;

    public GameObject enemyPrefab;
    public GameObject[] enemys;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;

        enemys = GameObject.FindGameObjectsWithTag("enemy");

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
<<<<<<< HEAD
            distance = transform.position - target.position;

            if (distance.x > 0)
            {
                self.localScale = new Vector3 (-1.3f, 1.3f, 1);
            }
            else
            {
                self.localScale = new Vector3(1.3f, 1.3f, 1);
            }
            isChasing = true;
=======
             foreach (GameObject enemy in enemys)
            {
                isChasing = true;
                player.isBeingChasing = true;
            }
            //player.isBeingChasing = true;
>>>>>>> fd65058314e4da4af4a852f8b1040e3130c60d39
            enemy.enabled = true;
            
            //Debug.Log("Chasing");
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            

        }
        else if (transform.position != patrolPoints[currentPointIndex].position)
        {
            //Debug.Log("NotChasing");
            isChasing = false;
            //player.isBeingChasing = false;
            enemy.enabled = false;
            //Debug.Log("Movement");
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            

        }

        
        if(isChasing == true)
        {          
            Debug.Log("changing songs");
            player.isBeingChasing = true;
        }
        else if(isChasing == false)
        {
            Debug.Log("NotChasing");
            player.isBeingChasing = false;
        }
        

        if (player.isDead == true)
        {
            StartCoroutine(respawn());
        }

        
    }

    private IEnumerator respawn()
    {
        yield return new WaitForSeconds(1);
        transform.position = spawnPoint;
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