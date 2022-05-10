using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class worldToGrid : MonoBehaviour //comopnent to detect floors and walls
{
    char[] seperators = new char[] { ':' };
    Vector2Int location;
    bool dataretrieved = false;
    void Start()
    {
        Blackboard.moves = new List<Vector2Int>();
    }

    void Update()
    {
       
    }
    public void OnTriggerExit(Collider other)
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        ///var item = other.GetComponent<Item>();
        if (other.gameObject.layer == 6)// 6 = "walls and floors"
        {
            try
            {
                string obj = other.name;
                obj = obj.Remove(0,4);
                string[] data = obj.Split(seperators,StringSplitOptions.RemoveEmptyEntries);

                 int numVal1 = Int32.Parse(data[0]);
                 int numVal2 = Int32.Parse(data[1]);            
                 Debug.Log(numVal1 +","+ numVal2);
                // //UIupdatable
                location = new Vector2Int(numVal1,numVal2);
                dataretrieved = true;
            }
            catch (Exception e) //player not on known floor
            {
               
                Debug.Log("Grid position not detected" + e);
                throw;
            }
            if(dataretrieved)
            {
                
                Blackboard.moves.Add(location);
                dataretrieved = false;
            }
        }
    }
}
