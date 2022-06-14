using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Levelone : MonoBehaviour
{
    public List<ScriptableEnemies> childerenobjects;
    public List<GameObject> deployed;
    public MazeGeneration mazegenerator;
    Enemy_Manager childerenmanager;
    Dictionary<int, bool> levels;
    public GameObject player; //make player scriptable object?
    // Start is called before the first frame update
    void Awake()
    {
        player = Blackboard.player;
        Blackboard.moves = new List<Vector2Int>();
        Blackboard.maxmoves = 10;
        childerenmanager = new Enemy_Manager(childerenobjects);
        SpawnEnemies();
        //mazegenerator.GenerateMaze();
        Blackboard.generateleveldict();
        levels = Blackboard.getleveldict();
        Blackboard.setlevelstatus(SceneManagerScript.returnactivesceneint(), false);
       // player.GetComponent<worldToGrid>().enabled = true; //shitty solution
    }

    // Update is called once per frame
    void Update()
    {
        //if (Keyboard.current.enterKey.wasPressedThisFrame) //manual overide
        //{
           
        //    //alkTo(new Vector3(Random.Range(0, 10), Random.Range(0, 10)), location, Astarcell);
        //    //childerenmanager.globalwalkagents(deployed, mazegenerator);
        //    childerenmanager.walkChilderen(deployed, mazegenerator);
        //}
        if (SceneManagerScript.returnactivesceneint() == 1 && levels[1] == false) //maze
        {
            foreach (GameObject item in deployed) //make sure all enemies are not at 0,0
            {
                Agent instance = item.GetComponent<Agent>();
                instance.maze = mazegenerator;
                instance.WalkTo(new Vector3(Random.Range(1, Blackboard.Mazewidth), 0, Random.Range(1, Blackboard.Mazeheight)), instance.location, mazegenerator.grid);
            }
            levels[1] = true;
        }
        if (Blackboard.moves != null)
        {
            if(Blackboard.moves.Count == 10 || Blackboard.moves.Count >= 10)
            {
                foreach (GameObject item in deployed) //make sure all enemies are not at 0,0
                {
                    Agent instance = item.GetComponent<Agent>();
                    instance.maze = mazegenerator;
                    instance.WalkTo(new Vector3(Random.Range(1, Blackboard.Mazewidth), 0, Random.Range(1, Blackboard.Mazeheight)), instance.location, mazegenerator.grid);
                }
                Blackboard.moves = new List<Vector2Int>();
            }
        }

        if (Blackboard.Enemies.Count == 0) //win condition
        {
            //all enemies killed
            //inform player to get to the end of the level
            SceneManagerScript.callScenebyname("level_2");
           // player.transform.position = Vector3.zero;
        }
        //int count = 0;
        //List<int>remove = new List<int>();
        //    for (int i = 0; i < Blackboard.Enemies.Count; i++) //check if any kids died oops!
        //    {
        //        if (Blackboard.Enemies.Contains(deployed[i]))
        //        {
        //        count++;
        //        }
        //        else
        //        {
        //        deployed.RemoveAt(i);
        //        }
        //    }
    }
    /// <summary>
    /// sets blackboard enemies
    /// </summary>
    protected void SpawnEnemies()
    {
        List<GameObject> plot = childerenmanager.enemyModels;
        Debug.Log("Creating: " + childerenmanager.enemyModels.Count  + " childeren");
        for (int i = 0; i < childerenmanager.enemyModels.Count; i++) //for every enemymodel instantiate x enemy's
        {
            deployed.Add(Instantiate(plot[i], plot[i].transform.position, new Quaternion()));
        }
        Blackboard.Enemies = deployed;
    }
    public void EnemyDied(GameObject deceased)
    {
        deployed.Remove(deceased);
    }
}
