using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SmartAgent
{
    GameObject visual;
    public Vector2Int location; //location on the grid
    private List<Vector2Int> path;
    private AstarV2 Astar;
    public Cell[,] Astarcell;
    public float speed;
    private bool destination;

    private LineRenderer line;
    private MeshRenderer renderer;
    /// <summary>
    /// Make an enemy who can move in any given area
    /// </summary>
    /// <param name="x">size of area</param>
    /// <param name="y">size of area</param>
    /// <param name="Smartagent"></param>
    public SmartAgent(int x, int y,GameObject _Smartagent)
    {
        visual = _Smartagent;
        Astar = new AstarV2(x,y);
        location = Vector3ToVector2Int(_Smartagent.transform.position);
        path = new List<Vector2Int>();
        makecells(x,y);
        //visual debug
        line = _Smartagent.GetComponent<LineRenderer>();
        renderer = _Smartagent.GetComponentInChildren<MeshRenderer>();
        line.material.color = Color.white;
    }
    /// <summary>
    /// Sets the Astarcell list to a new cell size
    /// </summary>
    public void makecells(int x, int y)
    {
        Debug.Log("Starting Smart agent bloodcells");
        Astarcell = new Cell[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Cell cell = new Cell();
                cell.gridPosition = new Vector2Int(i, j);
                //walls
                Astarcell[i, j] = cell;
            }
        }
    }
    public void roomcells(Dictionary<int, List<Vector3Int>> playerea)
    {
        Vector3Int[] locs = new Vector3Int[playerea.Count+1]; //playerea.Values;
        for (int i = 0; i < playerea.Count; i++) //convert dictionary to vector2 cells
        {
            locs = playerea[i].ToArray();
            Astarcell = new Cell[locs[i].x, locs[i].y];
        }
        //foreach (List<Vector3Int> item in playerea.Values)
        //{

        //    //   Astarcell = new Cell[item[i], playerea.va];
        //}
        for (int i = 0; i < playerea.Count; i++)
        {
            //   Astarcell = new Cell[playerea.Values (locs, i),];
        }

    }
        public void Update()
    {
        //if(Keyboard.current.enterKey.wasPressedThisFrame) //manual overide
        //{
        //    WalkTo(new Vector3(Random.Range(0, 10),Random.Range(0, 10)), location, Astarcell);
        //}
        Tick();
    }
    public bool Tick()
    {
        if (path == null)
        {
            Debug.Log("No Path Set");
            
        }
        if (path != null && path.Count > 0)
        {
            
            destination = false;
            if (visual.transform.position != Vector2IntToVector3(path[0])) //moving
            {
                //    actionindex = 1;
                visual.transform.position = Vector3.MoveTowards(visual.transform.position, Vector2IntToVector3(path[0]), Time.deltaTime / speed); //transform.position
                visual.transform.rotation = Quaternion.RotateTowards(visual.transform.rotation, Quaternion.LookRotation(Vector2IntToVector3(path[0]) - visual.transform.position), 360f * Time.deltaTime/ speed);
            }
            else
            {
                location = path[0]; //location /4 = ~gridsize world/ location  
                                    //   Debug.Log("location of "+ this.name +"=" + location + "world = "+this.gameObject.transform.position);// Debug.Log(path.Count + "Path control for" + gameObject.name);
                                    //   actionindex = 0;
                path.RemoveAt(0);
                destination = true;
            }
        }
        return destination;
    }
    public void WalkTo(Vector3 startPos, Vector2Int endPos, Cell[,] grid)
    {
        if (line != null)
        {
            DrawPath();
        }
        Vector2Int targetPos = Vector3ToVector2Int(startPos);
      //  Debug.Log("i " + visual.gameObject.name + "At " + startPos + "want to go to: " + endPos); //100/5 = 20 wich is the width/height of the maze
        List<Vector2Int> Rawpath = Astar.FindPathToTarget(endPos, targetPos, grid);
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
            Debug.Log("path resulted in no instance: " + location + "from " + endPos);
            Debug.Log(Rawpath.Count+ "rawpath");
            throw;
        }
       
    }
    private void DrawPath()
    {
        if (path != null && path.Count > 0)
        {
            // Debug.Log("Path drawn from " + path[0] + "To " + path[path.Count-1]);
            line.positionCount = path.Count;
            for (int i = 0; i < path.Count; i++)
            {
                line.SetPosition(i, Vector2IntToVector3(path[i], 0.1f));
            }
        }
        else
        {
            Debug.Log(path + "non working paths");
        }
    }
    public Vector2Int Vector3ToVector2Int(Vector3 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));
    }
    private Vector3 Vector2IntToVector3(Vector2Int pos, float YPos = 0)
    {
        return new Vector3(Mathf.RoundToInt(pos.x), YPos, Mathf.RoundToInt(pos.y));
    }
    private void OnDrawGizmos()
    {
        if (path != null && path.Count > 0)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.color = renderer.material.color;
                Gizmos.DrawLine(Vector2IntToVector3(path[i], 0.5f), Vector2IntToVector3(path[i + 1], 0.5f));
            }
        }
    }
}
