using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorController : MonoBehaviour
{
    [HideInInspector] public bool unlocked = false;
    [HideInInspector] public bool is100Completed = false;
    [HideInInspector] public int[] letters;
    private Image unlockImage;
    private Image level;
    public Loader.Scene scene;

    private Color originalColor;
    private int progress = 0;

    private void Start()
    {
        unlockImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        level = GetComponent<Image>();
        originalColor = level.color;

        SaveController.Instance.LoadLevel(gameObject.name, this);
        UpdateLevelImage();
        PlayerPrefs.SetInt("FinalPro", progress);
    }

    private void UpdateLevelImage()
    {
        if(!unlocked)
        {
            unlockImage.gameObject.SetActive(true);   
        }
        else
        {
            unlockImage.gameObject.SetActive(false);

            if (is100Completed)
            {
                level.color = Color.yellow;
                progress++;
            }
            else
            {
                level.color = originalColor;
            }
        }
    }

    public void LoadLevel()
    {
        if(unlocked)
        {
            SaveController.Instance.LoadPlayer();
            PlayerPrefs.SetInt("Letter S", letters[0]);
            PlayerPrefs.SetInt("Letter L", letters[1]);
            PlayerPrefs.SetInt("Letter I", letters[2]);
            PlayerPrefs.SetInt("Letter M", letters[3]);
            PlayerPrefs.SetInt("Letter E", letters[4]);
            Loader.Load(scene);
        }
    }

    public void GoBack()
    {
        Loader.Load(Loader.Scene.Title);
    }
}
