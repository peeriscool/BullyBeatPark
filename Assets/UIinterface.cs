using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIinterface : MonoBehaviour
{
    public TextMeshProUGUI stepcount;
    public TextMeshProUGUI childcount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Blackboard.moves != null)
        {
            stepcount.text = Blackboard.moves.Count.ToString();
        }
        try
        {
            if (Blackboard.Enemies != null)
            {
                childcount.text = GameManager.Instance.deployed.Count.ToString();
            }
        }
        catch (System.Exception)
        {

            throw;
        }
       
           
    }
}
