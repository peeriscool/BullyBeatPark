using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace FSMExample
{
    public delegate bool Condition();

    public class Transition
    {
        public System.Type target;
        public Condition condition;
        public Transition(System.Type _target, Condition _condition)
        {
            target = _target;
            condition = _condition;
        }
    }

    public class FSM
    {
        IState currentState;
        IActor owner;

        public FSM(System.Type _initialState, IActor _owner)
        {
            owner = _owner;
            SetState(StatePool.GetState(_initialState));
        }

        private void SetState(IState _newState)
        {
            //clean up
            if (currentState != null)
            {
                currentState.Complete();
                currentState.IsActive = false;
            }

            //init
            _newState.IsActive = true;
            _newState.Start(owner);

            //switch
            currentState = _newState;
        }

        public void Update()
        {
            //early return
            if (currentState == null) return;

            //update state
            currentState.Run();

            foreach (Transition t in currentState.Transitions)
            {
                if (t.condition())
                {
                    SetState(StatePool.GetState(t.target));
                }
            }
        }
    }
}