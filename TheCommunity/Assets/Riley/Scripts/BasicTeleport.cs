using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTeleport : MonoBehaviour
{
    public GameObject player;
    public float transportX;
    public float transportY;
    public bool isPlayerHere;


    // Start is called before the first frame update
    void Start()
    {
        isPlayerHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isPlayerHere == true)
        {
            player.transform.position = new Vector3(transportX, transportY, player.transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //player.transform.position = new Vector3(transportX, transportY, player.transform.position.z);

            isPlayerHere = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerHere = false;
        }
    }

}
