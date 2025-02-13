﻿using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    //inspector fields
    public int width, height;
    public float scaleFactor;
    public List<CellPrefab> CellList;
    public List<CellPrefab> obstacles;
    public float desiredWallpercentage = 0.4f;
    public int seed = 1234;

    public Cell[,] grid;
    private List<GameObject> allCellObjects = new List<GameObject>();

    public void Start()
    {
        Random.InitState(seed);//assign seed
        GenerateMaze();
        //    Blackboard.scalefactor = scaleFactor;
        Blackboard.Mazeheight = height;
        Blackboard.Mazewidth = width;
    }

    public void RegenarateMaze()
    {
        seed = Random.Range(0, int.MaxValue);
        Random.InitState(seed);
        width = Random.Range(10, 100);
        height = Random.Range(10, 100);
        desiredWallpercentage = Random.Range(0.2f, 1.0f);
        DestroyMazeObjects();
        GenerateMaze();
    }

    private void DestroyMazeObjects()
    {
        allCellObjects.Clear();
        foreach (Transform t in transform) //delete all transfomrs of this gameobject
        {
            Destroy(t.gameObject);
        }
    }

    public void GenerateMaze()
    {
        grid = new Cell[width, height];
        grid.Initialize();
        for (int x = 0; x < width; x++) //generates grid with full walls : Size = width,height
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Cell();
                grid[x, y].gridPosition = new Vector2Int(x, y);
                grid[x, y].walls = Wall.DOWN | Wall.LEFT | Wall.RIGHT | Wall.UP;
            }
        }

        Stack<Cell> cellStack = new Stack<Cell>(); //first in last out
        List<Cell> visitedCells = new List<Cell>();
        cellStack.Push(grid[0, 0]); //first item (start pos)
        Cell currentCell;
        while (cellStack.Count > 0) //path checking/creation
        {
            currentCell = cellStack.Pop(); //removes item from stack : last added item
                                           //  Debug.Log(cellStack.Count);
            List<Cell> neighbours = GetUnvisitedNeighbours(currentCell, visitedCells, cellStack); //fill list of neighbours detected 
            if (neighbours.Count > 1)
            {
                cellStack.Push(currentCell);
            }

            if (neighbours.Count != 0) //aslong as we have neighbours, check a "random" neighbour in the stack. 
            {
                Cell randomUnvisitedNeighbour = neighbours[Random.Range(0, neighbours.Count)];
                RemoveWallBetweenCells(currentCell, randomUnvisitedNeighbour); // RemoveWall between currentcell and active neighbour
                visitedCells.Add(randomUnvisitedNeighbour);
                cellStack.Push(randomUnvisitedNeighbour); //make neighbour the new currentcell
            }
        }

        //Remove a couple random walls to make the maze more 'open'
        int totalWallsInMaze = GetWallCount(grid);
        int totalPossibleWallsInmaze = 4 * width * height;
        float wallPercentage = totalWallsInMaze / (float)totalPossibleWallsInmaze;
        Debug.Log("Wall Percentage: " + wallPercentage);
        while (wallPercentage > desiredWallpercentage)
        {
            int randomX = Random.Range(0, width);
            int randomY = Random.Range(0, height);
            Cell randomCell = grid[randomX, randomY];
            List<Cell> neighbours = GetNeighbours(randomCell);
            if (neighbours.Count > 0)
            {
                Cell randomNeighbour = neighbours[Random.Range(0, neighbours.Count)];
                bool wallsRemoved = RemoveWallBetweenCells(randomCell, randomNeighbour);
                if (wallsRemoved)
                {
                    totalWallsInMaze -= 2;
                    wallPercentage = totalWallsInMaze / (float)totalPossibleWallsInmaze;
                }
            }
        }

        //Generate floor + walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Quaternion rotated = Quaternion.identity;
                rotated.z = 90;
                rotated.y = 90;
                CellPrefab cellObject = Instantiate(CellList[Random.Range(0, CellList.Count)], new Vector3(x * scaleFactor, 0, y * scaleFactor) * 2, rotated, transform);
                cellObject.name = "Tile" + y +":"+ x;
                cellObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                cellObject.SpawnWalls(grid[x, y]);
                allCellObjects.Add(cellObject.gameObject);
                
                
               //generate exit at last cell of the maze 
                if(x == width-1 && y == height-1)
                {
                    //Pole for indication of ending
                    cellObject.gameObject.AddComponent<SphereCollider>();
                    cellObject.gameObject.GetComponent<SphereCollider>().isTrigger = true;
                    //cellObject.gameObject.AddComponent<FinishComponent>();

                     GameObject end =  GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                      end.transform.position = new Vector3(x * scaleFactor, 1f,y * scaleFactor) * 2;
                      end.transform.localScale = new Vector3(1, 3, 1);
                      end.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }

        //Generate obstacles and others
        List<int> RandomRow = MazeStructures.RowControl(height, width); //gives back a list of number based on every row.
        foreach (int item in RandomRow)
        {
            //asign random location onrow
            int randomX = Random.Range(0, width);
            int randomY = Random.Range(0, height);
            Cell randomCell = grid[item, randomY]; //asign cell

            //transform isues
            // new Quaternion(0,1,0,1) BRIDGE length
            CellPrefab cellObject = obstacles[Random.Range(0, obstacles.Count)];
            if (cellObject.Is2x2 == true)
            {
                //ToDo delete 2 cells
                cellObject = Instantiate(cellObject, new Vector3(
                        randomCell.gridPosition.x * scaleFactor,            //x
                        -0.15F,                                             //y
                        randomCell.gridPosition.y * scaleFactor) * 2,       //z
                        Quaternion.Euler(-90, 90, 0),                        //quaternion
                        transform                                           //transform
                );
            }
            if (cellObject.Is2x2 == false)
            {
                Quaternion rotated = Quaternion.identity;
                rotated.z = 90;
                rotated.y = 90;
                cellObject = Instantiate
                (
                        cellObject,
                        new Vector3(
                        randomCell.gridPosition.x * scaleFactor,        //x
                        0,                                              //y
                        randomCell.gridPosition.y * scaleFactor) * 2,   //z
                        rotated,                                        //quaternion
                        transform                                       //transform
                );
                cellObject.transform.localScale = cellObject.transform.localScale * scaleFactor; //Scale ;
            }
            //Direct assigning of cellprefab: (obstacles[Random.Range(0, obstacles.Count)], new Vector3(x * scaleFactor, 0, y * scaleFactor) * 2, rotated, transform);
        }
    }
    private int GetWallCount(Cell[,] grid)
    {
        int walls = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];
                walls += cell.GetNumWalls();
            }
        }
        return walls;
    }

    private List<Cell> GetNeighbours(Cell cell)
    {
        List<Cell> result = new List<Cell>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                int cellX = cell.gridPosition.x + x;
                int cellY = cell.gridPosition.y + y;
                if (cellX < 0 || cellX >= width || cellY < 0 || cellY >= height || Mathf.Abs(x) == Mathf.Abs(y))
                {
                    continue;
                }
                Cell canditateCell = grid[cellX, cellY];
                result.Add(canditateCell);
            }
        }
        return result;
    }

    /// <summary>
    /// Gets the unvisited neighbours for a cell
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="visitedCells"></param>
    /// <param name="cellstack"></param>
    /// <returns></returns>
    private List<Cell> GetUnvisitedNeighbours(Cell cell, List<Cell> visitedCells, Stack<Cell> cellstack)
    {
        List<Cell> result = new List<Cell>();
        for (int x = -1; x < 2; x++) //check for cell neighbours in 3x3 grid
        {
            for (int y = -1; y < 2; y++)
            {
                int cellX = cell.gridPosition.x + x;
                int cellY = cell.gridPosition.y + y;
                if (cellX < 0 || cellX >= width || cellY < 0 || cellY >= height || Mathf.Abs(x) == Mathf.Abs(y))
                {
                    continue;
                }
                Cell canditateCell = grid[cellX, cellY];
                if (!visitedCells.Contains(canditateCell) && !cellstack.Contains(canditateCell)) //if cell is not visited nor in the cellstack add cell
                {
                    result.Add(canditateCell);
                }
            }
        }

        return result;
    }

    /// <summary>
    /// This function assumes the two inputcells are next to each other
    /// </summary>
    /// <param name="cellOne"></param>
    /// <param name="cellTwo"></param>
    private bool RemoveWallBetweenCells(Cell cellOne, Cell cellTwo)
    {
        int numWallCellOne = cellOne.GetNumWalls(); //see howmany walls we have on this cell
        Vector2Int dirVector = cellTwo.gridPosition - cellOne.gridPosition; //see which way the cells are connected (example result would be: dirVector (1,0) = needs wall top en down )
        if (dirVector.x != 0)
        {
            cellOne.RemoveWall(dirVector.x > 0 ? Wall.RIGHT : Wall.LEFT);
            cellTwo.RemoveWall(dirVector.x > 0 ? Wall.LEFT : Wall.RIGHT);
        }
        if (dirVector.y != 0)
        {
            cellOne.RemoveWall(dirVector.y > 0 ? Wall.UP : Wall.DOWN);
            cellTwo.RemoveWall(dirVector.y > 0 ? Wall.DOWN : Wall.UP);
        }

        //Is a wall succesfully removed?
        if (numWallCellOne != cellOne.GetNumWalls()) { return true; }
        return false;
    }

    public Cell GetCellForWorldPosition(Vector3 worldPos)
    {
        return grid[(int)(Mathf.RoundToInt(worldPos.x) / scaleFactor), (int)(Mathf.RoundToInt(worldPos.z) / scaleFactor)];
    }
}

[System.Serializable]
public class Cell
{
    public Vector2Int gridPosition;
    public Wall walls; //bitwise Encoded https://www.alanzucconi.com/2015/07/26/enum-flags-and-bitwise-operators/
    public void RemoveWall(Wall wallToRemove)
    {
        walls = (walls & ~wallToRemove);
    }

    public int GetNumWalls()
    {
        int numWalls = 0;
        if (((walls & Wall.DOWN) != 0)) { numWalls++; }
        if (((walls & Wall.UP) != 0)) { numWalls++; }
        if (((walls & Wall.LEFT) != 0)) { numWalls++; }
        if (((walls & Wall.RIGHT) != 0)) { numWalls++; }
        return numWalls;
    }

    public bool HasWall(Wall wall)
    {
        return (walls & wall) != 0;
    }
}

[System.Flags]
public enum Wall
{
    LEFT = 0x1,
    UP = 0x2,
    RIGHT = 0x4,
    DOWN = 0x8
}
