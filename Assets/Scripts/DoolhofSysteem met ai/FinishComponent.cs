using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Blackboard.levelfinished = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Blackboard.levelfinished = false;
        }
    }
}
