using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveltwo : MonoBehaviour
{
    public List<ScriptableEnemies> childerenobjects;
    public List<GameObject> deployed;
    public RoomDungeonGenerator dungeon;
    Enemy_Manager childerenmanager;
    Dictionary<int, bool> levels;
    public GameObject player;
    public BegeleiderStateMAchine begeleider;
    Cell[,] cells;
    // Start is called before the first frame update
    void Start()
    {
        player = Blackboard.player;
        Blackboard.moves = new List<Vector2Int>();
        Blackboard.maxmoves = 10;
        childerenmanager = new Enemy_Manager(childerenobjects);
        cells = dungeon.dungeontocell();
        SpawnEnemies();
        //mazegenerator.GenerateMaze();
        Blackboard.generateleveldict();
        levels = Blackboard.getleveldict();
        Blackboard.setlevelstatus(SceneManagerScript.returnactivesceneint(), false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManagerScript.returnactivesceneint() == 2 && levels[2] == false) //maze
        {
            //foreach (GameObject item in deployed) //make sure all enemies are not at 0,0
            //{
            //    Agent instance = item.GetComponent<Agent>();
            //    //  instance.maze = //mazegenerator;
            //    instance.WalkTo(new Vector3(Random.Range(1, dungeon.GridWidth), 0, Random.Range(1, dungeon.GridWidth)), instance.location, cells); //cell[,]
            //}

            levels[2] = true;
            Blackboard.setlevelstatus(SceneManagerScript.returnactivesceneint(), true);
         
        }
        if (Blackboard.moves != null)
        {
            if (Blackboard.moves.Count == 10 || Blackboard.moves.Count >= 10)
            {
                try
                {
                 //   begeleider.Targetplayer(new Vector3Int(Blackboard.moves[0].x, 0, Blackboard.moves[0].y));
                    foreach (GameObject item in deployed) //make sure all enemies are not at 0,0
                    {
                        Agent instance = item.GetComponent<Agent>();
                        //instance.maze = //mazegenerator;
                        instance.WalkTo(new Vector3(Random.Range(1, dungeon.GridWidth), 0, Random.Range(1, dungeon.GridWidth)), instance.location, cells); //cell[,]
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
              
                Blackboard.moves = new List<Vector2Int>();
            }
        }

        if (Blackboard.Enemies.Count == 0) //win condition
        {
            //all enemies killed
            //inform player to get to the end of the level
            SceneManagerScript.callScenebyname("StartMenu");
            // player.transform.position = Vector3.zero;
        }
    }

    protected void SpawnEnemies()
    {
        List<GameObject> plot = childerenmanager.enemyModels;
        Debug.Log("Creating: " + childerenmanager.enemyModels.Count + " childeren");
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
