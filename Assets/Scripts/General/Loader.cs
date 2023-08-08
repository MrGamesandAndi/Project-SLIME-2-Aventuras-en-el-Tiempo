using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Title,
        Loading,
        WorldSelector,
        Intro,

        Desert11,
        Desert12,
        Desert13,

        Desert21,
        Desert22,
        Desert23,


        Desert31,
        Desert32,
        
        DesertBossIntro,
        Desert41,
        DesertBossEnd,

        Pirate11,
        Pirate12,
        Pirate13,

        Pirate21,
        Pirate22,
        Pirate23,

        Pirate31,
        Pirate32,

        PirateBossIntro,
        Pirate41,
        PirateBossEnd,

        Future11,
        Future12,

        Future21,
        Future22,

        Future31,

        FutureBossIntro,
        Future41,
        FutureBossEnd,

        Dino11,
        Dino12,
        
        Dino21,
        Dino22,

        Dino31,
        Dino32,

        DinoBossIntro,
        Dino41,
        DinoBossEnd,

        Present11,
        Present12,

        Present21,
        Present22,
        
        Present31,
        Present32,

        PresentBossIntro,
        Present41,
        PresentBossEnd,
        GameOver
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        //Set the loader callback action to load the target scene
        onLoaderCallback = () =>
        {
            SceneManager.LoadSceneAsync(scene.ToString());
        };

        //Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString(),LoadSceneMode.Additive);

    }

    public static void ReloadScene()
    {
        //Set the loader callback action to load the target scene
        onLoaderCallback = () =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        };

        //Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString(), LoadSceneMode.Additive);
    }

    public static void LoaderCallBack()
    {
        //Triggered after first Update which lets the screen refresh
        //Execute the loader callback action which will load the target scene
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
