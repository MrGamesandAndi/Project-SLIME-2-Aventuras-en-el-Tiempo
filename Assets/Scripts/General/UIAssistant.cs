using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAssistant : MonoBehaviour
{
    private TextMeshProUGUI messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource talkingAudioSource;
    private int index;
    private Image faceHolder;
    [SerializeField] private Loader.Scene sceneToLoad;
    [SerializeField] private Sprite[] faces;
    [SerializeField] private Sprite[] bgs;
    [SerializeField] private Image bg;

    public string[] messageArray;

    private void Awake()
    {
        index = 0;
        faceHolder = transform.Find("Face").GetComponent<Image>();
        messageText = transform.Find("Message").Find("Message Text").GetComponent<TextMeshProUGUI>();
        talkingAudioSource = transform.Find("Talking Sound").GetComponent<AudioSource>();
        transform.Find("Message").GetComponent<Button_UI>().ClickFunc = () =>
        {
            StartTyping();
        };
    }

    private void Start()
    {
        StartTyping();
    }

    private void StartTyping()
    {
        if (textWriterSingle != null && textWriterSingle.IsActive())
        {
            textWriterSingle.WriteAllAndDestroy();
        }
        else
        {
            if (index <= messageArray.Length - 1)
            {
                bg.sprite = bgs[index];
                if(faces[index] != null)
                {
                    faceHolder.enabled = true;
                    faceHolder.sprite = faces[index];
                }
                else
                {
                    faceHolder.enabled = false;
                }
                string message = messageArray[index];
                talkingAudioSource.Play();
                textWriterSingle = TextWriter.AddWriter_Static(messageText, message, .05f, true, true, StopTalkingSound);
                index++;
            }
            else
            {
                //MainMenuController.MainMenuController_Static(sceneToLoad);
                Loader.Load(sceneToLoad);
            }
        }
    }

    private void StartTalkingSound()
    {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound()
    {
        talkingAudioSource.Stop();
    }
}
