using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MazeGeneration))]
public class GameManager : MonoBehaviour
{
    //---------------------active game data-----------------------------\\
    public List<GameObject> Dondestroyonload;
    public GameObject player;
    public List<ScriptableEnemies> EnemyList;
    public List<GameObject> deployed;
    //bool Isfininshed = true;
    //---------------------instances of classes-----------------------------\\
    private Enemy_Manager EManager;
    private MazeGeneration mazegenerator;

    Dictionary<int, bool> levels;
    //--------------------------------------------------\\

    void Start()
    {

        ItemInteraction inventory = player.GetComponent<ItemInteraction>();
        InventoryObject inv = inventory.GetInventoryobject();
        inv.LoadIformat();

        DontDestroyOnLoad(this.gameObject);
        foreach (GameObject item in Dondestroyonload)
        {
            DontDestroyOnLoad(item);
        }
        mazegenerator = this.gameObject.GetComponent<MazeGeneration>();
        // Blackboard.player = player;
        EManager = new Enemy_Manager(EnemyList); //initialize enemies
        levels = new Dictionary<int, bool>();
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
        {
            levels.Add(i,false);
        }
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
        mazegenerator.init();
        SpawnEnemies();
        player.GetComponent<worldToGrid>().enabled = true;
        Blackboard.maxmoves = 5;
        levels[1] = true;

    }
    private void Update()
    {

        if(SceneManagerScript.returnactivesceneint() == 0) //lobby
        {

        }
        if (SceneManagerScript.returnactivesceneint() == 1 && levels[1] == false) //maze
        {
            level1(); //initialize level values

            //update
            if (Blackboard.moves != null)
            {
                if (Blackboard.moves.Count >= Blackboard.maxmoves)
                {
                    //player has to end turn
                    Blackboard.player.GetComponent<PlayerScript>().enabled = false;
                    //enable end turn button
                    //  Endturn.interactable = true;
                }
            }
        }
        //if (deployed.Count == 0) //win condition
        //{
        //    //all enemies killed
        //    //inform player to get to the end of the level


        //}
        //if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        //{

        //}
    }

    public void EndTurn() //when player hits end turn
    {
        Blackboard.moves = new List<Vector2Int>();
        player.GetComponent<PlayerScript>().enabled = true;
        // move enemies
      //  StartCoroutine(TickEnemies(1));
    }
    protected void SpawnEnemies()
    {
        List<GameObject> plot = EManager.enemyModels;

        for (int i = 0; i < EManager.enemyModels.Count; i++) //for every enemymodel instantiate x enemy's
        {
         //   Debug.Log("test for enemies" + EManager.setRandomposition(mazegenerator.height, mazegenerator.width));
            deployed.Add(Instantiate(plot[i], plot[i].transform.position, new Quaternion()));
        }
        Blackboard.Enemies = deployed;
      //  StartCoroutine(TickEnemies(0)); //start by going to a random location
    }

    //public IEnumerator TickEnemies(int duration) //give enmies new commands every x seconds
    //{
    //    while (true)
    //    {
    //        EManager.globalwalkagents(deployed,mazegenerator.width,mazegenerator.height);//who/x/y
    //        yield return new WaitForSeconds(duration);
    //        break;
    //    }
    //}
    public void EnemyDied(GameObject deceased)
    {
        deployed.Remove(deceased);
    }
}
