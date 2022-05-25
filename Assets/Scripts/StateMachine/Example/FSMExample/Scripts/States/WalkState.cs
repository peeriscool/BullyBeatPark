using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMExample
{
    public class WalkState : AState
    {
        public WalkState()
        {
            Transitions = new List<Transition>();
            Transitions.Add(new Transition(typeof(IdleState), () => Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0));
        }

        public override void Start(IActor _actor)
        {
            Debug.Log("WALK");
            base.Start(_actor);
        }

        public override void Run()
        {
            activeActor.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * activeActor.speed, 0, Input.GetAxis("Vertical") * Time.deltaTime * activeActor.speed);
        }
    }
}