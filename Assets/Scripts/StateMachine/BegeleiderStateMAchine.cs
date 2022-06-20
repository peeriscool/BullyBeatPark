using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BegeleiderStateMAchine : MonoBehaviour
{
    public GameObject Playerrefrence; //should be vector3from blackboard instead of GameObj from inspector
    public Animator begeleider;
    // public ScriptableEnemies smartenemy;
    public int Eventtime;
    [Range(0.01f, 1)]
    public float speedparameter;
    public RoomDungeonGenerator myenvoirment;
    List<Room> RoomList;
    Dictionary<int, List<Vector3Int>> playarea = new Dictionary<int, List<Vector3Int>>();
    Begeleiderstate GoToPlayer;
    StateMachine ActionMachine;
    Followplayerstate followstate;
    int roomindex = 0;
    int length = 0; //length for tiles in a room
    void Start()
    {
        GoToPlayer = new Begeleiderstate(begeleider, Eventtime, Playerrefrence.transform.position, this.gameObject, speedparameter, myenvoirment.GridHeight, myenvoirment.GridWidth); //create states 
        //followstate = new Followplayerstate();
        ActionMachine = new StateMachine(GoToPlayer); //create statemachine
        ActionMachine.OnStart(GoToPlayer);            //parse states to machine
        useEnvoirment();
    }
    void useEnvoirment()
    {
        RoomList = myenvoirment.RoomList;

        foreach (Room r in RoomList) //for every room add known locations to dictionary
        {
            List<Vector3Int> roomdata = r.mypositions;
            playarea.Add(r.Roomindex, roomdata);
        }
        ///parse RoomList to begeleiderstate
     //   GoToPlayer.SetRoomAstar(myenvoirment.GridWidth, myenvoirment.GridHeight);
        GoToPlayer.SetdungeonAstar(playarea);
        //set target and agent on the first tile of the dungeon
        this.transform.position = RoomList[0].mypositions[0];
   //     Playerrefrence.transform.position = RoomList[0].mypositions[0];
    }

    private float nextUpdate = 0.1f;
    void Update() //50 ticks a sec
    {
        if (ActionMachine.currentstate.status == true)
        {
            ActionMachine.OnUpdate();
        }

        if (ActionMachine.currentstate.status == false)
        {
            Debug.Log("Exit loop");
            GoToPlayer.OnExit();
       //     ActionMachine.AddState();
        }

        if (Time.time >= nextUpdate)
        {
            // Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // update number
            length += 1;
        }
        UpdateTarget(RoomList[roomindex].mypositions[1].x, RoomList[roomindex].mypositions[RoomList[roomindex].mypositions.Count - 1].x); //update  
      //  Targetplayer();
    }

    /// <summary>
    /// Gives the target a random new location in a given area
    /// </summary>
    public void UpdateTarget(int min, int max)
    {
        //check if there are still spaces in current room else move to next room
        if (length == RoomList[roomindex].mypositions.Count || length >= RoomList[roomindex].mypositions.Count)
        {
            roomindex += 1;
            this.gameObject.transform.position = RoomList[roomindex].mypositions[0];
            length = 0;
        }
        if (roomindex == RoomList.Count || roomindex >= RoomList.Count) { roomindex = 0; } //loop roomindex
        Vector3Int command = RoomList[roomindex].mypositions[length]; //walk to the entrence of a room
        GoToPlayer.UpdateTarget(command);
        ActionMachine.OnStart(GoToPlayer);

    }
    public void Targetplayer(Vector3Int command)
    {
          //  Vector3Int command =  new Vector3Int(Blackboard.moves[0].x, 0, Blackboard.moves[0].y);
            GoToPlayer.UpdateTarget(command);
            ActionMachine.OnStart(GoToPlayer);
     
    }
}
