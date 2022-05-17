using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyInteraction : MonoBehaviour
{
    public BoxCollider PlayerRange;
    private bool Inrange;

    void Update()
    {
        if (Inrange)
        {
            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
            //    Blackboard.Interactionrequest("MarkEnemy");
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
          //      Blackboard.Interactionrequest("Hit");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Inrange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Inrange = false;
    }
}
