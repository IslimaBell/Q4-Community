using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTurns : MonoBehaviour
{
    [SerializeField]
    private AI enemy;



    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.GetComponent<PatrolPoint>())
        {
            if (other.tag == "PatrolPoint")
            {
                if (enemy.currentPointIndex + 1 < enemy.patrolPoints.Length)
                {
                    enemy.currentPointIndex++;
                    Debug.Log("Movin");
                }
                else
                {
                    enemy.currentPointIndex = 0;
                    //currentPointIndex--;
                }
            }

        }*/
        
        if (other.GetComponent<TurnRight>())
        {
            if (other.tag == "TurnRight")
            {
                enemy.currentPointIndex = 1;
            }

        }
        if (other.GetComponent<TurnLeft>())
        {
            if (other.tag == "TurnLeft")
            {
                enemy.currentPointIndex = 0;
            }

        }
    }
}
