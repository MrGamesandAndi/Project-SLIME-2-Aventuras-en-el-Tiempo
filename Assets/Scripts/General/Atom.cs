using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<CharacterController2D>();

        if(player != null)
        {
            AudioManager.Instance.PlaySfx(GameAssets.i.sfxAtom);
            gameObject.GetComponent<Animator>().SetBool("IsCollected", true);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject, .8f);
            gameController.CollectSlime("Atom", 1);
        }
    }
}
