using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class boxtriggerhandler : MonoBehaviour
{
    public Collider collsioninteract;
    public Canvas SetVisableWhenEntering;
    public bool UseCountdown;

    // Start is called before the first frame update
    void Start()
    {
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
            if(UseCountdown)
            {
                countdown instance = SetVisableWhenEntering.GetComponent<countdown>();
                // instance.OnEnable();
                instance.callfortimer(5);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        SetVisableWhenEntering.gameObject.SetActive(false);
        SetVisableWhenEntering.enabled = false;
    }
}
