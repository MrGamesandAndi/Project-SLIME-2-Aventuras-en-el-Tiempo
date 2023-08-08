using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureBossRotate : MonoBehaviour
{
    private GameObject boss;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(boss != null)
            {
                Rotation();
            }
        }
    }

    private void Rotation()
    {
        boss.GetComponent<FutureBossController>().ChangeMood();
    }
}
