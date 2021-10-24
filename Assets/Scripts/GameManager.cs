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
    private static GameManager instance;
    public static GameManager Instance   //singleton
    {
        get
        {
            return instance;
        }
        
    }
    void Start()
    {
        if (instance != null && instance != this) //ToDo : alowing singleton gamemanger to be switched with baseclass gamemanagers?
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);


        Enemy_Manager = new Enemy_Manager(EnemyList);
        List<GameObject> deployables = Enemy_Manager.SpawnableEnemies();
        foreach (GameObject item in deployables) //make some enemies
        {
            Instantiate(item, EnemyList[0].spawnPoints[0], new Quaternion());
        }
        //for (int i = 0; i < deployables.Count; i++)
        //{
        //    GameObject instance = deployables[i];
        //    instance.transform.localScale = deployables[i].transform.localScale; //* Random.Range(0.5f, 1f); fix fbx in prefab!
        //    Instantiate(instance, EnemyList[0].spawnPoints[0], new Quaternion());

        //}
    }
    // Update is called once per frame
    public void triggerMazeGeneration()
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
}
