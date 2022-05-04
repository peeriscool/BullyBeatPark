using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SimpleDungeonGenerator : MonoBehaviour
{
    public GameObject FloorPrefab;
    public GameObject WallPrefab;
    public int GridWidth = 40;
    public int GridHeight = 40;
    public int RoomCount = 4;
    public int MinWidth = 4;
    public int MaxWidth = 7;
    public int MinRoomSize = 4; //roomsize
    public int MaxRoomSize = 13; //roomsize
    public enum tiletype {floor,wall};
    public Dictionary<Vector3Int, tiletype> dungeon = new Dictionary<Vector3Int, tiletype>();
    public Dictionary<Vector3Int, tiletype> map2 = new Dictionary<Vector3Int, tiletype>();
    public List<GameObject> instanced = new List<GameObject>();
    public List<Room> RoomList = new List<Room>();
    public List<obstruction> obstructionList = new List<obstruction>();
    ///ProTips:
    /// ctrl x knipt by default hele regels
    /// ctrl-rr voor alles renamen
    //alt pijltje omhoog en omlaag om regels te verplaatsen
    // uNDERSCORE = LOCAL

    void Start()
    {
        Generate();
    }
    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            // SceneManagerScript.callScenebyname(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            foreach (GameObject inst in instanced)
            {
                Destroy(inst);
            }
            GridWidth += GridWidth/10;
            GridHeight += GridHeight/10;
            Generate();

        }
    }
    public void Generate() ///Generate dungeon
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
            obstruction wall = new obstruction(minX, maxX, minZ, maxZ);
            if (Roomcheck(room))
            {
                
                AddRoomToDungeon(room);
                AddWallToDungeon(wall, map2);
            }
            else
            {
                i--; //to make sure we get all the rooms
            }
        }
        spawndungeon();
        //Conncet Rooms with bridges
        //generate dungeon

    }
    public void spawndungeon()
    {
        foreach (KeyValuePair<Vector3Int,tiletype> kv in dungeon)
        {
            switch (kv.Value)
            {
                case tiletype.floor:
                    instanced.Add(Instantiate(FloorPrefab, kv.Key, Quaternion.Euler(-90,0,0), transform));
                    break;

                case tiletype.wall:
               //     Instantiate(WallPrefab, kv.Key,Quaternion.identity, transform);
                    break;
            }
        }
        foreach (KeyValuePair<Vector3Int, tiletype> kv in map2)
        {
            switch (kv.Value)
            {
                case tiletype.floor:
                 //   Instantiate(FloorPrefab, kv.Key, Quaternion.Euler(-90, 0, 0), transform);
                    break;

                case tiletype.wall:
                    instanced.Add(Instantiate(WallPrefab, kv.Key, Quaternion.Euler(-90, 0, 0), transform));
                    break;
            }
        }
        //  FloorPrefab
    }
    public void AddRoomToDungeon(Room room)
    {
        for (int x = room.minX; x < room.maxX; x++) //save in dictonary
        {
            for (int z = room.minZ; z < room.maxZ; z++)
            {
                dungeon.Add(new Vector3Int(x,0,z),tiletype.floor);
               
            }
        }
        RoomList.Add(room);

    }
    public void AddWallToDungeon(obstruction wall, Dictionary<Vector3Int,tiletype> map)
    {
        for (int x = wall.minX; x < wall.maxX; x++) //save in dictonary
        {
            for (int z = wall.minZ; z < wall.maxZ; z++)
            {
                try
                {
                    map.Add(new Vector3Int(x, 0, z), tiletype.wall);
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
           
 
 
}

public class Room
{
    public int minX, maxX, minZ, maxZ;
    public Room(int _minX, int _maxX, int _minZ, int _maxZ)
    {
        minX = _minX;
        maxX = _maxX;
        minZ = _minZ;
        maxZ = _maxZ;
    }

}
public class obstruction
{
    public int minX, maxX, minZ, maxZ;
    public obstruction(int _minX, int _maxX, int _minZ, int _maxZ)
    {
        minX = _minX;
        maxX = _maxX;
        minZ = _minZ;
        maxZ = _maxZ;
    }

}
