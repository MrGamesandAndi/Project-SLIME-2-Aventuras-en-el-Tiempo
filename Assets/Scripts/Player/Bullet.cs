using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb;
    public GameObject impactEffect;
    public float lifeTime = 2f;
    public float damageAmount = 1;
    public bool isChild = false;
    public float fireRate = 10f;
    public bool destroysOnContact = true;
    public bool showsParticlesOnImpact = true;
    public string ammoType;

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(showsParticlesOnImpact)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        ObjectHealth objectHealth = collision.GetComponent<ObjectHealth>();
        if(objectHealth != null)
        {
            objectHealth.Damage(damageAmount);
        }

        if(destroysOnContact)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (showsParticlesOnImpact)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        ObjectHealth objectHealth = collision.GetComponent<ObjectHealth>();
        if (objectHealth != null)
        {
            objectHealth.Damage(damageAmount);
        }

        if (destroysOnContact)
        {
            Destroy(gameObject);
        }
    }

}
