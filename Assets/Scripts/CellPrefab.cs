using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPrefab : MonoBehaviour
{
    public GameObject WallPrefab;
    Quaternion Left = new Quaternion(-1, 1, 1, 1);
    Quaternion Right = new Quaternion(1, 1, 1, -1);
    CellPrefab()
    {
        
    }
    CellPrefab(GameObject obj,Vector3 loc, Quaternion r,Transform a) //cellObject = Instantiate(obstacles[Random.Range(0, obstacles.Count)], new Vector3(x* scaleFactor, 0, y* scaleFactor) * 2, rotated, transform);
    {
        GameObject cellObject = Instantiate(obj, loc, r, a);
    }
    public void SpawnWalls(Cell cell)
    {
        
        if (cell.HasWall(Wall.DOWN)) { WallPrefab.name = "Down"; Instantiate(WallPrefab, transform.position, Quaternion.LookRotation(new Vector3(0, 270, 0)), transform); }
        if (cell.HasWall(Wall.UP)) { WallPrefab.name = "Up"; Instantiate(WallPrefab, transform.position, Quaternion.LookRotation(new Vector3(0, 90, -1)), transform); }
        if (cell.HasWall(Wall.LEFT)) { WallPrefab.name = "Left"; Instantiate(WallPrefab, transform.position, Left, transform); }
        if (cell.HasWall(Wall.RIGHT)) { WallPrefab.name = "Right"; Instantiate(WallPrefab, transform.position, Right, transform); }
    }
}