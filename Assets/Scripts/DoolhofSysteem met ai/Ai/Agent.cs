using System.Collections.Generic;
using UnityEngine;
public class Agent : MonoBehaviour
{
    public int moveButton = 0;
    public float moveSpeed = 3;
    public int actionindex = 0;
    public Vector2Int location = new Vector2Int();
    private int Hp = 3;
    private AstarV2 Astar = new AstarV2(Blackboard.Mazewidth,Blackboard.Mazeheight);
    private List<Vector2Int> path = new List<Vector2Int>();

    private MeshRenderer renderer;
    private GameObject targetVisual;
    private MazeGeneration maze;
    private LineRenderer line;

    private void Awake()
    {
        maze = FindObjectOfType<MazeGeneration>();
        renderer = GetComponentInChildren<MeshRenderer>();
        targetVisual = this.gameObject; 
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
        else {
            Debug.Log(path + "non working paths");
        }
    }
    //Move to clicked position
    public void WalkTo(Vector3 location,Vector2Int current)
    {  
        Vector2Int targetPos = Vector3ToVector2Int(location); 
        Debug.Log("i " + this.gameObject.name +"At " + current + "want to go to: " + targetPos); //100/5 = 20 wich is the width/height of the maze
        List<Vector2Int> Rawpath = Astar.FindPathToTarget(current, targetPos, maze.grid);

        try
        {
            for (int i = 0; i < Rawpath.Count; i++)
            {
                Rawpath[i] = Rawpath[i] * (int)(maze.scaleFactor * 2);
            }
            path = Rawpath;
            DrawPath();
        }
        catch (System.Exception)
        {
            Debug.Log("path resulted in no instance: " + location + "from " +current );
            throw;
        }
      
        }
    public void TakeDamage() //should hit childeren 3 times before they die
    {
        Debug.Log(Hp + "Health points left");

        if (Hp == 0)
        {
            actionindex = 3;
        }
        else
        {
            Hp--;
        }
    }
    public void MarkEnemy() //makes enemies slower and track there path 
    {
        moveSpeed = 1;
        targetVisual.GetComponent<Renderer>().material.color = Color.red;
    }
    public void Update()
    {
        //if (actionindex == 3) //kill enemy 
        //{
        //    GameManager.Instance.EnemyDied(this.gameObject);
        //    Destroy(this.gameObject);
        //}

        if (path != null && path.Count > 0)
        {
            if (transform.position != Vector2IntToVector3(path[0])) //moving
            {      
                actionindex = 1;
                transform.position = Vector3.MoveTowards(transform.position, Vector2IntToVector3(path[0]), moveSpeed * Time.deltaTime); //transform.position
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector2IntToVector3(path[0]) - transform.position), 360f * Time.deltaTime);               
            }
            else
            {
                location = path[0]/5; //location /4 = ~gridsize world/ location  
             //   Debug.Log("location of "+ this.name +"=" + location + "world = "+this.gameObject.transform.position);// Debug.Log(path.Count + "Path control for" + gameObject.name);
                actionindex = 0;
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
}
