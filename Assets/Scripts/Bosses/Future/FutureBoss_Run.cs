using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureBoss_Run : StateMachineBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speed = animator.GetComponent<FutureBossController>().speed;
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, 0);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
