using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(MazeGeneration))]
public class GameManager : MonoBehaviour
{
    //---------------------active game data-----------------------------\\
    public GameObject player;
    Levelone mazelevel;
    Cell[,] leveldata;
    // public List<ScriptableEnemies> EnemyList;
    //public List<GameObject> deployed;
    //bool Isfininshed = true;
    //---------------------instances of classes-----------------------------\\
    // private Enemy_Manager EManager;
    private MazeGeneration mazegenerator;
  //  static Cell[,] gridrefrence;
    Dictionary<int, bool> levels;
    //--------------------------------------------------\\

    void Start()
    {

        ItemInteraction inventory = player.GetComponent<ItemInteraction>();
        InventoryObject inv = inventory.GetInventoryobject();
        inv.LoadIformat();

        //DontDestroyOnLoad(this.gameObject);
        //foreach (Transform item in this.GetComponent<Transform>())
        //{
        //    DontDestroyOnLoad(item);
        //}
       // mazegenerator = this.gameObject.GetComponent<MazeGeneration>();
        // Blackboard.player = player;
      //  EManager = new Enemy_Manager(EnemyList); //initialize enemies
        Blackboard.generateleveldict();
        levels = Blackboard.getleveldict();
        Blackboard.setlevelstatus(SceneManagerScript.returnactivesceneint(),false); //false because there is no need to be ready in lobby level
        lobby();

    }
    void lobby()
    {
        Blackboard.player = player;
    }
    void level1()
    {
        //intit
        player.transform.position = Vector3.zero;
       // mazegenerator.init();
      //  if(!Eready) SpawnEnemies(); //once plz
        player.GetComponent<worldToGrid>().enabled = true;
       // leveldata = mazelevel.mazegenerator.grid;
       // Blackboard.maxmoves = 10;
        //mazegenerator = GameObject.FindObjectOfType<MazeGeneration>();
        //mazegenerator.GenerateMaze();
       // gridrefrence = mazegenerator.grid;

        levels[1] = true;
      //  return null;
       
        
        // return null;
    }
    //public void WalkEnemyCommand(Agent instance)
    //{
    //    instance.WalkTo(new Vector3(Random.Range(0, Blackboard.Mazewidth), 0, Random.Range(0, Blackboard.Mazeheight)), instance.location, leveldata);
    //}
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) //manual overide
        { SceneManagerScript.callScenebyname("Level_1"); }
            //if(SceneManagerScript.returnactivesceneint() == 0) //lobby
            //{

            //}
            //if (SceneManagerScript.returnactivesceneint() == 1 && levels[1] == false) //maze
            //{   
            //        level1();
            //    //foreach (GameObject item in deployed) //make sure all enemies are not at 0,0
            //    //{
            //    //    Agent instance = item.GetComponent<Agent>();
            //    //    instance.WalkTo(new Vector3(Random.Range(0, Blackboard.Mazewidth), 0, Random.Range(0, Blackboard.Mazeheight)), instance.location, mazegenerator.grid);
            //    //}
            //    //initialize level values
            //}
            //if (levels[1] == true && SceneManagerScript.returnactivesceneint() == 1) //update level1
            //{
            //    //update
            //    if (Blackboard.moves != null)
            //    {
            //        if (Blackboard.moves.Count >= Blackboard.maxmoves)
            //        {
            //            //player has to end turn
            //            Blackboard.player.GetComponent<PlayerScript>().enabled = false;

            //            //foreach (GameObject item in deployed) //make sure all enemies are not at 0,0
            //            //{
            //            //    Agent instance = item.GetComponent<Agent>();
            //            //    instance.maze = mazegenerator;
            //            //    instance.WalkTo(new Vector3(Random.Range(0, Blackboard.Mazewidth), 0, Random.Range(0, Blackboard.Mazeheight)), instance.location, mazegenerator.grid);
            //            //}
            //            Blackboard.moves.Clear();
            //            //enable end turn button
            //            //  Endturn.interactable = true;
            //        }
            //    }
            //    if(Blackboard.moves.Count == 0)
            //    {
            //        Blackboard.player.GetComponent<PlayerScript>().enabled = true;
            //    }


            //    if (Blackboard.Enemies.Count == 0) //win condition
            //    {
            //        //all enemies killed
            //        //inform player to get to the end of the level
            //        SceneManagerScript.callScenebyname("level_2");
            //        player.transform.position = Vector3.zero;
            //    }
            //}


        }

    public void EndTurn() //when player hits end turn
    {
        Blackboard.moves = new List<Vector2Int>();
        player.GetComponent<PlayerScript>().enabled = true;
        // move enemies
      //  StartCoroutine(TickEnemies(1));
    }
    //protected void SpawnEnemies()
    //{
    //    List<GameObject> plot = EManager.enemyModels;

    //    for (int i = 0; i < EManager.enemyModels.Count; i++) //for every enemymodel instantiate x enemy's
    //    {
    //     //   Debug.Log("test for enemies" + EManager.setRandomposition(mazegenerator.height, mazegenerator.width));
    //        deployed.Add(Instantiate(plot[i], plot[i].transform.position, new Quaternion()));
    //    }
    //    Blackboard.Enemies = deployed;
    //    Eready = true;
    //}

    //public void EnemyDied(GameObject deceased)
    //{
    //    deployed.Remove(deceased);
    //}
}
