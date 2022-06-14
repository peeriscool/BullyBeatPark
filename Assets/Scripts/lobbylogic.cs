using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobbylogic : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Blackboard.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
