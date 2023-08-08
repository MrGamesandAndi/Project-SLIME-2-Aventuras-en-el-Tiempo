using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveTime : MonoBehaviour
{
    public float time = 3f;

    void Start()
    {
        Destroy(gameObject, time);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterController2D>().DamageKnockback(10);
        }
    }
}
