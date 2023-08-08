using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerController : MonoBehaviour
{
    public GameObject[] enemiesAffected;
    public float watchingTime = 10f;
    public bool isScreaming = false;
    public float sightRange = 25f;
    public LayerMask playerLayer;
    public AudioClip scream;

    private float timer;
    private Animator animator;
    private Transform playerPosition;
    private Vector2 position;
    private Vector2 direction = Vector2.left;


    void Start()
    {
        enemiesAffected = GameObject.FindGameObjectsWithTag("Enemy");
        animator = GetComponent<Animator>();
        timer = watchingTime;
    }

    void Update()
    {
        CheckForPlayer();
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Flip();
            timer = watchingTime;
        }
    }

    private void Flip()
    {
        if(direction == Vector2.left)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CheckForPlayer()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        var hit = Physics2D.Raycast(transform.position, direction, sightRange, playerLayer);
        Debug.DrawRay(transform.position, direction, Color.magenta);
        if (hit && hit.transform.name == "Player")
        {
            animator.SetBool("IsScreaming", true);
            isScreaming = true;
            foreach (var enemy in enemiesAffected)
            {
                if (enemy != null)
                {
                    if (enemy.name == "Pirate")
                    {
                        enemy.GetComponent<IdiotController>().isEnraged = true;
                    }
                }
            }
        }
        else
        {
            animator.SetBool("IsScreaming", false);
            isScreaming = false;
        }
    }
}
