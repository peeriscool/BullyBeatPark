using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class boxtriggerhandler : MonoBehaviour
{
    public Collider collsioninteract;
    public Canvas SetVisableWhenEntering;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        SetVisableWhenEntering.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
            if(other == collsioninteract)
        {
            Debug.Log(other.name + " has entered the dome");
            SetVisableWhenEntering.gameObject.SetActive(true);
            SetVisableWhenEntering.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        SetVisableWhenEntering.gameObject.SetActive(false);
        SetVisableWhenEntering.enabled = false;
    }
}
