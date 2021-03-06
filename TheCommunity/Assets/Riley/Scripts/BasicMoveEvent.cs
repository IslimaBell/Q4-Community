using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveEvent : MonoBehaviour
{
    public GameObject actor;
    public float XChange;
    public float YChange;
    public string TagToActivate;
    public bool deletesSelf;
    public bool EventFlipsX;
    public bool EventFlipsY;
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

        if (collision.gameObject.tag == TagToActivate)
        {
            Debug.Log("EVENT TRIGGERED");
            actor.GetComponent<SpriteRenderer>().flipX = EventFlipsX;
            actor.GetComponent<SpriteRenderer>().flipY = EventFlipsY;
            actor.GetComponent<Rigidbody2D>().velocity = new Vector2(XChange, YChange);
            //actor.GetComponent<Rigidbody2D>().velocity = new Vector2(actor.GetComponent<Rigidbody2D>().velocity.x + XChange, actor.GetComponent<Rigidbody2D>().velocity.y + YChange);
            if (deletesSelf == true) Destroy(this.gameObject);
        }

        
    }


}
