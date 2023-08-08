using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float health = 3;

    public Material matWhite;
    private Material matDefault;
    private SpriteRenderer spriteRenderer;
    public GameObject explosionRef;
    public bool spawnsObjects = true;
    public List<Transform> items = new List<Transform>();
    public AudioClip destroyedSfx;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
    }

    public void Damage(float amount)
    {
        health = health - amount;

        if (amount > 0)
        {
            spriteRenderer.material = matWhite;
        }

        if (health <= 0)
        {
            Kill();
        }
        else
        {
            Invoke("ResetMaterial", 0.1f);
        }
    }

    void ResetMaterial()
    {
        spriteRenderer.material = matDefault;
    }

    public void Kill()
    {
        GameObject explosion = Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f + transform.position.z);

        if (spawnsObjects)
        {
            Instantiate(items[Random.Range(0, items.Count - 1)], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        CursorController.instance.ActivateBullseyeClear();
        AudioManager.Instance.PlaySfx(destroyedSfx);
    }

    private void OnMouseEnter()
    {
        CursorController.instance.ActivateBullseyeDetect();
    }

    private void OnMouseExit()
    {
        CursorController.instance.ActivateBullseyeClear();
    }
}
