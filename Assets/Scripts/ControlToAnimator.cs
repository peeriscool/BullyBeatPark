using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ControlToAnimator : MonoBehaviour
{
    public Animator linkedRig;
    private string currentState;
    AnimatorControllerParameter[] a;
    // Start is called before the first frame update
    void Start()
    {
        //linkedRig.runtimeAnimatorController.animationClips()
        //foreach (var item in linkedRig.parameters)
        //{
        //    Debug.Log(item.name);
        //}

        a = linkedRig.parameters;
    }
    private void Update()
    {
        keycheck();
    }
    private void FixedUpdate()
    {
     if(linkedRig.GetCurrentAnimatorStateInfo(0).IsName("WalkingInPlace"))
        {
            Debug.Log(linkedRig.GetCurrentAnimatorStateInfo(0).IsName("WalkingInPlace"));
            //linkedRig. .Play("mesh_001|walk");
            linkedRig.GetCurrentAnimatorStateInfo(0).Equals("WalkingInPlace");
        }
    }
    // Update is called once per frame
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        linkedRig.Play(newState);
        currentState = newState;
    }
    void keycheck()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            ChangeAnimationState("walkingInPlace");
            //linkedRig.Play("mesh_001|walk");
            linkedRig.SetTrigger("Walk") ;
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            linkedRig.ResetTrigger("Walk");
        }

        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            ChangeAnimationState("WalkingInPlace_alt");
            //set walk speed to x2
            linkedRig.SetTrigger("Run");
            linkedRig.speed = 2;
        }
        if (Keyboard.current.shiftKey.wasReleasedThisFrame)
        {
            linkedRig.speed = 1;
            linkedRig.ResetTrigger("Run");
            
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ChangeAnimationState("StandingJump");
           // linkedRig.Play("mesh_001|Dive and get up");
            linkedRig.SetTrigger("Dive");
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            linkedRig.ResetTrigger("Dive");
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            ChangeAnimationState("hook");
           // linkedRig.Play("mesh_001|Zombie_Punch");
            linkedRig.SetTrigger("punch");
            //punch
        }
        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            linkedRig.ResetTrigger("punch");
        }
    }
}
