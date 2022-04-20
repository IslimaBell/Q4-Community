using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHideable : MonoBehaviour
{
    [SerializeField]
    private RefinedMovement player;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HideableObject>())
        {
            if(other.tag == "Hide")
            {
                player.HidingAllowed = true;
                player.hideIndicator.SetActive(true);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<HideableObject>())
        {
            if(other.tag == "Hide")
            {
                player.HidingAllowed = false;
                player.hideIndicator.SetActive(false);
            }
            
        }
    }

    
}
