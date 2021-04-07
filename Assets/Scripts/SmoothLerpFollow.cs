using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLerpFollow : MonoBehaviour
{
    // Start is called before the first frame update\

    public Transform follow;
    Transform me;
    int Tijme = 1;
    Transform local;
    void Start()
    {
        me = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        me = this.gameObject.transform;
      
        //Tijme++;
        //if (Tijme > 10)
        //{
            this.transform.position = Vector3.MoveTowards(me.position,new Vector3( Mathf.Lerp(follow.position.x, me.position.x, Time.deltaTime), Mathf.Lerp(follow.position.y, me.position.y, Time.deltaTime), Mathf.Lerp(follow.position.z, me.position.z, Time.deltaTime)),0.3f);
          //  Tijme = 1;
       // }
       
    }
}
