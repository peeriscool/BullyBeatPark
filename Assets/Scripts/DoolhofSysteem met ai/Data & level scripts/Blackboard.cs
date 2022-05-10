using System.Collections.Generic;
using UnityEngine;

public static class Blackboard
{
    public static List<GameObject> Enemies;
    public static GameObject player;

    public static float scalefactor;
    public static int width;
    public static int height;
    
    public static bool levelfinished;
    public static bool UiHintToggle = true;

    private static GameObject enemyrange;

    public static List<Vector2Int> moves;
    public static void SelectEnemy(GameObject enemy)
    {
        enemyrange = enemy;    
    }
    public static void EnemylocationPing()
    {
        Debug.Log(enemyrange.transform.position + "Is in the player range");
    }
    public static void Interactionrequest(string Command)
    {
        //to do: translate command or number to agent action
        if(Command.Equals("MarkEnemy"))
        {
            // selected.GetComponent<Agent>().WalkTo(new Vector3(0,0,0));
            enemyrange.GetComponent<Agent>().MarkEnemy();
        }
        if (Command.Equals("Hit"))
        {
            // selected.GetComponent<Agent>().WalkTo(new Vector3(0,0,0));
            enemyrange.GetComponent<Agent>().TakeDamage();
        }
    }
}
