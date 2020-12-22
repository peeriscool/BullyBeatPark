using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPrefab : MonoBehaviour
{
    public GameObject WallPrefab;
    Quaternion Left = new Quaternion(-1, 1, 1, 1);
    Quaternion Right = new Quaternion(1, 1, 1, -1);
    void Start()
    {
        
    }

    public void SpawnWalls(Cell cell)
    {
        
        if (cell.HasWall(Wall.DOWN)) { WallPrefab.name = "Down"; Instantiate(WallPrefab, transform.position, Quaternion.LookRotation(new Vector3(0, 270, 0)), transform); }
        if (cell.HasWall(Wall.UP)) { WallPrefab.name = "Up"; Instantiate(WallPrefab, transform.position, Quaternion.LookRotation(new Vector3(0, 90, -1)), transform); }
        if (cell.HasWall(Wall.LEFT)) { WallPrefab.name = "Left"; Instantiate(WallPrefab, transform.position, Left, transform); }
        if (cell.HasWall(Wall.RIGHT)) { WallPrefab.name = "Right"; Instantiate(WallPrefab, transform.position, Right, transform); }
    }
}