using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BegeleiderStateMAchine : MonoBehaviour
{
    public GameObject Playerrefrence; //should be vector3from blackboard instead of GameObj from inspector
    public Animator begeleider;
    public ScriptableEnemies smartenemy;
    public int Eventtime;
    [Range(0.01f, 1)]
    public float speedparameter;
    public RoomDungeonGenerator myenvoirment;
    List<Room> RoomList;
    List<Vector3Int> playarea = new List<Vector3Int>();
    Begeleiderstate GoToPlayer;
    StateMachine ActionMachine;
    int roomindex = 1;
    void Start()
    {
        GoToPlayer = new Begeleiderstate(begeleider,Eventtime, Playerrefrence, this.gameObject, speedparameter,myenvoirment.GridHeight, myenvoirment.GridWidth); //create states 
        ActionMachine = new StateMachine(GoToPlayer); //create statemachine
        ActionMachine.OnStart(GoToPlayer);            //parse states to machine
        useEnvoirment();
    }
    void useEnvoirment()
    {
      RoomList = myenvoirment.RoomList;
        foreach (Room r in RoomList) //for every room
        {
            for (int i = 0; i < r.mypositions.Count; i++) //for all known positions in the room
            {
                playarea.Add(r.mypositions[i]); //add pos at index
                if (i == 0) //get first cell 
                {
                //enter room
                }
                else if (i == r.mypositions.Count - 1) //get last cell
                {
                //exit room
                }
            }
        }
        ///parse RoomList ro begeleiderstate
        GoToPlayer.SetRoomAstar(RoomList[roomindex].maxX, RoomList[roomindex].maxZ);
    }
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
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame) //set room 
        {
            roomindex = +1;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame) //set location in room
        {
            //List<Vector3Int> playarea;
            if (roomindex == RoomList.Count) { roomindex = 1; } //loop roomindex
            GoToPlayer.SetRoomAstar(RoomList[roomindex].maxX, RoomList[roomindex].maxZ);
            UpdateTarget(RoomList[roomindex].mypositions[1].x, RoomList[roomindex].mypositions[RoomList[roomindex].mypositions.Count-1].y);
            //for (int i = 0; i < RoomList[roomindex].mypositions.Count; i++)
            //{
            //  }
            //UpdateTarget(RoomList[1].mypositions,);
            //WalkTo(new Vector3(Random.Range(0, 10), Random.Range(0, 10)), location, Astarcell);
        }
    }
    /// <summary>
    /// Gives the target a random new location in a given area
    /// </summary>
    public void UpdateTarget(int min,int max)
    {
        //TODO:should check if set position is within level

        //if(myenvoirment.Roomcheck(RoomList[roomindex])) //room is in dungeon
        // {
        Vector3Int command = new Vector3Int((int)Random.Range(min, max), 0, (int)Random.Range(min, max));
      
        if (playarea.Contains(command))
        {
            Playerrefrence.transform.position = command;
            GoToPlayer.UpdateTarget(Playerrefrence.transform.position);
            ActionMachine.OnStart(GoToPlayer);
        }
        
       // }
        
    }
}
