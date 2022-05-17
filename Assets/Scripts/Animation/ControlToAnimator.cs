using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ControlToAnimator
{
    public Animator linkedRig;
    private string currentState;
    public ControlToAnimator(Animator _playerController)
    {
        linkedRig = _playerController;
    }
    public void Tick()
    {
        keycheck();
        Diagnalcheck();
    }

    void ChangeAnimationState(string newState) //Keycheck uses this for linking back to the Animator component
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
            linkedRig.SetTrigger("punch");
            //punch
        }
        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            linkedRig.ResetTrigger("punch");
        }
    }
    void Diagnalcheck()
    {
        if (Keyboard.current.wKey.isPressed && Keyboard.current.aKey.isPressed)
        {
          //  Debug.Log("front left ");
            //diagonal forward
            ChangeAnimationState("diagonal forward");
        }
        if (Keyboard.current.wKey.isPressed && Keyboard.current.dKey.isPressed)
        {
          //  Debug.Log("front right");
            ChangeAnimationState("diagonal forward 0");
        }

        if (Keyboard.current.sKey.isPressed &&Keyboard.current.aKey.isPressed)
        {
          //  Debug.Log("left back");
            //diagonal backwards
            ChangeAnimationState("diagonal forward");
        }
        if (Keyboard.current.sKey.isPressed && Keyboard.current.dKey.isPressed)
        {
          //  Debug.Log("right back ");
            ChangeAnimationState("diagonal forward 0");
            //linkedRig.
      
        }
       
    }
}
