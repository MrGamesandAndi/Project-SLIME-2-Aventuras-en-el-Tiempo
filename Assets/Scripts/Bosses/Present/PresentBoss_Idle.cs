using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentBoss_Idle : StateMachineBehaviour
{
    public float timer = .5f;
    private float totalTimer;
    private RoblocksController roblocksController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        roblocksController = animator.GetComponent<RoblocksController>();
        totalTimer = timer;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            animator.SetTrigger("Jump");
            timer = totalTimer;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Land");
    }
}
