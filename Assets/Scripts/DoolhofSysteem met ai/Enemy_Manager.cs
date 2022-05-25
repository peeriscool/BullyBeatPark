using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager
{
    public List<GameObject> enemyModels = new List<GameObject>();
  //  private List<Vector3> Spawnpoints = new List<Vector3>();
    private List<Agent> agents;
    public Enemy_Manager(List<ScriptableEnemies> horde) //recieve a list of enemies make them in to models
    {

        int X = 0;
        foreach (ScriptableEnemies Enemydata in horde)
        {
            for (int i = 0; i < Enemydata.amountTomake; i++)
            {
                enemyModels.Add(Enemydata.Prefab); //get gameobject
                enemyModels[X].name = Enemydata.prefabName; //get name
                Enemydata.spawnPoints[i] = new Vector3(Random.Range(0, Blackboard.Mazewidth-1),0, Random.Range(0, Blackboard.Mazeheight-1));
                enemyModels[X].transform.position = Enemydata.spawnPoints[i]; //set initial spawnlocation
                X++;          
            }
        }     
    }
    public void globalwalkagents(List<GameObject> instances, int sizeX, int sizeY) //give instanced enemies as param
    {
        for (int i = 0; i < instances.Count; i++)
        {
                instances[i].gameObject.GetComponent<Agent>().WalkTo(setRandomposition(sizeX-1 , sizeY-1),instances[i].gameObject.GetComponent<Agent>().location); //walking range should be width height maze
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