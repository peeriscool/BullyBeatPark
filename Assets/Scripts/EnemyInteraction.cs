using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyInteraction : MonoBehaviour
{
    //"come over here"
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.xKey.wasPressedThisFrame)
        {
            Blackboard.Interactionrequest("come over here");
        }
    }
}
