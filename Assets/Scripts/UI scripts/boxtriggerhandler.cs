using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class boxtriggerhandler : MonoBehaviour //level triggering script communicating with countdown
{
    public Collider collsioninteract;
    public Canvas SetVisableWhenEntering;
    public bool UseCountdown;
    public bool deleteplayer;


    // Start is called before the first frame update
    void Start()
    {
        if(collsioninteract == null)
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
            if(UseCountdown)
            {
            ;
                if(deleteplayer)
                {
                    //to do save inventory
                    foreach (GameObject item in Blackboard.player.GetComponent<PlayerBehavoir>().essentails)
                    {
                        Destroy(item);
                    }
                    Destroy(Blackboard.player.gameObject);

                }
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
