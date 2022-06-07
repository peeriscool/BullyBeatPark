using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SimpleDungeonGenerator : MonoBehaviour
{
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
    public Dictionary<Vector3Int,GameObject> instanced = new Dictionary<Vector3Int, GameObject>();
    public List<Room> RoomList = new List<Room>();
    public List<CellPrefab> CellList;
   // public Cell[,] grid;
    public List<Vector2Int> connections;
    SimpleDungeonGenerator instance;
    void Start()
    {
        if(instance != null)
        {
        
        }
        else
        {
            instance = this;
        }
        connections = new List<Vector2Int>();
      //  grid = new Cell[GridWidth, GridHeight];
      //  grid.Initialize();
        //for (int x = 0; x < GridWidth; x++) //generates grid with full walls : Size = width,height
        //{
        //    for (int y = 0; y < GridHeight; y++)
        //    {
        //        grid[x, y] = new Cell();
        //        grid[x, y].gridPosition = new Vector2Int(x, y);
        //        grid[x, y].walls = Wall.DOWN | Wall.LEFT | Wall.RIGHT | Wall.UP;
        //    }
        //}
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
        }
       //ToDO add walls
        for (int i = 0; i < drawablepath.Count; i++)
        {
          
            Vector3Int loc = new Vector3Int(drawablepath[i].x, 0, drawablepath[i].y);
            if (instanced.ContainsKey( loc))
            {
                //skip
            }
            else
            {
                instanced.Add(loc,Instantiate(CellList[0].gameObject, loc, Quaternion.Euler(-90, 0, 0), transform));
                //GameObject walll = GameObject.Instantiate(CellList[0].WallPrefab, loc, new Quaternion(-1, 1, 1, 1), this.transform);
                //walll.transform.localScale *= 0.5f;
                //GameObject wallr = GameObject.Instantiate(CellList[0].WallPrefab, loc, new Quaternion(1, 1, 1, -1), this.transform);
                //wallr.transform.localScale *= 0.5f;
            }
        }
    }
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
           // Obstruction wall = new Obstruction(minX, maxX, minZ, maxZ);
            if (Roomcheck(room))
            {
                AddRoomToDungeon(room);
                AddWallToDungeon(CellList[0].WallPrefab,dungeon,room);
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
                    instanced.Add(kv.Key,Instantiate(CellList[0].gameObject, kv.Key, Quaternion.Euler(-90, 0, 0), transform));
                   // spawnwalls(kv.Key);
                    break;

                case Tiletype.wall:
               //     Instantiate(WallPrefab, kv.Key,Quaternion.identity, transform);
                    break;
            }
        }   
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
    public void AddWallToDungeon(GameObject _wall, Dictionary<Vector3Int,Tiletype> map, Room r)
    {
        for (int x = r.minX; x < r.maxX; x++)
        {
            for (int z = r.minZ; z < r.maxZ; z++)
            {
                try
                {
                    if(x == r.minX) //left walls
                    {
                        GameObject walll = GameObject.Instantiate(_wall, new Vector3Int(x, 0, z), new Quaternion(-1, 1, 1, 1), this.transform);
                        walll.transform.localScale *= 0.5f;
                        //return;
                    }
                    //  map.Add(new Vector3Int(x, 0, z), Tiletype.wall);
                   if(x == r.maxX-1) //rightwalls
                    {
                        GameObject wallr = GameObject.Instantiate(_wall, new Vector3Int(x, 0, z), new Quaternion(1, 1, 1, -1), this.transform);
                        wallr.transform.localScale *= 0.5f;
                    }
                   if(z == r.minZ) //down
                    {
                        GameObject wallD = Instantiate(_wall, new Vector3Int(x, 0, z), Quaternion.LookRotation(new Vector3(0, 270, 0)), transform);
                        wallD.transform.localScale *= 0.5f;
                    }
                    if (z == r.maxZ-1) //up
                    {
                        GameObject wallU = Instantiate(_wall, new Vector3Int(x, 0, z), Quaternion.LookRotation(new Vector3(0, 90, -1)), transform);
                        wallU.transform.localScale *= 0.5f;
                    }
                }
                catch (System.Exception)
                {
                    Debug.LogError("Map error");
                    throw;
                }
            }
        }
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
                 //   fcell.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                    connections.Add(new Vector2Int(r.mypositions[i].x, r.mypositions[i].z)); //needs path to other room
                }
                else if (i == r.mypositions.Count-1) //make last cell white
                {
                    GameObject lcell = instanced[r.mypositions[i]];
                //    lcell.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
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
/*
    public void spawnwalls(Vector3Int kvalue) //simple function to give 4 walls to the given value
    {
        GameObject wallU = Instantiate(CellList[0].WallPrefab, instanced[kvalue].transform.position, Quaternion.LookRotation(new Vector3(0, 90, -1)), transform);
        wallU.transform.localScale *= 0.5f;
        GameObject wallD = Instantiate(CellList[0].WallPrefab, instanced[kvalue].transform.position, Quaternion.LookRotation(new Vector3(0, 270, 0)), transform);
        wallD.transform.localScale *= 0.5f;
        GameObject wallL = Instantiate(CellList[0].WallPrefab, instanced[kvalue].transform.position, new Quaternion(1, 1, 1, -1), transform);
        wallL.transform.localScale *= 0.5f;
        GameObject wallR = Instantiate(CellList[0].WallPrefab, instanced[kvalue].transform.position, new Quaternion(-1, 1, 1, 1), transform);
        wallR.transform.localScale *= 0.5f;

    }
  */
