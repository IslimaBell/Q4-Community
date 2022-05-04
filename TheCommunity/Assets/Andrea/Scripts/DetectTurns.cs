using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTurns : MonoBehaviour
{
    [SerializeField]
    private AI enemy;
    public bool flipped;
    [SerializeField]
    private SpriteRenderer Enemy;


    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.GetComponent<TurnRight>())
        {
            if (other.tag == "TurnRight")
            {
                enemy.self.localScale = new Vector3(1.3f, 1.3f, 1);
                flipped = true;
                enemy.currentPointIndex = 1;
            }

        }
        if (other.GetComponent<TurnLeft>())
        {
            if (other.tag == "TurnLeft")
            {
                enemy.self.localScale = new Vector3(-1.3f, 1.3f, 1);
                flipped = true;
                enemy.currentPointIndex = 0;
            }

        }
    }
}
