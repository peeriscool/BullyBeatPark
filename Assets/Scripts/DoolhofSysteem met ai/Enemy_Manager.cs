using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager
{
    public int enemycount;
    private List<GameObject> enemyModels = new List<GameObject>();
  //  private List<Vector3> Spawnpoints = new List<Vector3>();
    private List<Agent> agents;
    public Enemy_Manager(List<ScriptableEnemies> horde,int _enemycount) //recieve a list of enemies make them in to models
    {
        enemycount = _enemycount;
        int X = 0;
        foreach (ScriptableEnemies Enemydata in horde)
        {
            for (int i = 0; i < Enemydata.numberOfPrefabsToCreate; i++)
            {
                enemyModels.Add(Enemydata.Prefab);
                enemyModels[X].name = Enemydata.prefabName;
                enemyModels[X].transform.position = Enemydata.spawnPoints[i];
             //   Spawnpoints.Add(Enemydata.spawnPoints[i]);
                X++;          
            }
        }     
    }

    public List<GameObject> SpawnableEnemies()
    {
        return enemyModels;
    }
    //public List<Vector3> spawnpoints()
    //{
    //    return Spawnpoints;
    //}
    //public Vector3 getybyindex(int i)///asigns 1 of the predefined spawnpoints in the object
    //{
    //    return Spawnpoints[i];
    //}

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
                instances[i].gameObject.GetComponent<Agent>().WalkTo(setRandomposition(sizeX-1 , sizeY-1),instances[i].gameObject.GetComponent<Agent>().location); //walking range should be width height maze
               // instances[i].gameObject.GetComponent<Agent>(). = ;
            }
            //go throug array and change path location for agent  
        }
    }
    public Vector3 setRandomposition(int sizeX,int sizeY)
    {
       
        Vector3 locationcommand = new Vector3(Random.Range(0, sizeX), 0, Random.Range(0, sizeY));
        return locationcommand;
    }
    private Vector2Int Vector3ToVector2Int(Vector3 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));
    }
}
