using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FSMOwner : MonoBehaviour
{
    public GameObject Playerrefrence; //should be vector3from blackboard instead of GameObj from inspector
    public Animator begeleider;
    Begeleiderstate State1;
   // agentState agenstate;
    public ScriptableEnemies smartenemy;
    StateMachine voorbeeldMachine;
    public int Eventtime;
    public RoomDungeonGenerator myenvoirment;
    [Range(0.01f, 1)]
    public float speedparameter;
    // Cell[,] leveldata = new Cell[10, 10];
    void Start()
    {
       // agenstate = new agentState(smartenemy);
        State1 = new Begeleiderstate(begeleider,Eventtime, Playerrefrence, this.gameObject, speedparameter, myenvoirment.GridHeight, myenvoirment.GridWidth); //create states 
        voorbeeldMachine = new StateMachine(State1); //create statemachine
        voorbeeldMachine.OnStart(State1);            //parse states to machine
      //  voorbeeldMachine.AddState(agenstate);

        //for (int x = 0; x < 10; x++) //generates grid with full walls : Size = width,height
        //{
        //    for (int y = 0; y < 10; y++)
        //    {
        //        leveldata[x, y] = new Cell();
        //        leveldata[x, y].gridPosition = new Vector2Int(x, y);
        //        leveldata[x, y].walls = Wall.DOWN | Wall.LEFT | Wall.RIGHT | Wall.UP;
        //    }
        //}
    }
    void Update() //50 ticks a sec
    {
        
        if (voorbeeldMachine.currentstate.status == true)
        {
            voorbeeldMachine.OnUpdate();
        }
            if (voorbeeldMachine.currentstate.status == false)
            {
            Debug.Log("Exit loop");
            State1.OnExit();
            // voorbeeldMachine.gotostate(agenstate);
            //agenstate.OnEnter();
            // intro = true;
            // voorbeeldMachine.StateUpdate();
        }
        //if (voorbeeldMachine.currentstate.Equals(agenstate))
        //{
        //    voorbeeldMachine.OnUpdate();
        //}
        //if(Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    i++;
        //    agenstate.givelocation(leveldata[i,i].gridPosition, leveldata);
        //}
        if (Keyboard.current.aKey.wasPressedThisFrame == true)
        {
            //update for state machine
            Playerrefrence.transform.position = new Vector3(Random.Range(0, 10),0, Random.Range(0, 10));
           // voorbeeldMachine.AddState(State1);
            State1.UpdateTarget(Playerrefrence.transform.position);
            voorbeeldMachine.OnStart(State1);
        }

    }

    public static Cell[,] FillArray(Cell[,] array)
    {
        Cell data = new Cell();
        //System.Random rnd = new System.Random();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                data.gridPosition = new Vector2Int(i,j);
                array[i, j] = data; //rnd Next(1, 15)
            }
        }
        return array;
    }
}
