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
        foreach (ScriptableEnemies Enemydata in horde) //papa were do childeren come from? scriptableobjects.
        {
            for (int i = 0; i < Enemydata.amountTomake; i++)
            {
                enemyModels.Add(Enemydata.Prefab); //get gameobject
                enemyModels[X].name = Enemydata.prefabName; //get name
                //Enemydata.spawnPoints[i] = new Vector3(Random.Range(0, Blackboard.Mazewidth-1),0, Random.Range(0, Blackboard.Mazeheight-1));
                enemyModels[X].transform.position = Enemydata.spawnPoints[i]; //set initial spawnlocation
                X++;
            }
        }
        for (int i = 0; i < enemyModels.Count; i++)
        {
            GameObject.Instantiate(enemyModels[i]);
        }
    }
    public void globalwalkagents(List<GameObject> instances, MazeGeneration m) //give instanced enemies as param
    {
        Cell[,] grid = m.grid;
        for (int i = 0; i < instances.Count; i++)
        {
            instances[i].gameObject.GetComponent<Agent>().maze = m;
            instances[i].gameObject.GetComponent<Agent>().WalkTo(command(grid.GetLength(0), grid.GetLength(1)), instances[i].gameObject.GetComponent<Agent>().location, grid); //walking range should be width height maze
        }
    }
    public Vector3 command(int sizeX, int sizeY)
    {

        Vector3 locationcommand = new Vector3(Random.Range(0, sizeX), 0, Random.Range(0, sizeY));
        return locationcommand;
    }
    public void walkChilderen(List<GameObject> instances, MazeGeneration m)
    {
        if (Blackboard.moves != null)
        {
            if (Blackboard.moves.Count >= Blackboard.maxmoves)
            {
                //player has to end turn
                Blackboard.player.GetComponent<PlayerScript>().enabled = false;

                foreach (GameObject item in instances) //make sure all enemies are not at 0,0
                {
                    Agent instance = item.GetComponent<Agent>();
                    instance.maze = m;
                    instance.WalkTo(new Vector3(Random.Range(0, Blackboard.Mazewidth), 0, Random.Range(0, Blackboard.Mazeheight)), instance.location, m.grid);
                }
                Blackboard.moves.Clear();
                //enable end turn button
                //  Endturn.interactable = true;
            }
        }
        if (Blackboard.moves.Count == 0)
        {
            Blackboard.player.GetComponent<PlayerScript>().enabled = true;
        }
    }
}
      