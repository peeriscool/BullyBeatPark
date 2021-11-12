using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager
{
    private List<GameObject> EnemyModels = new List<GameObject>();
    private List<Vector3> Spawnpoints = new List<Vector3>();
    public Enemy_Manager(List<ScriptableEnemies> horde)
    {
        int X = 0;
        foreach (ScriptableEnemies Enemydata in horde)
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
    }

    public List<GameObject> SpawnableEnemies()
    {
        return EnemyModels;
    }
    public List<Vector3> spawnpoints()
    {
        return Spawnpoints;
    }
    public Vector3 getybyindex(int i)
    {
        return Spawnpoints[i];
    }

    public void globalwalkagents(List<GameObject> instances, int sizeX, int sizeY) //give instanced enemies as param
    {
        
        for (int i = 0; i < instances.Count; i++)
        {
            if (instances[i].gameObject.GetComponent<Agent>().actionindex == 1)
            {
                //location already set and not yet reached
                //dont give new location
            }
            else
            {
                instances[i].gameObject.GetComponent<Agent>().WalkTo(setwalkposition(sizeX, sizeY)); //walking range should be width height maze
            }
            //go throug array and change path location for agent  
        }
    }
    protected Vector3 setwalkposition(int sizeX,int sizeY)
    {
       
        Vector3 locationcommand = new Vector3(Random.Range(0, sizeX), 0, Random.Range(0, sizeY));
        return locationcommand;
    }
}
