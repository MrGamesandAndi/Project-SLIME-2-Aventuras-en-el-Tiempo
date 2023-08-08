using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureBossController : MonoBehaviour
{
    public float normalSpeed = 100f;
    public float enragedSpeed = 170f;

    [HideInInspector] public float speed;
    private SpriteRenderer face;
    private Color green = Color.green;
    private Color red = Color.red;
    private bool isCalm = true;
    private BossHealth bossHealth;


    void Start()
    {
        speed = normalSpeed;
        face = GameObject.Find("Face").GetComponent<SpriteRenderer>();
        bossHealth = GetComponentInChildren<BossHealth>();
        face.color = green;
    }

   public void ChangeMood()
   {
        if (isCalm)
        {
            face.color = red;
            face.GetComponent<Transform>().Rotate(new Vector3(180, 0));
            speed = enragedSpeed;
            bossHealth.isInvulnerable = true;
        }
        else
        {
            face.color = green;
            face.GetComponent<Transform>().Rotate(new Vector3(180, 0));
            speed = normalSpeed;
            bossHealth.isInvulnerable = false;
        }

        isCalm = !isCalm;
   }
}
