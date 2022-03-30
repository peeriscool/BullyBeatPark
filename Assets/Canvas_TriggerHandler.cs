using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_TriggerHandler : MonoBehaviour
{


    void Update()
    {
       if( Blackboard.UiHintToggle == false)
        {
            Debug.Log("kill canvas");
            Destroy(this.gameObject);
        }
    }
}
