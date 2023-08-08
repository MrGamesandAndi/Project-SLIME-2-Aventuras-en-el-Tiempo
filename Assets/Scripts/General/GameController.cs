using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private TextMeshProUGUI fireText, waterText, acidText, healthText, metalText;
    public TextMeshProUGUI guiaShopTextBox;
    [HideInInspector] public int fireScore, waterScore, acidScore, healthScore, metalScore;
    public List<GameObject> availableShopItems;
    private HealthSystem healthSystem;
    public AudioClip music;
    public static AudioClip staticMusic;

    public static GameController Instance;

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
        staticMusic = music;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    void Start()
    {
        CursorController.instance.ActivateBullseyeClear();
        fireText = GameObject.Find("FireText").GetComponent<TextMeshProUGUI>();
        waterText = GameObject.Find("WaterText").GetComponent<TextMeshProUGUI>();
        acidText = GameObject.Find("AcidText").GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        metalText = GameObject.Find("AtomText").GetComponent<TextMeshProUGUI>();

        AudioManager.Instance.PlayMusic(music);

        healthSystem = GetComponent<HealthSystem>();

        CollectSlime("Fire", PlayerPrefs.GetInt("PlayerFireAmmo"));
        CollectSlime("Water", PlayerPrefs.GetInt("PlayerWaterAmmo"));
        CollectSlime("Acid", PlayerPrefs.GetInt("PlayerAcidAmmo"));
        CollectSlime("Atom", PlayerPrefs.GetInt("PlayerAtoms"));
        CollectSlime("Health", PlayerPrefs.GetInt("PlayerHealthPotion"));
    }

    public int UseAmmo(string type)
    {
        int ammo = 0;
        switch (type)
        {
            case "Fire":
                ammo = fireScore;
                UseSlime("Fire", 1);
                break;

            case "Water":
                ammo = waterScore;
                UseSlime("Water", 1);
                break;

            case "Acid":
                ammo = acidScore;
                UseSlime("Acid", 1);
                break;
        }
        return ammo;
    }

    public void UseSlime(string type, int quantity)
    {
        switch (type)
        {
            case "Fire":
                if(fireScore > 0)
                {
                    fireScore = fireScore - quantity;
                    fireText.text = "" + fireScore.ToString();
                    PlayerPrefs.SetInt("PlayerFireAmmo", fireScore);
                }
                break;

            case "Water":
                if (waterScore > 0)
                {
                    waterScore = waterScore - quantity;
                    PlayerPrefs.SetInt("PlayerWaterAmmo", waterScore);
                }
                waterText.text = "" + waterScore.ToString();
                break;

            case "Acid":
                if (acidScore > 0)
                {
                    acidScore = acidScore - quantity;
                    PlayerPrefs.SetInt("PlayerAcidAmmo", acidScore);
                }
                acidText.text = "" + acidScore.ToString();
                break;

            case "Health":
                if (healthScore > 0)
                {
                    healthScore = healthScore - quantity;
                    PlayerPrefs.SetInt("PlayerHealthPotion", healthScore);
                }
                healthText.text = "" + healthScore.ToString();
                break;

            case "Atom":
                if (metalScore > 0)
                {
                    metalScore = metalScore - quantity;
                    PlayerPrefs.SetInt("PlayerAtoms", metalScore);
                }
                metalText.text = "" + metalScore.ToString();
                break;

            default:
                Debug.Log("Erased something weird:" + type);
                break;
        }
    }

    public int GetAmount(string type)
    {
        switch (type)
        {
            case "Fire":
                return fireScore;

            case "Water":
                return waterScore;

            case "Acid":
                return acidScore;

            case "Health":
                return healthScore;

            case "Atom":
                return metalScore;

            default:
                Debug.Log("returned something weird:" + type);
                return 0;
        }
    }

    public void CollectSlime(string type, int quantity)
    {
        switch (type)
        {
            case "Fire":
                fireScore += quantity;
                PlayerPrefs.SetInt("PlayerFireAmmo", fireScore);
                fireText.text = "" + fireScore.ToString();
                break;

            case "Water":
                waterScore += quantity;
                PlayerPrefs.SetInt("PlayerWaterAmmo", waterScore);
                waterText.text = "" + waterScore.ToString();
                break;

            case "Acid":
                acidScore += quantity;
                PlayerPrefs.SetInt("PlayerAcidAmmo", acidScore);
                acidText.text = "" + acidScore.ToString();
                break;

            case "Health":
                healthScore += quantity;
                PlayerPrefs.SetInt("PlayerHealthPotion", healthScore);
                healthText.text = "" + healthScore.ToString();
                break;

            case "Atom":
                metalScore += quantity;
                PlayerPrefs.SetInt("PlayerAtoms", metalScore);
                metalText.text = "" + metalScore.ToString();
                break;

            default:
                Debug.Log("Grab something weird:"+type);
                break;
        }
    }

    public void SetShopDialog(string text)
    {
        guiaShopTextBox.SetText("");
        guiaShopTextBox.SetText(text);
    }
}
