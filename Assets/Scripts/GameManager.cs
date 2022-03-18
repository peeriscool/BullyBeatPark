using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MazeGeneration))]
public class GameManager : MonoBehaviour
{
    //--------------------------------------------------\\
    //active game data
    public GameObject playerrefrence;
    public List<ScriptableEnemies> EnemyList;
    public int enemytick = 60;
    public List<GameObject> deployed;
    bool Isfininshed = true;
    public Text FinishText; //hints the player to go to the end of the level
    public Canvas FinishCanvas; //interactive canvas for menu navigating
    //instances of classes
    private Enemy_Manager Enemy_Manager;
    private MazeGeneration mazegenerator;
   // private  SceneManagerScript Manager;
    private static GameManager _instance; //this
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
        mazegenerator = this.gameObject.GetComponent<MazeGeneration>(); 

        if (_instance != null && _instance != this) //ToDo : alowing singleton to be switched with baseclass gamemanager
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }

        //DontDestroyOnLoad(this);
        
        Blackboard.height = mazegenerator.height;
        Blackboard.width = mazegenerator.width;
        
        deployed = new List<GameObject>();
        SpawnEnemies();
        StartCoroutine("TickEnemies");
    }

    private void Update()
    {

        if (deployed.Count == 0 && Isfininshed)
        {

            //all enemies killed
            //inform player to get to the end of the level
            FinishText.enabled = true;
            if (Blackboard.levelfinished) //player has reached end of level
            {
                FinishCanvas.enabled = true;
                FinishCanvas.gameObject.SetActive(true);
                Isfininshed = false;
            }
        }
        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            SceneManagerScript.AppendScene("ControlsExplination");
            //call the controls menu
        }

        //if (playerrefrence.GetComponent<BoxCollider>().)
        //Blackboard.EnemylocationPing()
    }
    protected void SpawnEnemies()
    {
        Enemy_Manager = new Enemy_Manager(EnemyList);
        List<GameObject> deployables = Enemy_Manager.SpawnableEnemies();
        for (int i = 0; i < Enemy_Manager.spawnpoints().Count; i++)
        {
            deployed.Add(Instantiate(deployables[i], Enemy_Manager.getybyindex(i), new Quaternion()));
            Debug.Log("Spawning at "+deployables[i].name + " at " +deployables[i].transform.position);
        }
    }

   public void EnemyDied(GameObject deceased)
    {
        deployed.Remove(deceased);
        
    }
    void Regenarate()
    {
        mazegenerator.RegenarateMaze();
    }
    public IEnumerator TickEnemies() //give enmies new commands every x seconds
    {
        while (true)
        {
            Enemy_Manager.globalwalkagents(deployed,mazegenerator.width,mazegenerator.height);//currently the maze is 150 by 150 units
            yield return new WaitForSeconds(enemytick);
        }
    }
}

/*  public void triggerMazeGeneration() //valentijn code
    {
        // Create action that binds to the primary action control on all devices.
        var action = new InputAction(binding: "
{ primaryAction}
");

        // Have it run your code when action is triggered.
action.performed += _ => Fire();

// Start listening for control changes.
action.Enable();
        // Keyboard.current[KeyCode.Space].wasPressedThisFrame;


    }
*/