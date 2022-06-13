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
    string Tilename; //to make sure we only step on 1 tile at the time

    void Start()
    {
        Blackboard.moves = new List<Vector2Int>();
        Tilename = "";
    }

    public void floorcheck(Collision _current)
    {
        if(Tilename != _current.gameObject.name)
        {
           Tilename = _current.gameObject.name;
            try//get floor by name
            {
                string obj = Tilename;
                obj = obj.Remove(0, 4);
                string[] data = obj.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                int numVal1 = Int32.Parse(data[0]);
                int numVal2 = Int32.Parse(data[1]);
                location = new Vector2Int(numVal1, numVal2);
                if (location == new Vector2Int(0, 0)) { return; } //doesn't count as step
                dataretrieved = true;
            }
            catch (Exception e) //player not on known floor
            {
                Debug.Log("Grid position not detected: " + e);
                throw;
            }
        }
            if (dataretrieved)
            {
                Blackboard.moves.Add(location);
                
                dataretrieved = false;
            }
    }
}
        
