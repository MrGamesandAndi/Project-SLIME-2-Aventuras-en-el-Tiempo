using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public float health = 1;
    public GameObject deathEffect;
    public bool isInvulnerable = false;
    public Material matWhite;
    private Material matDefault;
    private SpriteRenderer spriteRenderer;
    public GameObject explosionRef;
    public Transform healthBar;
    public AudioClip secondaryMusic;
    private Animator animator;
    public Loader.Scene scene;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(spriteRenderer != null)
        {
            matDefault = spriteRenderer.material;
        }

        animator = gameObject.GetComponent<Animator>();
    }

    public void SetColor(Color color)
    {
        healthBar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();

        if(bullet != null)
        {
            TakeDamage((int)bullet.damageAmount);
        }
    }

    void ResetMaterial()
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.material = matDefault;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        health -= .05f;
        SetSize(health);

        if(spriteRenderer != null)
        {
            spriteRenderer.material = matWhite;
        }

        if (health <= .5f && animator != null)
        {
            if (!animator.GetBool("IsEnraged"))
            {
                GetComponent<Animator>().SetBool("IsEnraged", true);
                ChangeMusic();
            }
        }

        if (health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", 0.1f);
        }
    }

    public void ChangeMusic()
    {
        AudioManager.Instance.PlayMusicWithCrossFade(secondaryMusic, 3);
    }

    private void Die()
    {
        isInvulnerable = true;
       
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        GameObject explosion = Instantiate(explosionRef);
        explosion.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f + transform.position.z);
        Destroy(gameObject, 1f);
        CursorController.instance.ActivateBullseyeClear();
        string name = SceneManager.GetActiveScene().name.Substring(0, SceneManager.GetActiveScene().name.Length - 1);
        SaveController.Instance.SaveLevel(name, new int[6], true, true);
        Loader.Load(scene);
    }

    void SetSize(float sizeNormalized)
    {
        healthBar.localScale = new Vector3(sizeNormalized, 1f);
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
