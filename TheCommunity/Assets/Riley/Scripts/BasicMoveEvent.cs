using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveEvent : MonoBehaviour
{
    public GameObject actor;
    public float XChange;
    public float YChange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("EVENT TRIGGERED");
        actor.GetComponent<Rigidbody2D>().velocity = new Vector2(XChange, YChange);
        //actor.GetComponent<Rigidbody2D>().velocity = new Vector2(actor.GetComponent<Rigidbody2D>().velocity.x + XChange, actor.GetComponent<Rigidbody2D>().velocity.y + YChange);

    }


}
