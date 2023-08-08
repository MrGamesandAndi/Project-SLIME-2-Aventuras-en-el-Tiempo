using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Loader.Scene startLevel;
    public Loader.Scene levelSelect;
    public static MainMenuController instance;
    public AudioClip titleMusic;
    public GameObject resumeButton;
    [Range(0,1)] public float playerHp;
    public GameObject menu, credits;

    private void Start()
    {
        CursorController.instance.ActivateMenuCursor();
        AudioManager.Instance.PlayMusic(titleMusic);
        instance = this;
    }

    public static void MainMenuController_Static(Loader.Scene startLevel)
    {
        instance.startLevel = startLevel;
        instance.NewGame();
    }

    public void NewGame()
    {
        SaveController.Instance.ErasePlayer();
        SaveController.Instance.EraseLevels();
        Loader.Load(startLevel);
        PlayerPrefs.SetFloat("PlayerCurrentHP", playerHp);
        PlayerPrefs.SetInt("PlayerFireAmmo", 0);
        PlayerPrefs.SetInt("PlayerWaterAmmo", 0);
        PlayerPrefs.SetInt("PlayerAcidAmmo", 0);
        PlayerPrefs.SetInt("PlayerAtoms", 0);
        PlayerPrefs.SetInt("PlayerHealthPotion", 0);

        PlayerPrefs.SetInt("Letter S", 0);
        PlayerPrefs.SetInt("Letter L", 0);
        PlayerPrefs.SetInt("Letter I", 0);
        PlayerPrefs.SetInt("Letter M", 0);
        PlayerPrefs.SetInt("Letter E", 0);
    }

    public void LevelSelect()
    {
        Loader.Load(levelSelect);
    }

    public void LoadLevel()
    {
        Loader.Load(startLevel);
    }

    public void LoadMenu()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void LoadCredits()
    {
        credits.SetActive(true);
        menu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Game exited");
        Application.Quit();
    }
}
