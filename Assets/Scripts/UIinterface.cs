using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIinterface : MonoBehaviour //updates the UItext with data from the blackboard
{
    public TextMeshProUGUI stepcount;
    public TextMeshProUGUI childcount;
    public Button exit;
    public Button actions;
    public int steps;

    private void Awake()
    {
        actions.interactable = false;
        exit.interactable = true;
    }
    void Update()
    {
      
        try
        {
            if (Blackboard.Enemies.Count >= 0) //To DO: detect if player is in range of enemy 
            {
                //allow player to interact with enemy
                actions.interactable = true;
            }
            if (Blackboard.moves != null)
            {
                stepcount.text = Blackboard.moves.Count.ToString();
            }
            if (Blackboard.Enemies != null)
            {
                childcount.text = Blackboard.Enemies.Count.ToString();//GameManager.Instance.deployed.Count.ToString();
            }
        }
        catch (System.Exception)
        {

            throw;
        }        
    }

    public void tomenu()
    {
        SceneManagerScript.callScenebyname("StartMenu");
    }
}
