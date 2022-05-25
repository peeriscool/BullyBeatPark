using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Timers;
using System;

public class EnvoirmentIdle : State
{
    // protected StateMachine owner;
    //  public event EventHandler Tick;

     static int localTime = 0;
    static int MonoTime = 0;
    int FpsToSec = 50;
    int MonoSeconds = 0;
    int eventtime = 10;

    public EnvoirmentIdle(StateMachine owner)
    {
        this.owner = owner;
    }
    public override void OnEnter()
    {
        //basicly void start()

        /*
         * wat moet de envoirment allemaal doen:
         * - stalactieten laten vallen
         * - camera shake * cave status(gebaseerd op tijd)
          */
        

        LevelActiveTimer(); //starts the timer of level duration
    }
    public override void OnUpdate()
    {
        //  Debug.Log("Update started");

        MonoTime += 1;
        if(MonoTime == FpsToSec)
        {
            Debug.Log(MonoSeconds);
            FpsToSec+=50;
            MonoSeconds++;

            //TODO: spawn stalactieten elke seconden
        }
        if(MonoSeconds == eventtime)
        {
            //TODO: add camera shake
            eventtime += 10; //next event
        }
    }
    public override void OnExit()
    {

    }

    public void LevelActiveTimer() //creates a timer that is independend of monobehavior update
    {
        Timer timer1 = new Timer
        {
            Interval = 1000
        };
        // timer1 = new System.Timers.Timer(1000); for Pc Time
        timer1 = new Timer();
        timer1.Elapsed += OnTimerEvent; //happends every second
        timer1.Enabled = true;
    }

    public static void OnTimerEvent(object source, EventArgs e)
    {
        localTime += 1;
        //System.Random a = new System.Random();
       // Debug.Log(localTime);
    }
}
