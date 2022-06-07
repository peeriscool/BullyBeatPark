using System.Collections.Generic;
using UnityEngine;

public class AstarV2
{
    float width;
    float height;

    public AstarV2(float _width, float _height)
    {
        width = _width;
        height = _height;
    }
    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid) //agent use this to walk across known grid
    {
        if (startPos.x >= width) { Debug.Log("current location outside of grid!" + startPos.x + "size:" + width); } //debug code
       
        List<Node> OpenSet = new List<Node>(); //has to be filled
        Node[,] AllNodes = GridToNodes(grid,endPos); //maybe array would be better here because of number 0
        HashSet<Node> ClosedSet = new HashSet<Node>(); //final path
        width = grid.GetLength(0);
        height = grid.GetLength(1);

        Node StartNode = new Node(startPos, null, 0, 0);
        Node EndNode = new Node(endPos, null, 0, 0); //change 0,0 to G and H 

        foreach (Node item in AllNodes) //set start node and end node
        {
            if (item.position == startPos)
            {
               // Debug.Log("Start node found");
                StartNode = item;
                StartNode.GScore = 0;
            }
            if (item.position == endPos)
            {
             //   Debug.Log("End node found");
                EndNode = item;
                EndNode.HScore = 0;
            }
        }
        OpenSet.Add(StartNode);

        while (OpenSet.Count > 0) //loop through all nodes in the open set
        {
            Node current = OpenSet[0];
            for (int i = 1; i < OpenSet.Count; i++)
            {
                if (OpenSet[i].FScore < current.FScore || (OpenSet[i].FScore == current.FScore && OpenSet[i].HScore < current.HScore)) //find the lowest F cost using Current and index
                {
                    current = OpenSet[i]; //lowest F cost found
                }
            }
            OpenSet.Remove(current);
            ClosedSet.Add(current);

            if (current.position == endPos) //found end node
            {
                return RetracePath(StartNode, EndNode);
            }

            List<Node> neighbours = GetNeighbours(current, AllNodes);
            foreach (Node Neighbour in neighbours)
            {
                //check for walls
                //grid[0,0].HasWall(Wall.DOWN);
                Vector2Int difference = Neighbour.position - current.position;
                if (difference == new Vector2Int(0, 1) && grid[current.position.x, current.position.y].HasWall(Wall.UP)) //up
                {
                    continue;
                }
                if (difference == new Vector2Int(1, 0) && grid[current.position.x, current.position.y].HasWall(Wall.RIGHT)) //right
                {
                    continue;
                }
                if (difference == new Vector2Int(0, -1) && grid[current.position.x, current.position.y].HasWall(Wall.DOWN)) //down
                {
                    continue;
                }
                if (difference == new Vector2Int(-1, 0) && grid[current.position.x, current.position.y].HasWall(Wall.LEFT)) //left
                {
                    continue;
                }

                float tempGScore = current.GScore + GetDistance(current, Neighbour);
                if (tempGScore < Neighbour.GScore)
                {//the new path is shorter, update the GScore and the parent (for pathing)
                    Neighbour.GScore = tempGScore;
                    // node.HScore = GetDistance(node, EndNode);
                    Neighbour.parent = current;

                    if (!OpenSet.Contains(Neighbour))
                    {
                        OpenSet.Add(Neighbour);
                    }
                }
            }
        }
        Debug.Log("Path Zero");
        return null;
    }

    int GetDistance(Node A, Node B)//grid position between 2 nodes
    {
        int disX = Mathf.Abs(A.position.x - B.position.x);
        int disY = Mathf.Abs(A.position.y - B.position.y);
        if (disX > disY)
        {
            return 14 * disY + 10 * (disX - disY); //if the x distance is bigger 14 * vertical + 10 * (diagnal distance) else use horizontal
        }
        else return 14 * disX + 10 * (disY - disX);
    }
    /// <summary>
    /// checking in a 3x3 area for neighbours
    /// </summary>
    /// <param name="_current">control</param>
    /// <param name="_AllNodes">3x3 search</param>
    /// <returns></returns>
    public List<Node> GetNeighbours(Node _current, Node[,] _AllNodes)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x < 2; x++) //check 3x3 grid from current node
        {
            for (int y = -1; y < 2; y++)
            {
                if (Mathf.Abs(x) == Mathf.Abs(y)) continue; //negative becomes positive, 0 is skipped
                int cellX = _current.position.x + x;
                int cellY = _current.position.y + y;
                if (cellX < 0 || cellX >= width || cellY < 0 || cellY >= height) continue; //if its a valid neigbour                                                                        
                neighbours.Add(_AllNodes[cellX, cellY]);
            }
        }
        return neighbours;
    }
    List<Vector2Int> RetracePath(Node _startNode, Node _EndNode) //retraces path using parenting nodes
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = _EndNode;
        while (currentNode != _startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        //ToDo: add start poss
        path.Reverse();
        return path;
    }
    /// <summary>
    /// gets a grid and converts them to notes and calculates Hscore
    /// </summary>
    /// <param name="_Grid"></param>
    /// <param name="_EndPos"></param>
    /// <returns></returns>
    private Node[,] GridToNodes(Cell[,] _Grid, Vector2Int _EndPos)
    {
        Node[,] nodes = new Node[_Grid.GetLength(0), _Grid.GetLength(1)];
        float H = 0;
        for (int i = 0; i < _Grid.GetLength(0); i++) //loop through cell grid and make nodes
        {
            for (int j = 0; j < _Grid.GetLength(1); j++)
            {
                H = (int)(_Grid[i, j].gridPosition.x - _EndPos.x) + (_Grid[i, j].gridPosition.y - _EndPos.y);
                Node a = new Node(_Grid[i, j].gridPosition, null, int.MaxValue, (int)H);
                nodes[i, j] = a; //Dont know if i is correct position in the array
            }
        }
       // Debug.Log(nodes.Length);
        return nodes;
    }

    public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node
        public float FScore
        { 
            get { return GScore + HScore; } //GScore + HScore
        }
        public float GScore; //Current Travelled Distance
        public float HScore; //Distance estimated based on Heuristic

        public Node() { }
        public Node(Vector2Int position, Node parent, int GScore, int HScore)
        {
            this.position = position;
            this.parent = parent;
            this.GScore = GScore;
            this.HScore = HScore;
        }
    }
    public Cell[,] NodetoCell(Node[,] Nodelist)
    {
        Cell[,] value = new Cell[Nodelist.Length, Nodelist.Length];
        
        return value;
    }
}