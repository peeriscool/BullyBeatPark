using System.Collections.Generic;
using UnityEngine;
public class Agent : MonoBehaviour
{
    public float moveSpeed = 3;
    public Vector2Int location = new Vector2Int();
    public int Hp = 3;
    private AstarV2 Astar = new AstarV2(Blackboard.Mazewidth,Blackboard.Mazeheight);
    private List<Vector2Int> path = new List<Vector2Int>();
    private MeshRenderer renderer;
    public MazeGeneration maze { get; set; }
    private LineRenderer line;

    public Agent(MeshRenderer renderer)
    {
        this.renderer = renderer;
    }

    private void Awake()
    {
      //  maze = FindObjectOfType<MazeGeneration>();
        renderer = GetComponentInChildren<MeshRenderer>();
        line = GetComponent<LineRenderer>();
        line.material.color = Color.white;
    }

    public void FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        path = Astar.FindPathToTarget(startPos, endPos, grid);
        for (int i = 0; i < path.Count; i++)
        {
            path[i] = path[i]; //should set the path to the world position
        }
        DrawPath();
    }
    private void DrawPath()
    {
        if (path != null && path.Count > 0)
        {
           // Debug.Log("Path drawn from " + path[0] + "To " + path[path.Count-1]);
            line.positionCount = path.Count;
            for (int i = 0; i < path.Count; i++)
            {
                line.SetPosition(i, Vector2IntToVector3(path[i], 0.1f) );
            } 
        }
        else 
        {
            Debug.Log(path + "non working paths");
        }
    }
    public void WalkTo(Vector3 _location, Vector2Int current, Cell[,] grid)
    {
        if(Astar == null)
        {
            //we gots a problem
            Astar = new AstarV2(grid.GetLength(0),grid.GetLength(1));
        }
        if(new Vector2Int( (int)_location.x, (int)_location.z) == current)
        {
            //location is the same
            return;
        }
        Debug.Log(current.ToString());
        Vector2Int targetPos = Vector3ToVector2Int(_location); 
    //    Debug.Log("i " + this.gameObject.name +"At " + current + "want to go to: " + targetPos); //100/5 = 20 wich is the width/height of the maze
        List<Vector2Int> Rawpath = Astar.FindPathToTarget(current, targetPos, grid);
        try
        {
            for (int i = 0; i < Rawpath.Count; i++)
            {
                if(maze != null)
                {
                    Rawpath[i] = Rawpath[i] * (int)(maze.scaleFactor * 2);
                }
                else //using dungeon
                {
                    Rawpath[i] = Rawpath[i];
                }
            }
            path = Rawpath;
            DrawPath();
            location = targetPos;
        }
        catch (System.Exception)
        {
            Debug.Log("path resulted in no instance: " + location + "from " + current );
            throw;
        }
    }
    public void TakeDamage() //should hit childeren 3 times before they die
    {
        Debug.Log(Hp + "Health points left");
        if (Hp == 1)
        {
            Debug.Log(Hp + "IM DED REMOVE ME");
            Blackboard.Enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
            //spawn a toy as token of reward on the place of "death"
           // Item a = new Item(); //itemobject
        }
        else
        {
            Hp--;
        }
    }
    public void Update()
    {
      //  Debug.Log("Update of:" + gameObject.name);
        if (path != null && path.Count > 0)
        {
            if (transform.position != Vector2IntToVector3(path[0])) //moving
            {      
                transform.position = Vector3.MoveTowards(transform.position, Vector2IntToVector3(path[0]), moveSpeed * Time.deltaTime); //transform.position
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector2IntToVector3(path[0]) - transform.position), 360f * Time.deltaTime);               
            }
            else
            {
                path.RemoveAt(0);
                DrawPath();
            }
            
        }
    }
    private Vector2Int Vector3ToVector2Int(Vector3 pos)
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
    void OnCollisionEnter(Collision dataFromCollision)
    {
        if (dataFromCollision.gameObject.tag == "Player")
        {
           if( Blackboard.playerpunch == true)
            {
                TakeDamage();
            }
            Debug.Log(dataFromCollision.collider.name);
        }

    }
}
