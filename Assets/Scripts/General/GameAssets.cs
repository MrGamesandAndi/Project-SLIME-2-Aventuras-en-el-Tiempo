using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public Sprite healthPotion;
    public Sprite fireBullet;
    public Sprite waterBullet;
    public Sprite acidBullet;

    public AudioClip principalMusic;
    public AudioClip secondaryMusic;
    public AudioClip sfxAtom;
    public AudioClip sfxRobotDeath;
}
