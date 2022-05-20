using System.Collections.Generic;
using UnityEngine;

public static class Blackboard
{
    public static List<GameObject> Enemies;
    public static GameObject player;
    public static int Mazewidth;
    public static int Mazeheight;
    public static List<Vector2Int> moves;
    public static int maxmoves;
    private static GameObject selectedenemy;
}
