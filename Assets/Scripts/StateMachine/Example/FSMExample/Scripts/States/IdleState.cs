using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMExample
{
    public class IdleState : AState
    {
        public IdleState()
        {
            Transitions = new List<Transition>();
            Transitions.Add(new Transition(typeof(WalkState), () => Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0));
        }

        public override void Start(IActor _actor)
        {
            Debug.Log("IDLE");
            base.Start(_actor);
        }
    }
}