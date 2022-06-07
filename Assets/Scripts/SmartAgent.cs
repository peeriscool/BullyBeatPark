using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SmartAgent : MonoBehaviour
{
    public Vector2Int location = new Vector2Int(); //location on the grid
    private List<Vector2Int> path = new List<Vector2Int>();
    private AstarV2 Astar = new AstarV2(10, 10);
    //SimpleDungeonGenerator instance;
    Cell[,] Astarcell;
    // Update is called once per frame
    private void Start()
    {
        Astarcell = new Cell[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Cell cell = new Cell();
                cell.gridPosition = new Vector2Int(i, j);
                //walls
                Astarcell[i, j] = cell;
            }
        }
        path.Add(new Vector2Int(0, 0));
        path.Add(new Vector2Int(5, 5));
    }
    public void Update()
    {
        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            WalkTo(location,path[1], Astarcell);
        }
    }
    public void Tick()
    {
        if (path == null)
        {
            Debug.Log("No Path Set");
        }
        if (path != null && path.Count > 0)
        {
            if (transform.position != Vector2IntToVector3(path[0])) //moving
            {
                //    actionindex = 1;
                transform.position = Vector3.MoveTowards(transform.position, Vector2IntToVector3(path[0]), Time.deltaTime); //transform.position
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector2IntToVector3(path[0]) - transform.position), 360f * Time.deltaTime);
            }
            else
            {
                location = path[0]; //location /4 = ~gridsize world/ location  
                                    //   Debug.Log("location of "+ this.name +"=" + location + "world = "+this.gameObject.transform.position);// Debug.Log(path.Count + "Path control for" + gameObject.name);
                                    //   actionindex = 0;
                path.RemoveAt(0);
            }
        }
    }
    public void WalkTo(Vector2Int _location, Vector2Int current, Cell[,] grid)
    {
        Vector2Int targetPos = _location;
        Debug.Log("i " + this.gameObject.name + "At " + current + "want to go to: " + targetPos); //100/5 = 20 wich is the width/height of the maze
        List<Vector2Int> Rawpath = Astar.FindPathToTarget(current, targetPos, grid);
        try
        {
            for (int i = 0; i < Rawpath.Count; i++)
            {
                Rawpath[i] = Rawpath[i]; //* (int)(maze.scaleFactor * 2)
            }
            path = Rawpath;
          //  DrawPath();
            location = targetPos;
        }
        catch (System.Exception)
        {
            Debug.Log("path resulted in no instance: " + location + "from " + current);
            Debug.Log(Rawpath+ "rawpath");
            throw;
        }
    }
    
    private Vector3 Vector2IntToVector3(Vector2Int pos, float YPos = 0)
    {
        return new Vector3(Mathf.RoundToInt(pos.x), YPos, Mathf.RoundToInt(pos.y));
    }

}
