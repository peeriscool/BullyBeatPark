using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentState : State
{
    SmartAgent Smartagent;
    GameObject begeleiderprefab;
    //public Vector2Int loc;
    public agentState(ScriptableEnemies begeleider)
    {
        begeleiderprefab = begeleider.Prefab;
        Smartagent = begeleiderprefab.GetComponent<SmartAgent>();
        //loc = Smartagent.location;
    }
    public override void OnEnter()
    {
        Debug.Log("agent enter");
    }

    public override void OnExit()
    {
        Debug.Log("agent exit");
    }

    public override void OnUpdate()
    {
        Debug.Log("agent update");
        Smartagent.Tick();
    }
    public void givelocation( Vector2Int endPos, Cell[,] grid)
    {
        // Smartagent.FindPathToTarget(startPos,endPos,grid);
        Smartagent.WalkTo(new Vector2Int(9,9), endPos, grid);
     //   Smartagent.WalkTo(Smartagent.location, endPos, grid);
    }
}
