using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager
{
    public List<GameObject> EnemyModels;
    //Enemies.EnemyOne one;
    public Enemy_Manager(List<GameObject> modeles)
    {
        EnemyModels = modeles;
    }
   
    public List<GameObject> SpawnableEnemies()
    {
        return EnemyModels;
    }
}
