using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorController : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 2f;
    public Transform groundDetection;
    public int damageAmount = 10;

    private bool movingRight = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (!groundInfo.collider)
        {
            if(movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
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
}
