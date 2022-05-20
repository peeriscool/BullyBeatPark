using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class worldToGrid : MonoBehaviour //comopnent to detect floors and walls
{
    //should the player be able to walk back before reseting the values?
    // in this case whe would remove from the moves list if the position has already been walked on
    char[] seperators = new char[] { ':' };
    Vector2Int location;
    bool dataretrieved = false;
    BoxCollider mycollider;
    void Start()
    {
        Blackboard.moves = new List<Vector2Int>();
        mycollider =  this.gameObject.AddComponent<BoxCollider>();
        mycollider.isTrigger = true;
    }
   
    public void OnTriggerExit(Collider other)
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Floor")
        {
            Debug.Log("grounded");
        }
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
                 //Debug.Log(numVal1 +","+ numVal2);
                // //UIupdatable
                location = new Vector2Int(numVal1,numVal2);

                if(location == new Vector2Int(0,0))
                {
                    //doesn't count as step
                    return;
                }
                dataretrieved = true;
            }
            catch (Exception e) //player not on known floor
            {
               
                Debug.Log("Grid position not detected: " + e);
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
