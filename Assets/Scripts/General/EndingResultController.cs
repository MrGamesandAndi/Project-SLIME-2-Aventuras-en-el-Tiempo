using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingResultController : MonoBehaviour
{
    private int progress;
    public GameObject goodEnding;
    public GameObject badEnding;

    private void Start()
    {
        progress = PlayerPrefs.GetInt("FinalPro");
        goodEnding.SetActive(false);
        badEnding.SetActive(false);

        if(progress >= 10)
        {
            goodEnding.SetActive(true);
        }
        else
        {
            badEnding.SetActive(true);
        }
    }
}
