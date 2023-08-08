using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Material material;
    private float fade = 1f;
    private bool isDissolving = false;
    public bool isCollected = false;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        if(PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(isDissolving)
        {
            fade -= Time.deltaTime;

            if(fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isDissolving = true;
            isCollected = true;
            PlayerPrefs.SetInt(gameObject.name, 1);
        }
    }
}
