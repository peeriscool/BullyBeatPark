using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager
{
    private List<GameObject> EnemyModels = new List<GameObject>();
    //Enemies.EnemyOne one;
    public Enemy_Manager(List<ScriptableEnemies> horde)
    {
        int X = 0;
        foreach(ScriptableEnemies index in horde)
        {
            for (int i = 0; i < index.numberOfPrefabsToCreate; i++)
            {
            EnemyModels.Add(index.Prefab);
            EnemyModels[X].name = index.prefabName;
            EnemyModels[X].transform.position = index.spawnPoints[i];
            }
            X++;
        }
    }
   
    public List<GameObject> SpawnableEnemies()
    {
        return EnemyModels;
    }
}
