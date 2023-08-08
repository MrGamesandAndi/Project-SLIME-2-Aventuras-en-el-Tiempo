using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravity : MonoBehaviour
{
    public bool top;
    private GameObject boss;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Rotation(collision.transform);
        }
    }

    private void RotateBoss()
    {
        boss.GetComponent<FutureBossController>().ChangeMood();
    }

    private void Rotation(Transform transform)
    {
        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        CharacterController2D player = transform.GetComponent<CharacterController2D>();

        if (!top && player.GetComponent<Rigidbody2D>().gravityScale == 1)
        {
            rb.gravityScale *= -1;
            transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
            player.m_JumpForce = -player.m_JumpForce;
            if (boss != null)
            {
                RotateBoss();
            }
        }
        else if(top && player.GetComponent<Rigidbody2D>().gravityScale == -1)
        {
            rb.gravityScale *= -1;
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            player.m_JumpForce = -player.m_JumpForce;
            if (boss != null)
            {
                RotateBoss();
            }
        }
    }
}
