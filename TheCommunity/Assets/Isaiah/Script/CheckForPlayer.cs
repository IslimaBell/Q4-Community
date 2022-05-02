using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject EnemyText;
    public GameObject fadeOut;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            player.GetComponent<RefinedMovement>().isDead = true;
            EnemyText.SetActive(true);
            fadeOut.SetActive(true);            
            animator.SetBool("IsDead", true);

        }
    }
}
