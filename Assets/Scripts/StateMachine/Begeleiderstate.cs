using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Timers;
using System;

public class Begeleiderstate : State
{
    static int localTime = 0;
    static int MonoTime = 0;
    int FpsToSec = 50;
    int MonoSeconds = 0;
    int eventtime = 3;
    Animator anim;
    Timer timer1 = new Timer
    {
        Interval = 1000
    };

    public Begeleiderstate(StateMachine owner, Animator _anim)
    {
        this.owner = owner;
        anim = _anim;
        this.status = true;
    }
    public override void OnEnter()
    {
        Debug.Log("begeleider awake");
        this.status = true;
        LevelActiveTimer(); //starts the timer of level duration
    }
    public override void OnUpdate()
    {
        Debug.Log("update begeleider goals");
        #region timer implementation 1 second trigger;
        MonoTime += 1;
        if (MonoTime == FpsToSec)
        {
            anim.Play("Grab");
            Debug.Log(MonoSeconds);
            FpsToSec += 50;
            MonoSeconds++;
        }
        if (MonoSeconds == eventtime) //if number is 10 disable state
        {
            this.status = false;
        }
        #endregion
    }
    public override void OnExit()
    {
        Debug.Log("begeleider did its thing");
    }

    public void LevelActiveTimer() //creates a timer that is independend of monobehavior update
    {
        timer1 = new Timer();
        timer1.Elapsed += OnTimerEvent; //happends every second
        timer1.Enabled = true;
    }

    public static void OnTimerEvent(object source, EventArgs e)
    {
        localTime += 1;
        // Debug.Log(localTime);
    }
}
