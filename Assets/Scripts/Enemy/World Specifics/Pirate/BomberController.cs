using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberController : MonoBehaviour
{
    private Transform playerPosition;
    private Vector2 position;
    public float speed = 10f;
    public float sightRange = 25f;
    public LayerMask playerLayer;
    private Animator animator;
    public int damageAmount = 50;
    private bool facingRight = false;
    private Vector2 direction = Vector2.left;


    private void Start()
    {
        animator = GetComponent<Animator>();
        CheckForPlayer();
    }

    private void Update()
    {
        CheckForPlayer();

        if (Vector3.Distance(playerPosition.position, transform.position) < 20)
        {
            if (playerPosition.position.x > transform.position.x && !facingRight)
            {
                //if the target is to the right of enemy and the enemy is not facing right
                Flip();
                direction = Vector2.right;
            }
            if (playerPosition.position.x < transform.position.x && facingRight)
            {
                Flip();
                direction = Vector2.left;
            }
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }


    private void CheckForPlayer()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        var hit = Physics2D.Raycast(transform.position, direction, sightRange, playerLayer);
        Debug.DrawRay(transform.position, direction, Color.magenta);
        if (hit && hit.transform.name == "Player")
        {
            animator.SetBool("IsAttacking", true);
            position = new Vector2(playerPosition.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController2D player = collision.GetComponent<CharacterController2D>();

        if (player != null)
        {
            //Hit the player
            player.DamageKnockback(damageAmount);
            gameObject.GetComponent<ObjectHealth>().Damage(999);
        }
    }
}
