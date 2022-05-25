using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Timers;
using System;

public class DefaultState : State
{
    // protected StateMachine owner;
   //  public event EventHandler Tick;
    public DefaultState(StateMachine owner)
    {
        this.owner = owner;
    }


public void init(StateMachine owner)
    {
        this.owner = owner;
    }

    public override void OnEnter()
    {

    }
    public override void OnUpdate()
    {
        Debug.Log("Update started");
      //  InitTimer();


    }
    public override void OnExit()
    {

    }

    //public void InitTimer()
    //{
    //    Timer timer1 = new Timer
    //    {
    //        Interval = 2000
    //    };
    //    timer1 = new System.Timers.Timer(2000);
    //    timer1.Elapsed += OnTimerEvent;
    //    timer1.Enabled = true;
    //}

    //public static void OnTimerEvent(object source, EventArgs e)
    //{ 
    //   System.Random a = new System.Random();
    //    Debug.Log(a);
    //}
}
