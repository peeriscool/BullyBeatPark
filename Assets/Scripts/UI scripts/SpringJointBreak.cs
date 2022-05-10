using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJointBreak : MonoBehaviour //if joint brakes enable canvas
{
    public bool useblackboard;
    void OnJointBreak(float breakForce)
    {
        Debug.Log("Joint Broke!, force: " + breakForce);
        if(useblackboard)
        {
            Blackboard.UiHintToggle = false;
        }
    }
}
