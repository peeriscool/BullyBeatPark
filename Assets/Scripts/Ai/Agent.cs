using System.Collections.Generic;
using UnityEngine;
public class Agent : MonoBehaviour
{
    public int moveButton = 0;
    public float moveSpeed = 3;
    public int actionindex = 0;
    private int Hp = 3;
    private enum actions { idle,walk,run,die};
    private AstarV2 Astar = new AstarV2(Blackboard.width,Blackboard.height);
    private List<Vector2Int> path = new List<Vector2Int>();
    private Plane ground = new Plane(Vector3.up, 0f);
    private MeshRenderer renderer;
    private GameObject targetVisual;
    private MazeGeneration maze;
    private LineRenderer line;
    private void Awake()
    {
        maze = FindObjectOfType<MazeGeneration>();
        renderer = GetComponentInChildren<MeshRenderer>();
        targetVisual = this.gameObject; //GameObject.CreatePrimitive(PrimitiveType.Cube);
        //targetVisual.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //targetVisual.GetComponent<MeshRenderer>().material.color = renderer.material.color;
        line = GetComponent<LineRenderer>();
        // Debug.Log(renderer.material.color);
        // line.material.color = renderer.material.color;
        line.material.color = Color.white;
    }

    public void FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        path = Astar.FindPathToTarget(startPos, endPos, grid);
        for (int i = 0; i < path.Count; i++)
        {
            path[i] = path[i] * 5; //should set the path to the world position
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
    public void WalkTo(Vector3 location)
    {
        Vector2Int targetPos = Vector3ToVector2Int(location); 
        Debug.Log("i " + this.gameObject.name + Vector3ToVector2Int(transform.position.normalized * maze.width) + "want to go to: " + targetPos); //100/5 = 20 wich is the width/height of the maze
       
        List<Vector2Int> Rawpath = Astar.FindPathToTarget(Vector3ToVector2Int(transform.position.normalized / maze.width), targetPos, maze.grid);
        for (int i = 0; i < Rawpath.Count; i++)
        {
            Rawpath[i] = Rawpath[i] * (int) (Blackboard.scalefactor *2); //(maze.width / 4); //works on 20x 20x on a scalefactor of 2.5 but is not a scaleble value!
        }
        path = Rawpath;
        DrawPath();
        // targetVisual.transform.position = Vector2IntToVector3(targetPos); 
        //for (int i = 0; i < Rawpath.Count; i++)
        //{
        //    Rawpath[i] = Rawpath[i] * 5;
        //}
        //path = Astar.FindPathToTarget(Vector3ToVector2Int(transform.position.normalized * 10), targetPos / 5, maze.grid); //normalized example 1.1 *10 = 11
    }
    public void TakeDamage() //should hit childeren 3 times before they die
    {
        Debug.Log(Hp + "Health points left");
        
        if(Hp == 0)
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
        if (actionindex == 3) //kill enemy 
        {
            GameManager.Instance.EnemyDied(this.gameObject);
            Destroy(this.gameObject);
        }

        if (path != null && path.Count > 0)
        {
            if (transform.position != Vector2IntToVector3(path[0]))
            {
                actionindex = 1;
                // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector2IntToVector3(path[0]) - transform.position), 360f * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, Vector2IntToVector3(path[0]), moveSpeed * Time.deltaTime); //transform.position
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector2IntToVector3(path[0]) - transform.position), 360f * Time.deltaTime);
                /*
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector2IntToVector3(path[0]) - transform.position), 360f * Time.deltaTime);
                Vector3 pos = Vector3.MoveTowards(transform.position, Vector2IntToVector3(path[0]), moveSpeed * Time.deltaTime); //transform.position
                this.GetComponent<Rigidbody>().MovePosition(pos);
                */
            }
            else
            {
               // Debug.Log(path.Count + "Path control for" + gameObject.name);
                actionindex = 0;
                path.RemoveAt(0);
                DrawPath();
            }
        }
        
  
    }
    public Vector3 MouseToWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distToGround = -1f;
        ground.Raycast(ray, out distToGround);
        Vector3 worldPos = ray.GetPoint(distToGround);

        return worldPos;
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
