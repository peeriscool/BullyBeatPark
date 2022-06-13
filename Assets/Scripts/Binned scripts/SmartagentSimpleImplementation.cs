using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartagentSimpleImplementation : MonoBehaviour
{
    public GameObject target;
    public GameObject smartagent;
    SmartAgent guard;
    [Range(0.01f, 1)]
    public float speedparameter;
    // Start is called before the first frame update
    void Start()
    {
        guard = new SmartAgent(10,10, smartagent);
        guard.WalkTo(smartagent.transform.position, new Vector2Int((int)Random.Range(0, 10), (int)Random.Range(0, 10)), guard.Astarcell);
    }
    void Update()
    {
        guard.speed = speedparameter;
        bool walking = guard.Tick();
        if (walking)
        {
            guard.WalkTo(smartagent.transform.position, new Vector2Int((int)Random.Range(smartagent.transform.position.x, target.transform.position.x), (int)Random.Range(smartagent.transform.position.z, target.transform.position.z)), guard.Astarcell);
        }
        Debug.Log(walking);
    }
}
