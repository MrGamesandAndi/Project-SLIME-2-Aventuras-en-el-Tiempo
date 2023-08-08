using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance;
    private const string SEPARATOR = "#SAVE-VALUE#";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayer(int[] ammo)
    {
        string[] contents = new string[]
        {
            ""+ammo[0],
            ""+ammo[1],
            ""+ammo[2],
            ""+ammo[3],
            ""+ammo[4]
        };
        string saveString = string.Join(SEPARATOR, contents);
        File.WriteAllText(Application.persistentDataPath + "/SAVES/PLAYER/playersave.txt", saveString);
    }

    public void LoadPlayer()
    {
        string saveString = File.ReadAllText(Application.persistentDataPath + "/SAVES/PLAYER/playersave.txt");

        string[] contents = saveString.Split(new[] { SEPARATOR }, System.StringSplitOptions.None);
        PlayerPrefs.SetInt("PlayerFireAmmo", int.Parse(contents[0]));
        PlayerPrefs.SetInt("PlayerWaterAmmo", int.Parse(contents[1]));
        PlayerPrefs.SetInt("PlayerAcidAmmo", int.Parse(contents[2]));
        PlayerPrefs.SetInt("PlayerHealthPotion", int.Parse(contents[3]));
        PlayerPrefs.SetInt("PlayerAtoms", int.Parse(contents[4]));
    }

    public void ErasePlayer()
    {
        string saveString = Application.persistentDataPath + "/SAVES/PLAYER/playersave.txt";
        if(File.Exists(saveString))
        {
            File.Delete(saveString);
        }
    }

    public void EraseLevels()
    {
        string saveString = Application.persistentDataPath + "/SAVES/LEVELS";
        DirectoryInfo directory = new DirectoryInfo(saveString);

        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }
    }

    public void SaveLevel(string name, int[] letters, bool isUnlocked = false, bool is100Completed = false)
    {
        LevelSave levelSave = new LevelSave
        {
            name = name,
            letters = letters,
            isUnlocked = isUnlocked,
            is100Completed = is100Completed
        };

        string json = JsonUtility.ToJson(levelSave);
        File.WriteAllText(Application.persistentDataPath + "/SAVES/LEVELS/"+name+"save.txt", json);
    }

    public void LoadLevel(string name, LevelSelectorController levelSelectorController)
    {
        string save = Application.persistentDataPath + "/SAVES/LEVELS/" + name + "save.txt";

        if (CheckIfDataExists(save))
        {
            string saveString = File.ReadAllText(Application.persistentDataPath + "/SAVES/LEVELS/" + name + "save.txt");
            LevelSave levelSave = JsonUtility.FromJson<LevelSave>(saveString);
            levelSelectorController.letters = levelSave.letters;
            levelSelectorController.unlocked = levelSave.isUnlocked;
            levelSelectorController.is100Completed = levelSave.is100Completed;
        }
        else
        {
            levelSelectorController.letters = new int[6];
            levelSelectorController.unlocked = false;
            levelSelectorController.is100Completed = false;
        }
    }

    public bool CheckIfDataExists(string saveString)
    {
        if(File.Exists(saveString))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private class LevelSave
    {
        public string name;
        public int[] letters;
        public bool isUnlocked;
        public bool is100Completed;
    }
}
