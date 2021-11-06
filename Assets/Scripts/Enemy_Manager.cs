using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager
{
    private List<GameObject> EnemyModels = new List<GameObject>();
    private List<Vector3> Spawnpoints = new List<Vector3>();
    // GameManager Owner;
    public Enemy_Manager(List<ScriptableEnemies> horde) // ,GameManager owner
    {
        int X = 0;
        foreach(ScriptableEnemies Enemydata in horde)
        {
            for (int i = 0; i < Enemydata.numberOfPrefabsToCreate; i++)
            {
            EnemyModels.Add(Enemydata.Prefab);
            EnemyModels[X].name = Enemydata.prefabName;
            EnemyModels[X].transform.position = Enemydata.spawnPoints[i];
            Spawnpoints.Add(Enemydata.spawnPoints[i]);
                X++;
            }
            
        }
       // Owner = owner;
    }
   
    public List<GameObject> SpawnableEnemies()
    {
        return EnemyModels;
    }
    public List<Vector3> spawnpoints()
    {
        return Spawnpoints ;
    }
    public Vector3 getybyindex(int i)
    {
        return Spawnpoints[i];
    }

    public void globalwalkagents(List<GameObject> instances) //give instanced enemies as param
    {
        for (int i = 0; i < instances.Count; i++)
        {
            //get array of instanced gameobjects from the owner
            //go throug array and change path location for agent
            instances[i].gameObject.GetComponent<Agent>().WalkTo(new Vector3(Random.Range(0,20), 1, Random.Range(0, 20))); //walking range should be width height maze
        }
    }
}
