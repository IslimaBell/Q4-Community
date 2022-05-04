﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            player.GetComponent<RefinedMovement>().isDead = true;

        }
    }
}
