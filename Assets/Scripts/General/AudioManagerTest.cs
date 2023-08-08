using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTest : MonoBehaviour
{
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;
    [SerializeField] private AudioClip sfxAtom;
    [SerializeField] private AudioClip sfxRobotDeath;



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Play music 1");
            AudioManager.Instance.PlayMusic(music1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Play music 2");
            AudioManager.Instance.PlayMusic(music2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Play music 1 with fade");
            AudioManager.Instance.PlayMusicWithFade(music1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Play music 2 with fade");
            AudioManager.Instance.PlayMusicWithFade(music2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Play music 1 with crossfade");
            AudioManager.Instance.PlayMusicWithCrossFade(music1, 3f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Play music 2 with crossfade");
            AudioManager.Instance.PlayMusicWithCrossFade(music2, 3f);
        }
    }

}
