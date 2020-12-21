using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Enemy_Manager Enemy_Manager;
    BlackBoard blackboard;
    public List<ScriptableEnemies> EnemyList;
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        
        blackboard = new BlackBoard(EnemyList);
        Enemy_Manager = new Enemy_Manager(blackboard.GetEnemyModels());
        List<GameObject> deployables = Enemy_Manager.SpawnableEnemies();

        for (int i = 0; i < deployables.Count; i++)
        {
            GameObject instance = deployables[i];
            instance.transform.localScale = deployables[i].transform.localScale; //* Random.Range(0.5f, 1f); Wil Change fbx in prefab!
            Instantiate(instance, blackboard.GetSpawnLocation(i),new Quaternion()); 
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
