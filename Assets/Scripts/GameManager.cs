using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject playerrefrence;
    Enemy_Manager Enemy_Manager;
    public List<ScriptableEnemies> EnemyList;
    MazeGeneration mazegenerator = new MazeGeneration();
    public int enemytick = 60;
    public List<GameObject> deployed;
    private static GameManager instance;

    public static GameManager Instance   //singleton implemenetation
    {
        get
        {
            return instance;
        }
        
    }
    void Start()
    {
        if (instance != null && instance != this) //ToDo : alowing singleton to be switched with baseclass gamemanager
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        deployed = new List<GameObject>();
        SpawnEnemies();
        
        StartCoroutine("TickEnemies");
    }
    private void FixedUpdate()
    {

    }

    protected void SpawnEnemies()
    {
        Enemy_Manager = new Enemy_Manager(EnemyList);
        List<GameObject> deployables = Enemy_Manager.SpawnableEnemies();
      //  
        //foreach (GameObject item in deployables) //for each available enemy make 1
        //{
        //    deployed.Add(Instantiate(item, EnemyList[0].spawnPoints[0], new Quaternion()));     
        //}
       // foreach (Vector3 item in Enemy_Manager.spawnpoints())
            for (int i = 0; i < Enemy_Manager.spawnpoints().Count; i++)
            {
            deployed.Add(Instantiate(deployables[i], Enemy_Manager.getybyindex(i), new Quaternion()));
            Debug.Log(deployables[i].transform.position);
            }
       //for (int i = 0; i < deployables.Count; i++)
       // {
       //     deployed.Add(Instantiate(deployables[i], deployables[i].transform.position, new Quaternion()));
       // }
        //for (int i = 0; i < deployables.Count; i++)
        //{

        //    for (int x = 0; x < EnemyList[i].numberOfPrefabsToCreate; x++)
        //    {
        //        deployed.Add(Instantiate(EnemyList[x].Prefab, EnemyList[x].spawnPoints[x], new Quaternion()));
        //        i++;
        //    }
           
        //}
    }

    public void triggerMazeGeneration() //valentijn code
    {
        // Create action that binds to the primary action control on all devices.
        var action = new InputAction(binding: "*/{primaryAction}");

        // Have it run your code when action is triggered.
        action.performed += _ => Fire();

        // Start listening for control changes.
        action.Enable();
       // Keyboard.current[KeyCode.Space].wasPressedThisFrame;
      
          
    }
    void Fire()
    {
        mazegenerator.RegenarateMaze();
    }



    public IEnumerator TickEnemies() //give enmies new commands every x seconds
    {
        while (true)
        {
            Debug.Log("enemies walk");
            Enemy_Manager.globalwalkagents(deployed);
            yield return new WaitForSeconds(enemytick);
            
        }
    }
}
