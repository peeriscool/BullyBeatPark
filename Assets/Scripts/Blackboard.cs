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
        enemy = selected;
        
    }
    public static void EnemylocationPing(GameObject enemy)
    {
        Debug.Log(enemy.transform.position);
    }
    }
