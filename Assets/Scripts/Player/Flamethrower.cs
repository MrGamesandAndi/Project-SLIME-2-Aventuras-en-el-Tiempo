using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public int damageAmount = 5;

    private void Awake()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            ObjectHealth objectHealth = collision.GetComponent<ObjectHealth>();
            if (objectHealth != null)
            {
                objectHealth.Damage(1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            ObjectHealth objectHealth = collision.GetComponent<ObjectHealth>();
            if (objectHealth != null)
            {
                objectHealth.Damage(damageAmount);
            }
        }
    }
}
