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
    public enum Tiletype {floor,wall};
    public Dictionary<Vector3Int, Tiletype> dungeon = new Dictionary<Vector3Int, Tiletype>();
    public Dictionary<Vector3Int, Tiletype> map2 = new Dictionary<Vector3Int, Tiletype>();
    public List<GameObject> instanced = new List<GameObject>();
    public List<Room> RoomList = new List<Room>();
    public List<Obstruction> obstructionList = new List<Obstruction>();
    ///ProTips:
    /// ctrl x knipt by default hele regels
    /// ctrl-rr voor alles renamen
    //alt pijltje omhoog en omlaag om regels te verplaatsen
    // uNDERSCORE = LOCAL

    void Start()
    {
        GenerateRooms();
    }
    private void Update()
    {
        //if(Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    // SceneManagerScript.callScenebyname(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        //    foreach (GameObject inst in instanced)
        //    {
        //        Destroy(inst);
        //    }
        //    GridWidth += GridWidth/10;
        //    GridHeight += GridHeight/10;
        //    Generate();

        //}
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
            Obstruction wall = new Obstruction(minX, maxX, minZ, maxZ);
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
        Spawndungeon();
        //Conncet Rooms with bridges
        //generate dungeon

    }
    public void Spawndungeon()
    {
        foreach (KeyValuePair<Vector3Int,Tiletype> kv in dungeon)
        {
            switch (kv.Value)
            {
                case Tiletype.floor:
                    instanced.Add(Instantiate(FloorPrefab, kv.Key, Quaternion.Euler(-90,0,0), transform));
                    break;

                case Tiletype.wall:
               //     Instantiate(WallPrefab, kv.Key,Quaternion.identity, transform);
                    break;
            }
        }
        foreach (KeyValuePair<Vector3Int, Tiletype> kv in map2)
        {
            switch (kv.Value)
            {
                case Tiletype.floor:
                 //   Instantiate(FloorPrefab, kv.Key, Quaternion.Euler(-90, 0, 0), transform);
                    break;

                case Tiletype.wall:
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
                dungeon.Add(new Vector3Int(x,0,z),Tiletype.floor);
               
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

/*  public void triggerMazeGeneration() //valentijn code
    {
        // Create action that binds to the primary action control on all devices.
        var action = new InputAction(binding: "
{ primaryAction}
");

        // Have it run your code when action is triggered.
action.performed += _ => Fire();

// Start listening for control changes.
action.Enable();
        // Keyboard.current[KeyCode.Space].wasPressedThisFrame;


    }
*/