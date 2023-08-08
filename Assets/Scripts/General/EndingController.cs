using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;  
    public GameObject[] letters;
    [HideInInspector] public int[] lettersCollected;
    [HideInInspector] public int[] ammo;
    private GameObject player;
    public Loader.Scene nextLevel;
    private bool is100Complete = true;

    private void Start()
    {
        endScreen.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        lettersCollected = new int[5];
        ammo = new int[5];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PauseMenu.Instance.canPause = false;
            endScreen.SetActive(true);
            player.layer = LayerMask.NameToLayer("GhostPlayer");
            player.GetComponentInChildren<PlayerMovement>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            player.GetComponentInChildren<Weapon>().enabled = false;


            if (PlayerPrefs.GetInt("Letter S") == 1)
            {
                letters[0].SetActive(true);
                lettersCollected[0] = 1;
                PlayerPrefs.SetInt("Letter S", 0);
            }

            if (PlayerPrefs.GetInt("Letter L") == 1)
            {
                letters[1].SetActive(true);
                lettersCollected[1] = 1;
                PlayerPrefs.SetInt("Letter L", 0);
            }

            if (PlayerPrefs.GetInt("Letter I") == 1)
            {
                letters[2].SetActive(true);
                lettersCollected[2] = 1;
                PlayerPrefs.SetInt("Letter I", 0);
            }

            if (PlayerPrefs.GetInt("Letter M") == 1)
            {
                letters[3].SetActive(true);
                lettersCollected[3] = 1;
                PlayerPrefs.SetInt("Letter M", 0);
            }

            if (PlayerPrefs.GetInt("Letter E") == 1)
            {
                letters[4].SetActive(true);
                lettersCollected[4] = 1;
                PlayerPrefs.SetInt("Letter E", 0);
            }

            for (int i = 0; i < lettersCollected.Length; i++)
            {
                if(lettersCollected[i] != 1)
                {
                    is100Complete = false;
                    break;
                }
            }

            ammo[0] = GameController.Instance.GetAmount("Fire");
            ammo[1] = GameController.Instance.GetAmount("Water");
            ammo[2] = GameController.Instance.GetAmount("Acid");
            ammo[3] = GameController.Instance.GetAmount("Health");
            ammo[4] = GameController.Instance.GetAmount("Atom");

            SaveController.Instance.SavePlayer(ammo);
            string name = SceneManager.GetActiveScene().name.Substring(0, SceneManager.GetActiveScene().name.Length - 1);
            SaveController.Instance.SaveLevel(name, lettersCollected, true, is100Complete);
        }
    }

    public void LoadNextLevel()
    {
        PauseMenu.Instance.canPause = true;
        SaveController.Instance.SaveLevel(nextLevel.ToString().Substring(0,nextLevel.ToString().Length-1), new int[6], true, false);
        Loader.Load(nextLevel);
    }
}
