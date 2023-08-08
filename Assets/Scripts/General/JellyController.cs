using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyController : MonoBehaviour
{
    private GameController gameController;
    public ParticleSystem particleEffects;
    public Slime[] slimes;
    private int numberOfSlimes = 0;
    private int actualSlime = 0;
    public AudioClip collect;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        numberOfSlimes = slimes.Length;
        actualSlime = Random.Range(0, numberOfSlimes);
        transform.GetComponent<SpriteRenderer>().color = slimes[actualSlime].color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(particleEffects, transform.position,transform.rotation);
            AudioManager.Instance.PlaySfx(collect,0.5f);
            Destroy(gameObject);
            gameController.CollectSlime(slimes[actualSlime].type, 1);
        }
    }
}
