using System.Collections.Generic;
using UnityEngine;

public static class Blackboard
{
    public static float scalefactor;
    public static List<GameObject> Enemies;
    public static int width;
    public static int height;
    private static GameObject selected;


    public static void SelectEnemy(GameObject enemy)
    {
        selected = enemy;
        
    }
    public static void EnemylocationPing()
    {
        Debug.Log(selected.transform.position);
    }
    public static void Interactionrequest(string Command)
    {
        //to do: translate command or number to agent action
        if(Command.Equals("come over here"))
        {
            selected.GetComponent<Agent>().WalkTo(new Vector3(0,0,0));
        }
    }
}
