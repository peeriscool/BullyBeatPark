using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentstate;
    private Dictionary<System.Type, State> states = new Dictionary<System.Type, State>(); //list of availible states

   // StateMachine owner;
    public StateMachine(State basestate) //constructer for state machine
    {
        //geef states mee aan de statemachine 
        //maak een instance aan, run update vanuit monobehavior
        //  this.owner = owner;
        AddState(basestate);
    }

    public void OnStart(State state) //init a state
    {      
        currentstate = states[state.GetType()]; //assign given state as first state
        currentstate.OnEnter();
    }
    public void OnUpdate() //get update method from a state
    {
        currentstate?.OnUpdate();
    }
    public void AddState(State state)
    {
        states.Add(state.GetType(), state); //putting state in dictonary
        Debug.Log(state + " added to " + states.ToString());
    }
    public void gotostate(State state)
    {
        currentstate = state;
    }
    public void StateUpdate()
    {
        //early return
        if (currentstate == null) return;
        //bool status = owner.currentstate.status(); //listens to the state event status

        //update state
        //currentstate.Run();

        //foreach (currentstate t in currentstate.Transitions)
        //{
        //    if (t.condition())
        //    {
        //        SetState(StatePool.GetState(t.target));
        //    }
        //}
    }
}

