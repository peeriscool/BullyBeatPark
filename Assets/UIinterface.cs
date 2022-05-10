using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIinterface : MonoBehaviour
{
    public TextMeshProUGUI stepcount;
    public TextMeshProUGUI childcount;
    public Button Endturn;
    public Button actions;
    public int steps;

    private void Awake()
    {
        actions.interactable = false;
        Endturn.interactable = false;
    }
    void Update()
    {
        if(Blackboard.EnemySelected == true ) //To DO: detect if player is in range of enemy 
        {
            //allow player to interact with enemy
            actions.interactable = true;
        }
        if (Blackboard.moves != null)
        {
            if(Blackboard.moves.Count == steps)
            {
                //player has to end turn
                Blackboard.player.GetComponent<PlayerScript>().enabled = false;
                //enable end turn button
                Endturn.interactable = true;
            }
            stepcount.text = Blackboard.moves.Count.ToString();
        }
        try
        {
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
}
