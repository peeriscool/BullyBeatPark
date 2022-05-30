using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SimpleDungeonGenerator : MonoBehaviour
{
    //  public GameObject FloorPrefab;
    //  public GameObject WallPrefab;
    [SerializeField]
    public List<Color> allowedcolores; 
    public int GridWidth = 40;
    public int GridHeight = 40;
    public int RoomCount = 4;
    public int MinWidth = 4;
    public int MaxWidth = 7;
    public int MinRoomSize = 4; //roomsize
    public int MaxRoomSize = 13; //roomsize
    public enum Tiletype {floor,wall};
    public Dictionary<Vector3Int, Tiletype> dungeon = new Dictionary<Vector3Int, Tiletype>();
 //   public Dictionary<Vector3Int, Tiletype> map2 = new Dictionary<Vector3Int, Tiletype>();
    public Dictionary<Vector3Int,GameObject> instanced = new Dictionary<Vector3Int, GameObject>();
    public List<Room> RoomList = new List<Room>();
    public List<Obstruction> obstructionList = new List<Obstruction>();


    public List<CellPrefab> CellList;
    public Cell[,] grid;
    //AstarV2 coridor;
    public List<Vector2Int> connections;
    ///ProTips:
    /// ctrl x knipt by default hele regels
    /// ctrl-rr voor alles renamen
    //alt pijltje omhoog en omlaag om regels te verplaatsen
    // uNDERSCORE = LOCAL

    void Start()
    {
        connections = new List<Vector2Int>();
        grid = new Cell[GridWidth, GridHeight];
        grid.Initialize();
        for (int x = 0; x < GridWidth; x++) //generates grid with full walls : Size = width,height
        {
            for (int y = 0; y < GridHeight; y++)
            {
                grid[x, y] = new Cell();
                grid[x, y].gridPosition = new Vector2Int(x, y);
                grid[x, y].walls = Wall.DOWN | Wall.LEFT | Wall.RIGHT | Wall.UP;
            }
        }
        GenerateRooms();
        Roomcolor(RoomList);
        makepath();
    }
    void makepath()
    {
        //List< Vector2Int> drawablepath = coridor.makePathToTarget(connections[0],connections[1],grid);
        List<Vector2Int> drawablepath = new List<Vector2Int>();
        for (int i = 1; i < connections.Count-1; i++)
        {
            int xp = connections[i].x;
            int yp = connections[i].y;
            int maxX = connections[i+1].x;
            int maxy = connections[i+1].y;
            
            while (xp != maxX)
            {
                if (xp <= maxX)
                {
                    drawablepath.Add(new Vector2Int(xp, yp));
                    xp++;
                }
                else if (xp >= maxX)
                {
                    drawablepath.Add(new Vector2Int(xp, yp));
                    xp--;
                }
            }
            while (yp != maxy)
            {
                if (yp <= maxy)
                {
                    drawablepath.Add(new Vector2Int(xp, yp));
                    yp++;
                }

                else if (yp >= maxy)
                {
                    drawablepath.Add(new Vector2Int(xp, yp));
                    yp--;
                }
            }
            //i++;
        }
       
       //ToDO See which corridors collide with rooms


        for (int i = 0; i < drawablepath.Count; i++)
        {
          
            Vector3Int loc = new Vector3Int(drawablepath[i].x, 0, drawablepath[i].y);
            if (instanced.ContainsKey( loc))
            {
                //skip
            }
            else
            {
                instanced.Add(loc,Instantiate(CellList[Random.Range(0, CellList.Count)].gameObject, loc, Quaternion.Euler(-90, 0, 0), transform));
                
            }
           
        }
    }
    //private void Update()
    //{
    //    //if(Mouse.current.leftButton.wasPressedThisFrame)
    //    //{
    //    //    // SceneManagerScript.callScenebyname(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    //    //    foreach (GameObject inst in instanced)
    //    //    {
    //    //        Destroy(inst);
    //    //    }
    //    //    GridWidth += GridWidth/10;
    //    //    GridHeight += GridHeight/10;
    //    //    Generate();

    //    //}
    //}
    public void GenerateRooms() ///Generate dungeon
    {
        //Rooms
        for (int i = 0; i < RoomCount; i++)
        {
           int minX = Random.Range(0,GridWidth);
            int maxX = minX + Random.Range(MinWidth,MaxWidth+1);
            int minZ = Random.Range(0, GridHeight);
            int maxZ = minZ + Random.Range(MinRoomSize, MaxRoomSize + 1);
            //check if room collides or tiles already used 
            Room room = new Room(minX,maxX,minZ,maxZ);
            Obstruction wall = new Obstruction(minX, maxX, minZ, maxZ);
            if (Roomcheck(room))
            {
                AddRoomToDungeon(room);
          //      AddWallToDungeon(wall, map2);
            }
            else
            {
                i--; //to make sure we get all the rooms
            }
        }
        Spawndungeon();
        
 

    }
    public void Spawndungeon()
    {
        foreach (KeyValuePair<Vector3Int,Tiletype> kv in dungeon)
        {
            switch (kv.Value)
            {
                case Tiletype.floor:
                    instanced.Add(kv.Key,Instantiate(CellList[Random.Range(0, CellList.Count)].gameObject, kv.Key, Quaternion.Euler(-90, 0, 0), transform));
                    break;

                case Tiletype.wall:
               //     Instantiate(WallPrefab, kv.Key,Quaternion.identity, transform);
                    break;
            }
        }

        //for (int i = 0; i < instanced.Count; i++) //for all the cells we have
        //{
        //    GameObject item = instanced[i]; //cache item

        //    // find out which rooms owns current item
        //    for (int j = 0; j < dungeon.Count; j++) //go trhough all known locations
        //    {
             
        //        //Vector3Int loc = new Vector3Int((int)item.transform.position.x, (int)item.transform.position.y, (int)item.transform.position.z);
        //        //if (dungeon.ContainsKey(loc))
        //        //{
        //        //    Debug.Log("Dungeon contains: " + loc);
        //        //    //for (int r = 0; r < RoomList.Count; r++)
        //        //    //{
        //        //      //  Room local = RoomList[1];
        //        //      //  item.GetComponent<Renderer>().material.color = local.Roomcolor;
        //        //   // }
                  
        //        //}
        //    }
        //  //  
        //}

        //foreach (KeyValuePair<Vector3Int, Tiletype> kv in map2)
        //{
        //    switch (kv.Value)
        //    {
        //        case Tiletype.floor:
        //         //   Instantiate(FloorPrefab, kv.Key, Quaternion.Euler(-90, 0, 0), transform);
        //            break;

        //        case Tiletype.wall:
        //            instanced.Add(Instantiate(WallPrefab, kv.Key, Quaternion.Euler(-90, 0, 0), transform));
        //            break;
        //    }
        //}
        //  FloorPrefab
    }
    public void AddRoomToDungeon(Room room)
    {
        for (int x = room.minX; x < room.maxX; x++) //aslong as room min is smaller as room max add tiles to the dungeon
        {
            for (int z = room.minZ; z < room.maxZ; z++)
            {
                dungeon.Add(new Vector3Int(x,0,z),Tiletype.floor);
                room.mypositions.Add(new Vector3Int(x, 0, z));
            }
        }
        RoomList.Add(room);
        
    }
    public void AddWallToDungeon(Obstruction wall, Dictionary<Vector3Int,Tiletype> map)
    {
        for (int x = wall.minX; x < wall.maxX; x++) //save in dictonary
        {
            for (int z = wall.minZ; z < wall.maxZ; z++)
            {
                try
                {
                    map.Add(new Vector3Int(x, 0, z), Tiletype.wall);
                }
                catch (System.Exception)
                {
                    Debug.LogError("Map error");
                    throw;
                }  
               

            }
        }
        obstructionList.Add(wall);

    }
    public bool Roomcheck(Room room)
    {
        for (int x = room.minX-1; x < room.maxX+1; x++)
        {
            for (int z = room.minZ-1; z < room.maxZ+1; z++)
            {
                if (dungeon.ContainsKey(new Vector3Int(x, 0, z))) return false ;
            }
        }
        return true;
    }

    //void celltomaze()
    //{
    //    grid = new Cell[GridWidth, GridHeight];
    //    grid.Initialize();
    //    for (int x = 0; x < GridWidth; x++)
    //    {
    //        for (int y = 0; y < GridHeight; y++)
    //        {
    //            CellPrefab cellObject = Instantiate(CellList[Random.Range(0, CellList.Count)], new Vector3(x , 0, y ) * 2, Quaternion.identity, transform);
    //            cellObject.gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f) , Random.Range(0, 1f));
    //            cellObject.name = "Tile" + y + ":" + x;
    //            cellObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    //            cellObject.SpawnWalls(grid[x, y]);
    //            allCellObjects.Add(cellObject.gameObject);
    //        }
    //    }
    //}
    void Roomcolor(List<Room> rooms)
    {
       
        foreach (Room r in rooms) //for every room
        {
            Color rcoler = allowedcolores[Random.Range(0,allowedcolores.Count)]; //new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            for (int i = 0; i < r.mypositions.Count; i++) //for all known positions in the room
            {
                if (i == 0) //make first cell black
                {
                    GameObject fcell = instanced[r.mypositions[i]];
                    fcell.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                    connections.Add(new Vector2Int(r.mypositions[i].x, r.mypositions[i].z)); //needs path to other room
                }
                else if (i == r.mypositions.Count-1) //make last cell white
                {
                    GameObject lcell = instanced[r.mypositions[i]];
                    lcell.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                    connections.Add(new Vector2Int(r.mypositions[i].x, r.mypositions[i].z)); //is end of path
                }
                else
                {
                    GameObject roomcell = instanced[r.mypositions[i]]; //find gameobject based on postion
                    roomcell.GetComponent<Renderer>().material.color = rcoler;
                }
                

            }
        }
    }

        //for (int x = 0; x < GridWidth; x++)
        //{
        //    for (int y = 0; y < GridHeight; y++)
        //    {
        //       // CellPrefab cellObject = Instantiate(CellList[Random.Range(0, CellList.Count)], new Vector3(x, 0, y) * 2, Quaternion.identity, transform);
        //        cellObject.gameObject.GetComponent<Renderer>().material.color = rcoler;
        //        cellObject.name = "Tile" + y + ":" + x;
        //        cellObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //        //cellObject.SpawnWalls(grid[x, y]);
        //        allCellObjects.Add(cellObject.gameObject);
        //    }
        //}
}

public class Room
{
    public List<Vector3Int> mypositions;
    public int minX, maxX, minZ, maxZ;
    public Room(int _minX, int _maxX, int _minZ, int _maxZ)
    {
        minX = _minX;
        maxX = _maxX;
        minZ = _minZ;
        maxZ = _maxZ;
        mypositions = new List<Vector3Int>();
    }
}
public class Obstruction
{
    public int minX, maxX, minZ, maxZ;
    public Obstruction(int _minX, int _maxX, int _minZ, int _maxZ)
    {
        minX = _minX;
        maxX = _maxX;
        minZ = _minZ;
        maxZ = _maxZ;
    }
}
