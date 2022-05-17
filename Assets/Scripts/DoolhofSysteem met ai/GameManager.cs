using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MazeGeneration))]
public class GameManager : MonoBehaviour
{
    //---------------------active game data-----------------------------\\
    public GameObject player;
    public List<ScriptableEnemies> EnemyList;
    public List<GameObject> deployed;
    //bool Isfininshed = true;
    //---------------------instances of classes-----------------------------\\
    private Enemy_Manager EManager;
    private MazeGeneration mazegenerator;

    // private bool InUi = false;
    //--------------------------------------------------\\

    void Start()
    {
        mazegenerator = this.gameObject.GetComponent<MazeGeneration>();
        Blackboard.player = player;
        EManager = new Enemy_Manager(EnemyList); //initialize enemies
        SpawnEnemies();      
    }

    private void Update()
    {
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

    public void EndTurn() //when player hits end turn
    {
        Blackboard.moves = new List<Vector2Int>();
        player.GetComponent<PlayerScript>().enabled = true;
        // move enemies
        StartCoroutine(TickEnemies(1));
    }
    protected void SpawnEnemies()
    {
        List<GameObject> plot = EManager.enemyModels;

        for (int i = 0; i < EManager.enemyModels.Count; i++) //for every enemymodel instantiate x enemy's
        {
         //   Debug.Log("test for enemies" + EManager.setRandomposition(mazegenerator.height, mazegenerator.width));
            deployed.Add(Instantiate(plot[i], EManager.setRandomposition(mazegenerator.height, mazegenerator.width), new Quaternion()));
        }
        Blackboard.Enemies = deployed;
        StartCoroutine(TickEnemies(0)); //start by going to a random location
    }

    public IEnumerator TickEnemies(int duration) //give enmies new commands every x seconds
    {
        while (true)
        {
            EManager.globalwalkagents(deployed,mazegenerator.width,mazegenerator.height);//who/x/y
            yield return new WaitForSeconds(duration);
            break;
        }
    }
    public void EnemyDied(GameObject deceased)
    {
        deployed.Remove(deceased);
    }
}
