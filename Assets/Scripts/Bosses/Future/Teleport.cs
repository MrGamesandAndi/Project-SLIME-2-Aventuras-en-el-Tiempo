using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject player;
    private GameObject boss;
    private Vector2 playerInitialPos, bossInitialPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInitialPos = player.transform.position;
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossInitialPos = boss.transform.position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.transform.position = playerInitialPos;
            boss.transform.position = bossInitialPos;
        }
    }
}
