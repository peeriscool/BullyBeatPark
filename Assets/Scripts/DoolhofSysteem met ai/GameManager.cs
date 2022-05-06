using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MazeGeneration))]
public class GameManager : MonoBehaviour
{
    //---------------------active game data-----------------------------\\
    //
    public GameObject player;
    public List<ScriptableEnemies> EnemyList;
    public int enemytick;
    public List<GameObject> deployed;
    //bool Isfininshed = true;
    public Text FinishText; //hints the player to go to the end of the level
    public Canvas FinishCanvas; //interactive canvas for menu navigating

    //---------------------instances of classes-----------------------------\\
    private Enemy_Manager Enemy_Manager;
    private MazeGeneration mazegenerator;
    private static GameManager _instance; //this
    // private bool InUi = false;
    // private  SceneManagerScript Manager;
    //--------------------------------------------------\\

    public static GameManager Instance   //singleton implemenetation
    {
        get
        {
            return _instance;
        }
    }
    
    void Start()
    {
        if (_instance != null && _instance != this) //ToDo : alowing singleton to be switched with baseclass gamemanager
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        mazegenerator = this.gameObject.GetComponent<MazeGeneration>();
        Blackboard.height = mazegenerator.height;
        Blackboard.width = mazegenerator.width;
        deployed = new List<GameObject>();
        SpawnEnemies();      
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StartCoroutine(TickEnemies(1));
        }

        //if (deployed.Count == 0 && Isfininshed) //win condition
        //{
        //    //all enemies killed
        //    //inform player to get to the end of the level
        //    FinishText.enabled = true;
        //    if (Blackboard.levelfinished) //player has reached end of level
        //    {
        //        FinishCanvas.enabled = true;
        //        FinishCanvas.gameObject.SetActive(true);
        //        Isfininshed = false;
        //    }
        //}
        //if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        //{
        //    if(!InUi)
        //    { SceneManagerScript.AppendScene("ControlsExplination"); InUi = true; }
        //    if(InUi)
        //    { SceneManagerScript.DeppendScene("ControlsExplination");InUi = false; }
        //    //call the controls menu
        //}
    }

    /// <summary>
    /// Enemy initialization and first location
    /// </summary>
    protected void SpawnEnemies()
    {    
        Enemy_Manager = new Enemy_Manager(EnemyList,EnemyList.Count); //initialize enemies

        List<GameObject> deployables = Enemy_Manager.SpawnableEnemies();
        for (int i = 0; i < Enemy_Manager.enemycount; i++) //for every spawnpoint instantiate enemy
        {
            deployed.Add(Instantiate(deployables[i], Enemy_Manager.setRandomposition(mazegenerator.height,mazegenerator.width), new Quaternion()));
            //Debug.Log("Spawning at "+deployables[i].name + " at " +deployables[i].transform.position);
            //Debug.Log("compare: " + deployed[i].transform.position.ToString());
        }
        StartCoroutine(TickEnemies(0)); //start by going to a random location
    }

    public IEnumerator TickEnemies(int duration) //give enmies new commands every x seconds
    {
        while (true)
        {
            Enemy_Manager.globalwalkagents(deployed,mazegenerator.width,mazegenerator.height);//who/x/y
            yield return new WaitForSeconds(duration);
            break;
        }
    }
    public void EnemyDied(GameObject deceased)
    {
        deployed.Remove(deceased);
    }
}
