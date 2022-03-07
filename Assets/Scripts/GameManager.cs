using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //instances of classes
    private Enemy_Manager Enemy_Manager;
    private MazeGeneration mazegenerator;
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

        DontDestroyOnLoad(this);
        
        Blackboard.height = mazegenerator.height;
        Blackboard.width = mazegenerator.width;
        
        deployed = new List<GameObject>();
        SpawnEnemies();
        StartCoroutine("TickEnemies");
    }

    private void Update()
    {
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

   
    void Fire()
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