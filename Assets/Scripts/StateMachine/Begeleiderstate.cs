using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Timers;
using System;

public class Begeleiderstate : State
{
    static int localTime = 0;
    static int MonoTime = 0;
    int FpsToSec = 50;
    int MonoSeconds = 0;
    int eventtime;
    Animator anim;

    public Vector3 target;
    public GameObject smartagent;
    SmartAgent guard;
    Vector2Int command;
   [Range(0.01f, 1)]
    public float speedparameter;
    Timer timer1 = new Timer
    {
        Interval = 1000
    };

    //public Begeleiderstate( Animator _anim)
    //{
    //   // this.owner = owner;
    //    anim = _anim;
    //    this.status = true;
    //}
    /// <summary>
    /// State which follows a target
    /// </summary>
    /// <param name="_anim">animator ref</param>
    /// <param name="_duration">time for this state</param>
    /// <param name="_target">what to follow</param>
    /// <param name="_agent">Who is the agent?</param>
    public Begeleiderstate(Animator _anim,int _duration,GameObject _target,GameObject _agent, float _speedparameter)
    {
        smartagent = _agent;
        target = _target.transform.position;
        eventtime = _duration;
        // this.owner = owner;
        anim = _anim;
        speedparameter = _speedparameter;
        guard = new SmartAgent(10, 10, smartagent);
    }
    public void UpdateTarget(Vector3 command)
    {
        target = command;
    }
    public override void OnEnter()
    {
        this.status = true;
        
        command = new Vector2Int((int)UnityEngine.Random.Range(smartagent.transform.position.x, target.x), (int)UnityEngine.Random.Range(smartagent.transform.position.z, target.z));
        guard.WalkTo(smartagent.transform.position,command, guard.Astarcell);
        //Debug.Log("begeleider awake");
        LevelActiveTimer(); //starts the timer of level duration
    }
    public override void OnUpdate()
    {
        guard.speed = speedparameter;
        bool walking = guard.Tick();
        if (walking)
        {
            command = new Vector2Int((int)UnityEngine.Random.Range(smartagent.transform.position.x, target.x), (int)UnityEngine.Random.Range(smartagent.transform.position.z, target.z));
            guard.WalkTo(smartagent.transform.position, command, guard.Astarcell);
            if (guard.Vector3ToVector2Int(smartagent.transform.position) == command)
            {
               // this.status = false; //destination reached}
            }
            Debug.Log(walking);
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
            if (MonoSeconds == eventtime) //if number is reached disable state
            {
                Debug.Log("END OF Begeleider timed event!");
                this.status = false;
            }
            #endregion
        }
    }
    public override void OnExit()
    {
        Debug.Log("begeleider did its thing");
        //reset command
        MonoSeconds = 0;
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
