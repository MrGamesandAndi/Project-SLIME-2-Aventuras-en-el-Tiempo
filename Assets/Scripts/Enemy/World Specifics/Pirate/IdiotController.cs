using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdiotController : MonoBehaviour
{
    private Transform playerPosition;
    [HideInInspector] public bool isEnraged = false;
    private Vector2 direction;
    private Animator animator;
    public float enragedTime = 10f;
    private float timer;

    public int damageInput = 25;
    public float speed = 5f;
    public Transform[] patrolPoints;
    private int currentPoint = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
        timer = enragedTime;
    }

    private void Update()
    {
        
        if (isEnraged)
        {
            animator.SetBool("IsTriggered", true);
            Attack();

            if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                isEnraged = false;
                timer = enragedTime;
                animator.SetBool("IsTriggered", false);
            }
        }

        
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            isEnraged = true;
        }

        if(collision.CompareTag("Player") && isEnraged)
        {
            collision.GetComponent<CharacterController2D>().DamageKnockback(damageInput);
        }
    }

    private void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position,patrolPoints[currentPoint].position) < 0.02f)
        {
            Flip();
            if (currentPoint == 0)
            {
                currentPoint = 1;
            }
            else
            {
                currentPoint = 0;
            }
        }
    }
}
