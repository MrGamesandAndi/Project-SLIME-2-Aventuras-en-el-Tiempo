using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public Animator animator;
    public bool jump = false;
    public bool crouch = false;

    private float horizontalMove = 0f;
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }


    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            weapon.enabled = false;
            animator.SetBool("IsJumping", true);
            jump = true;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            controller.Heal();
        }
    }

    public void OnLanding()
    {
        weapon.enabled = true;
        animator.SetBool("IsJumping", false);
        controller.CreateDust();
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
