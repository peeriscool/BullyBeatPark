using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class boxtriggerCanvashandler : MonoBehaviour //canvas triggering script including camera enable disable
{
    public Camera UseDiffrentCam;
    Camera current;
    public Collider collsioninteract;
    public Canvas SetVisableWhenEntering;

    void Start()
    {
        current = Camera.main;
        if (collsioninteract == null)
        {
            collsioninteract = Blackboard.player.GetComponent<Collider>();
                
        }
        this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        SetVisableWhenEntering.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
            if(other == collsioninteract)
            {
            Debug.Log(other.name + " has entered the dome");
            SetVisableWhenEntering.gameObject.SetActive(true);
            SetVisableWhenEntering.enabled = true;

            if (UseDiffrentCam != null)
            {
                current.enabled = false;
                UseDiffrentCam.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other = collsioninteract)
        {
            SetVisableWhenEntering.gameObject.SetActive(false);
            SetVisableWhenEntering.enabled = false;
            if (UseDiffrentCam != null)
            {
                current.enabled = true;
                UseDiffrentCam.enabled = false;
            }
        }
       
    }
}
