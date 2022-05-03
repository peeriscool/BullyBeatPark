using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class esccapemenu : MonoBehaviour
{
    private bool InUi = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            if (!InUi)
            { SceneManagerScript.AppendScene("ControlsExplination"); InUi = true; }
            if (InUi)
            { SceneManagerScript.DeppendScene("ControlsExplination"); InUi = false; }
            //call the controls menu
        }
    }
}
