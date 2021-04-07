using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    static class MazeStructures
    {
        public static List <int> RowControl(int height,int width)
        {
            int index = 0;
            List<int> row = new List<int>();
            
            int randomrow = Random.Range(0, width);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (index == 0)//inital run
                    {

                    }
                    if (index != y && !row.Contains(y)) //index happens every 10x for 10 rows
                    {
                        index = y;
                        row.Add(index);
                      //  Debug.Log(y + "H");

                    }
                    if (x == randomrow) //select a random row place objects
                    {
                    // Quaternion rotated = Quaternion.identity;
                    // rotated.z = 90;
                    // rotated.y = 90;
                    //  cellObject = Instantiate(obstacles[Random.Range(0, obstacles.Count)], new Vector3(x * scaleFactor, 0, y * scaleFactor) * 2, rotated, transform);
                    //  CellPrefab cellObject = new CellPrefab(obstacles[Random.Range(0, obstacles.Count)], new Vector3(x * scaleFactor, 0, y * scaleFactor) * 2, rotated, transform); //(obstacles[Random.Range(0, obstacles.Count)], new Vector3(x * scaleFactor, 0, y * scaleFactor) * 2, rotated, transform);
                    // cellObject.transform.localScale = cellObject.transform.localScale * 24;
                    //allCellObjects.Add(cellObject.gameObject);
                    }
                    //   int randomX = Random.Range(0, width);
                    //   int randomY = Random.Range(0, height);
                    //   Cell randomCell = grid[randomX, randomY];
                }
            }
            return row;
        }
    }

