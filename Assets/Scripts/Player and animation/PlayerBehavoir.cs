using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerBehavoir : MonoBehaviour
{
    public List<GameObject> essentails;
    bool ingame = false;
    void Start()
    {
        Blackboard.player = this.gameObject;
        foreach (GameObject item in essentails)
        {
            DontDestroyOnLoad(item);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // TODO punch behavior
    private void Update()
    {
    }
    public void ereaseplayer()
    {
        //to do save inventory
        foreach (GameObject item in essentails)
        {
            Destroy(item);
        }
        Destroy(this.gameObject);
    }
 
}
