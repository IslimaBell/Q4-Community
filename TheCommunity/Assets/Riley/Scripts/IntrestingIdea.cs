using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IntrestingIdea : MonoBehaviour
{
    public bool IsInside;
    
    private Tilemap tm;
    // Start is called before the first frame update
    void Start()
    {
        
        IsInside = false;
        tm = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(tm.color.a);

        if (IsInside == true && tm.color.a > 0)
        {
            Color tmp = tm.color;
            tmp.a = 0;
            tm.color = tmp;
            //tm.RefreshAllTiles();
        }

        

        if (IsInside == false && tm.color.a < 255)
        {
            Color tmp = tm.color;
            tmp.a = 255;
            tm.color = tmp;
            //tm.RefreshAllTiles();
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") IsInside = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") IsInside = false;
    }



}
