using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentedCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
#region AstarV2
//public List<Vector2Int> makePathToTarget(Vector2Int startPos, Vector2Int endPos,Cell[,] grid) //dungeongenerator 2 uses this to make paths between rooms
//{
//  //  Cell[,] grid = NodetoCell(griddata);
//    List<Node> OpenSet = new List<Node>();
//    Node[,] AllNodes = GridToNodes(grid, endPos); 
//    HashSet<Node> ClosedSet = new HashSet<Node>();
//    width = grid.GetLength(0); //get length of first array
//    height = grid.GetLength(1);//get length of Second array
//   // Grid = grid;

//    Node StartNode = new Node(startPos, null, 0, 0);
//    Node EndNode = new Node(endPos, null, 0, 0); //change 0,0 to G and H 

//    foreach (Node item in AllNodes) //set start node and end node
//     {
//        if (item.position == startPos)
//        {
//            Debug.Log("Start found");
//            StartNode = item;
//            StartNode.GScore = 0;
//        }
//        if (item.position == endPos)
//        {
//            Debug.Log("End found");
//            EndNode = item;
//            EndNode.HScore = 0;
//        }
//    }
//    OpenSet.Add(StartNode);

//    while (OpenSet.Count > 0) //loop through all nodes in the open set
//    {
//        Node current = OpenSet[0];
//        for (int i = 1; i < OpenSet.Count; i++)
//        {
//            if (OpenSet[i].FScore < current.FScore || (OpenSet[i].FScore == current.FScore && OpenSet[i].HScore < current.HScore)) //find the lowest F cost using Current and index
//            {
//                current = OpenSet[i]; //lowest F cost found
//            }
//        }
//        OpenSet.Remove(current);
//        ClosedSet.Add(current);

//        if (current.position == endPos) //found end node
//        {
//            return RetracePath(StartNode, EndNode);
//        }

//        List<Node> neighbours = GetNeighbours(current, AllNodes);

//        foreach (Node Neighbour in neighbours)
//        {
//            //check for walls
//            //grid[0,0].HasWall(Wall.DOWN);
//            Vector2Int difference = Neighbour.position - current.position;
//            if (difference == new Vector2Int(0, 1) && grid[current.position.x, current.position.y].HasWall(Wall.UP)) //up
//            {
//                continue;
//            }
//            if (difference == new Vector2Int(1, 0) && grid[current.position.x, current.position.y].HasWall(Wall.RIGHT)) //right
//            {
//                continue;
//            }
//            if (difference == new Vector2Int(0, -1) && grid[current.position.x, current.position.y].HasWall(Wall.DOWN)) //down
//            {
//                continue;
//            }
//            if (difference == new Vector2Int(-1, 0) && grid[current.position.x, current.position.y].HasWall(Wall.LEFT)) //left
//            {
//                continue;
//            }

//            float tempGScore = current.GScore + GetDistance(current, Neighbour);
//            if (tempGScore < Neighbour.GScore)
//            {//the new path is shorter, update the GScore and the parent (for pathing)
//                Neighbour.GScore = tempGScore;
//                // node.HScore = GetDistance(node, EndNode);
//                Neighbour.parent = current;

//                if (!OpenSet.Contains(Neighbour))
//                {
//                    OpenSet.Add(Neighbour);
//                }
//            }
//        }

//    }
//    return null;
//}
#endregion
#region
#endregion
#region
#endregion