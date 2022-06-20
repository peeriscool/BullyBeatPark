using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followplayerstate : State
{
    SmartAgent guard;
    Followplayerstate()
    { }
    /// <summary>
    /// initialize a state of following an object
    /// </summary>
    /// <param name="Followlocation"></param>
    public Followplayerstate(Vector3 Followlocation, SmartAgent _guard)
    {
        guard = _guard;
    }
    public override void OnEnter()
    {
        this.status = true;
        
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        guard.Tick();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
//public class Begeleiderstate : State
//{
//    //static int localTime = 0;
//    //static int MonoTime = 0;
//    //int FpsToSec = 50;
//    //int MonoSeconds = 0;
//    int eventtime;
//    Animator anim;

//    public Vector3 target;
//    public GameObject smartagent; //not sure if the state needs to know the gameobject or can just move this.gameobject?
//    SmartAgent guard;
//    Vector2Int command;
//   [Range(0.01f, 1)]
//    public float speedparameter;
//    Timer timer1 = new Timer
//    {
//        Interval = 1000
//    };
//    /// <summary>
//    /// State which follows a target
//    /// </summary>
//    /// <param name="_anim">animator ref</param>
//    /// <param name="_duration">time for this state</param>
//    /// <param name="_target">what to follow</param>
//    /// <param name="_agent">Who is the agent?</param>
//    /// <param name="speed">object speed</param>
//    /// <param name="width">Astar and cell size</param>
//    /// <param name="height">Astar and cell size</param>
//    public Begeleiderstate(Animator _anim,int _duration, Vector3 _target,GameObject _agent, float _speedparameter,int width,int height)
//    {
//        smartagent = _agent;
//        target = _target;
//        eventtime = _duration;
//        anim = _anim;
//        speedparameter = _speedparameter;
//        guard = new SmartAgent(width, height, smartagent);
//    }
//    public void SetRoomAstar(int width,int height)
//    {
//        guard.makecells(width, height);
//    }
//    public void SetdungeonAstar(Dictionary<int, List<Vector3Int>> playerea)
//    {
//        guard.roomcells(playerea);
//    }
//    public void UpdateTarget(Vector3 command) //set target position
//    {
//        target = command;
//    }
//    public override void OnEnter()
//    {
//        this.status = true;
//        command = new Vector2Int((int)target.x, (int)target.z);
//        //command = new Vector2Int((int)UnityEngine.Random.Range(smartagent.transform.position.x, target.x), (int)UnityEngine.Random.Range(smartagent.transform.position.z, target.z));
//        guard.WalkTo(smartagent.transform.position,command, guard.Astarcell);
//        LevelActiveTimer(); //starts the timer of level duration
//    }
//    public override void OnUpdate()
//    {
//        guard.speed = speedparameter;
//        bool walking = guard.Tick();
//        if (walking)
//        {
//            command = new Vector2Int((int)UnityEngine.Random.Range(smartagent.transform.position.x, target.x), (int)UnityEngine.Random.Range(smartagent.transform.position.z, target.z));
//            guard.WalkTo(smartagent.transform.position, command, guard.Astarcell);
//            if (guard.Vector3ToVector2Int(smartagent.transform.position) == command)
//            {
//               // this.status = false; //destination reached}
//            }
//            Debug.Log(walking);
//            Debug.Log("update begeleider goals");
//            //#region timer implementation 1 second trigger;
//            //MonoTime += 1;
//            //if (MonoTime == FpsToSec)
//            //{
//            //    anim.Play("Grab");
//            //    Debug.Log(MonoSeconds);
//            //    FpsToSec += 50;
//            //    MonoSeconds++;
//            //}
//            //if (MonoSeconds == eventtime) //if number is reached disable state
//            //{
//            //    Debug.Log("END OF Begeleider timed event!");
//            //    this.status = false;
//            //}
//            //#endregion
//        }
//    }
//    public override void OnExit()
//    {
//        Debug.Log("begeleider did its thing");
//        //reset command
//      //  MonoSeconds = 0;
//    }

//    public void LevelActiveTimer() //creates a timer that is independend of monobehavior update
//    {
//        timer1 = new Timer();
//        timer1.Elapsed += OnTimerEvent; //happends every second
//        timer1.Enabled = true;
//    }

//    public static void OnTimerEvent(object source, EventArgs e)
//    {
//       // localTime += 1;
//        // Debug.Log(localTime);
//    }
//}
