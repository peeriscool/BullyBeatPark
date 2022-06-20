using System.Collections.Generic;
using UnityEngine;

public static class Blackboard
{
    public static List<GameObject> Enemies; //childeren in the current level
    public static GameObject player; //ref to player
    public static int Mazewidth;
    public static int Mazeheight;
    public static List<Vector2Int> moves; //the steps the player set
    public static int maxmoves;
    public static bool playerpunch;
  //  private static GameObject selectedenemy;
    private static Dictionary<int, bool> levels;

    public static void generateleveldict()
    {
        levels = new Dictionary<int, bool>();
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
        {
            levels.Add(i, false);
        }
    }
        public static Dictionary<int,bool> getleveldict()
    {
        return levels;
    }
    public static void setlevelstatus(int index, bool status)
    {
        Debug.Log("setting level: " + index + "to " + status);
         levels[index] = status;
    }
    public static bool getlevelstatus(int index)
    {
        return levels[index];
    }
}
