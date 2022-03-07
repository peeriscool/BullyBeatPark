using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ControlToAnimator : MonoBehaviour
{
    public Animator linkedRig;
    AnimatorControllerParameter[] a;
    // Start is called before the first frame update
    void Start()
    {
        //foreach (var item in linkedRig.parameters)
        //{
        //    Debug.Log(item.name);
        //}

        a = linkedRig.parameters;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            //linkedRig.Play("mesh_001|walk");
            linkedRig.SetTrigger("Walk") ;
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            linkedRig.ResetTrigger("Walk");
        }

        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            //set walk speed to x2
            linkedRig.SetTrigger("Run");
        }
        if (Keyboard.current.shiftKey.wasReleasedThisFrame)
        {
            linkedRig.ResetTrigger("Run");
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            linkedRig.Play("mesh_001|Dive and get up");
            linkedRig.SetTrigger("Dive");
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            linkedRig.ResetTrigger("Dive");
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            linkedRig.Play("mesh_001|Zombie_Punch");
            linkedRig.SetTrigger("punch");
            //punch
        }
        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            linkedRig.ResetTrigger("punch");
        }


    }
}
