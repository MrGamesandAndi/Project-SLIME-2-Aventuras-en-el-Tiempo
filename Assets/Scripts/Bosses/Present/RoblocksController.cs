using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoblocksController : MonoBehaviour
{
    public CircleCollider2D trigger;
    public int damageAmount = 30;
    private GameObject player;
    private Animator animator;
    private BossHealth bossHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();
        bossHealth.isInvulnerable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController2D player = collision.GetComponent<CharacterController2D>();

        if (player != null)
        {
            player.DamageKnockback(damageAmount);
        }
    }

    IEnumerator Jump()
    {
        float cont = Time.time + 1f;

        while (Time.time < cont)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 20f);
            yield return null;
        }

        while (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x, 6.29f, 0f)) > 0.02f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x-4f, 6.29f, 0), Time.deltaTime * 20f);
            yield return null;
        }

        animator.SetTrigger("Land");
    }

    public void EnableCollider()
    {
        bossHealth.isInvulnerable = false;
        trigger.enabled = true;
    }

    public void DisableCollider()
    {
        trigger.enabled = false;
        bossHealth.isInvulnerable = true;
    }
}
