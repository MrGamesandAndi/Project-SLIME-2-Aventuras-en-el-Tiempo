using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour, IShopCustomer
{
    public GameObject shield;
    public AudioClip jumpSfx;
    public float invulnerabilityTime = 3f;

    public float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    [HideInInspector] public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public HealthBarFade healthBarFade;
    private GameController gameController;
    private WeaponSwitching weaponSwitching;


    public ParticleSystem dust;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        weaponSwitching = GameObject.Find("Weapon Holder").GetComponent<WeaponSwitching>();
        healthBarFade = GameObject.Find("Life").GetComponent<HealthBarFade>();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if(move != 0 && m_Grounded)
            {
                CreateDust();
            }
            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            CreateDust();
            // Add a vertical force to the player.
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            AudioManager.Instance.PlaySfx(jumpSfx);
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void CreateDust()
    {
        dust.Play();
    }

    public void DamageKnockback(int damageAmount)
    {
        shield.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("GhostPlayer");
        healthBarFade.Damage(damageAmount);
        StartCoroutine("Invencibility");
    }

    IEnumerator Invencibility()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        shield.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought item:" + itemType);
        switch (itemType)
        {
            case Item.ItemType.HealthPotion:
                gameController.CollectSlime("Health", 1);
                break;
            case Item.ItemType.FireBullet:
                weaponSwitching.bullets.Add(gameController.availableShopItems[0]);
                gameController.CollectSlime("Fire", 10);
                break;
            case Item.ItemType.WaterBullet:
                weaponSwitching.bullets.Add(gameController.availableShopItems[1]);
                gameController.CollectSlime("Water", 10);
                break;
            case Item.ItemType.AcidBullet:
                weaponSwitching.bullets.Add(gameController.availableShopItems[2]);
                gameController.CollectSlime("Acid", 10);
                break;
            default:
                break;
        }
    }

    public bool TrySpendAtomAmount(int spendAtomAmount)
    {
        if(gameController.GetAmount("Atom") > spendAtomAmount )
        {
            gameController.UseSlime("Atom", spendAtomAmount);
            gameController.SetShopDialog("Gracias por su compra Doctor.");
            return true;
        }
        else
        {
            gameController.SetShopDialog("Creo que no tiene los suficientes atomos Doctor.");
            return false;
        }
    }

    public void Heal()
    {
        if(gameController.GetAmount("Health") > 0)
        {
            healthBarFade.Heal(30);
            gameController.UseSlime("Health", 1);
        }
    }
}