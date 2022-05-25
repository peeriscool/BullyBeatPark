using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMExample
{
    public class Player : MonoBehaviour, IActor
    {
        public float speed => 10;

        private Transform myTransform;
        private FSM myFSM;

        private void Awake()
        {
            myFSM = new FSM(typeof(IdleState), this);
        }

        private void Start() 
        {
            myTransform = transform;
        }

        // Update is called once per frame
        private void Update() 
        {
            myFSM.Update();
        }
    }
}