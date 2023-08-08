using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    public bool canMove = false;
    public float speed = 0.1f;
    private Vector2 position;
    private Transform player;
    private Vector2 direction;
    private bool facingRight = false;
    public int damageAmount = 25;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController2D player = collision.GetComponent<CharacterController2D>();

        if (player != null)
        {
            //Hit the player
            player.DamageKnockback(damageAmount);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 20)
        {
            if (player.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
                Flip();
            if (player.position.x < transform.position.x && facingRight)
                Flip();
        }

        if (canMove)
        {
            Move(speed);
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    private void Move(float speed)
    {
        position = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }
}
