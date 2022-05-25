using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMExample
{
    public abstract class AState : IState
    {
        public List<Transition> Transitions { get; protected set; }
        public bool IsActive { get; set; }

        protected IActor activeActor;

        public virtual void Start(IActor _actor)
        {
            activeActor = _actor;
        }

        public virtual void Run()
        {

        }

        public virtual void Complete()
        {

        }
    }
}