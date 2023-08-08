using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController2D player = collision.GetComponent<CharacterController2D>();

        if (player != null)
        {
            //Hit the player
            player.DamageKnockback(999);
        }
    }
}
