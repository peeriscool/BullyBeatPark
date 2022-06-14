using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RoomDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    public List<Color> allowedcolores; //to randomize color in the maze
    public int GridWidth = 40;
    public int GridHeight = 40;
    public int RoomCount = 4;
    public int MinWidth = 4;
    public int MaxWidth = 7;
    public int MinRoomSize = 4; //roomsize
    public int MaxRoomSize = 13; //roomsize
    public enum Tiletype { floor, wall };
    public Dictionary<Vector3Int, Tiletype> dungeon = new Dictionary<Vector3Int, Tiletype>();
    public Dictionary<Vector3Int, GameObject> instanced = new Dictionary<Vector3Int, GameObject>();
    public List<Room> RoomList = new List<Room>();
    public List<CellPrefab> CellList;
    public List<Vector2Int> connections;
    public List<Wallobject> walls = new List<Wallobject>();
    List<Vector2Int> drawablepath = new List<Vector2Int>();
    RoomDungeonGenerator instance;
    void Awake()
    {
        if (instance != null)
        {

        }
        else
        {
            instance = this;
        }
        connections = new List<Vector2Int>();
        GenerateRooms();
        Findconnections(RoomList);
        makepath();
        Roomcolor(RoomList);
        //this.gameObject.transform.localScale *= 2;
    }
    public void GenerateRooms() ///Generate dungeon
    {
        //Rooms
        for (int i = 0; i < RoomCount; i++)
        {
            int minX = Random.Range(0, GridWidth);
            int maxX = minX + Random.Range(MinWidth, MaxWidth + 1);
            int minZ = Random.Range(0, GridHeight);
            int maxZ = minZ + Random.Range(MinRoomSize, MaxRoomSize + 1);

            //check if room collides or tiles already used 
            Room room = new Room(minX, maxX, minZ, maxZ);
            if (Roomcheck(room))
            {
                AddRoomToDungeon(room);
                AddWallToRooms(CellList[0].WallPrefab, room);
            }
            else
            {
                i--; //to make sure we get all the rooms
            }
        }
        Spawndungeontiles();
    }
    public void Spawndungeon()
    {
        foreach (KeyValuePair<Vector3Int, Tiletype> kv in dungeon)
        {
            switch (kv.Value)
            {
                case Tiletype.floor:

                    instanced.Add(kv.Key, Instantiate(CellList[0].gameObject, kv.Key, Quaternion.Euler(-90, 0, 0), transform));
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
                dungeon.Add(new Vector3Int(x, 0, z), Tiletype.floor);
                room.mypositions.Add(new Vector3Int(x, 0, z));
            }
        }
        RoomList.Add(room);
        room.Roomindex = RoomList.Count;
    }
    public void Spawndungeontiles()
    {

        foreach (Room r in RoomList)
        {
            for (int i = 0; i < r.mypositions.Count; i++)
            {
                GameObject floor = Instantiate(CellList[0].gameObject, r.mypositions[i], Quaternion.Euler(-90, 0, 0), transform);
                floor.name = "Tile" + r.mypositions[i].y + ":" + r.mypositions[i].x; //i.ToString();
                instanced.Add(r.mypositions[i], floor);
            }
        }
    }
    /// <summary>
    /// add walls to the Room using a wall prefab
    /// </summary>
    /// <param name="_wall"></param>
    /// <param name="map"></param>
    /// <param name="r"></param>
    public void AddWallToRooms(GameObject _wall, Room r)
    {
        for (int x = r.minX; x < r.maxX; x++) //from min to max X value
        {
            for (int z = r.minZ; z < r.maxZ; z++) //from min to max Z value
            {
                try
                {
                    if (x == r.minX) //left walls //&& new Vector3Int(x,0,z) != r.mypositions[0] && new Vector3Int(x, 0, z) != r.mypositions[r.mypositions.Count-1]
                    {
                        GameObject wallL = GameObject.Instantiate(_wall, new Vector3Int(x, 0, z), new Quaternion(-1, 1, 1, 1), this.transform);
                        wallL.transform.localScale *= 0.5f;
                        walls.Add(new Wallobject(Wall.LEFT, wallL));
                    }
                    if (x == r.maxX - 1) //right walls
                    {
                        GameObject wallR = GameObject.Instantiate(_wall, new Vector3Int(x, 0, z), new Quaternion(1, 1, 1, -1), this.transform);
                        wallR.transform.localScale *= 0.5f;
                        walls.Add(new Wallobject(Wall.RIGHT, wallR));


                    }
                    if (z == r.minZ) //down
                    {
                        GameObject wallD = Instantiate(_wall, new Vector3Int(x, 0, z), Quaternion.LookRotation(new Vector3(0, 270, 0)), transform);
                        wallD.transform.localScale *= 0.5f;
                        walls.Add(new Wallobject(Wall.DOWN, wallD));

                    }
                    if (z == r.maxZ - 1) //up
                    {
                        GameObject wallU = Instantiate(_wall, new Vector3Int(x, 0, z), Quaternion.LookRotation(new Vector3(0, 90, -1)), transform);
                        wallU.transform.localScale *= 0.5f;
                        walls.Add(new Wallobject(Wall.UP, wallU));
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
        for (int x = room.minX - 1; x < room.maxX + 1; x++)
        {
            for (int z = room.minZ - 1; z < room.maxZ + 1; z++)
            {
                if (dungeon.ContainsKey(new Vector3Int(x, 0, z))) return false;
            }
        }
        return true;
    }
    void Findconnections(List<Room> rooms)
    {
        foreach (Room r in rooms) //for every room
        {
            for (int i = 0; i < r.mypositions.Count; i++) //for all known positions in the room
            {
                if (i == 0) // first cell 
                {
                    connections.Add(new Vector2Int(r.mypositions[i].x, r.mypositions[i].z)); //needs path to other room
                }
                else if (i == r.mypositions.Count - 1) // last cell 
                {
                    connections.Add(new Vector2Int(r.mypositions[i].x, r.mypositions[i].z)); //is end of path
                }
            }
        }
    }
    void makepath()
    {
        drawablepath = new List<Vector2Int>(); //corridors
        List<Vector2Int> corners = new List<Vector2Int>(); //corners
        for (int i = 1; i < connections.Count - 1; i++) //find path to next room
        {
            int xp = connections[i].x;
            int yp = connections[i].y;
            int maxX = connections[i + 1].x;
            int maxy = connections[i + 1].y;

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
        for (int c = 0; c < drawablepath.Count; c++) //make walls and path
        {
            Vector3Int loc = new Vector3Int(drawablepath[c].x, 0, drawablepath[c].y); //where
            if (instanced.ContainsKey(loc)) //get all values that are already occupied
            {
                //dont spawn walls in the rooms
            }
            else
            {
                GameObject inst = Instantiate(CellList[0].gameObject, loc, Quaternion.Euler(-90, 0, 0), transform);
                instanced.Add(loc, inst); //make floor tile

                if (drawablepath[c].x == drawablepath[c + 1].x && drawablepath[c].y == drawablepath[c - 1].y) //corner
                {
                    corners.Add(drawablepath[c]);
                    inst.name = "corner";//GetComponent<Renderer>().material.color = Color.white;
                    inst.GetComponent<Renderer>().material.color = Color.yellow;
                }

                if (!corners.Contains(drawablepath[c])) //skip all corners
                {
                    //todo Check for intersection: If so dont make walls
                    //to do corner walls 
                    if (drawablepath[c].x == drawablepath[c + 1].x + 1 && drawablepath[c].y != drawablepath[c + 1].y + 1)//vertical walls
                    {
                        GameObject wallD = Instantiate(CellList[0].WallPrefab, loc, Quaternion.LookRotation(new Vector3(0, 270, 0)), transform); //down
                        wallD.transform.localScale *= 0.5f;


                        GameObject wallU = Instantiate(CellList[0].WallPrefab, loc, Quaternion.LookRotation(new Vector3(0, 90, -1)), transform); //up
                        wallU.transform.localScale *= 0.5f;
                    }
                    if (drawablepath[c].x == drawablepath[c + 1].x - 1 && drawablepath[c].y != drawablepath[c + 1].y - 1)//vertical walls negative
                    {
                        GameObject wallD = Instantiate(CellList[0].WallPrefab, loc, Quaternion.LookRotation(new Vector3(0, 270, 0)), transform); //down
                        wallD.transform.localScale *= 0.5f;

                        GameObject wallU = Instantiate(CellList[0].WallPrefab, loc, Quaternion.LookRotation(new Vector3(0, 90, -1)), transform); //up
                        wallU.transform.localScale *= 0.5f;
                    }
                    if (drawablepath[c].y == drawablepath[c + 1].y + 1 && drawablepath[c].x != drawablepath[c + 1].x + 1) //horizontal walls
                    {
                        GameObject walll = GameObject.Instantiate(CellList[0].WallPrefab, loc, new Quaternion(-1, 1, 1, 1), this.transform); //left
                        walll.transform.localScale *= 0.5f;

                        GameObject wallr = GameObject.Instantiate(CellList[0].WallPrefab, loc, new Quaternion(1, 1, 1, -1), this.transform); //right
                        wallr.transform.localScale *= 0.5f;
                    }
                    if (drawablepath[c].y == drawablepath[c + 1].y - 1 && drawablepath[c].x != drawablepath[c + 1].x - 1) //horizontal walls negative
                    {
                        GameObject walll = GameObject.Instantiate(CellList[0].WallPrefab, loc, new Quaternion(-1, 1, 1, 1), this.transform); //left
                        walll.transform.localScale *= 0.5f;

                        GameObject wallr = GameObject.Instantiate(CellList[0].WallPrefab, loc, new Quaternion(1, 1, 1, -1), this.transform); //right
                        wallr.transform.localScale *= 0.5f;
                    }
                }
            }
        }
        Removewalls();
    }
    private void Removewalls()
    {
        foreach (Wallobject wl in walls) //walls of the rooms not path
        {
            Vector2Int poscheck = new Vector2Int((int)wl.myref.transform.position.x, (int)wl.myref.transform.position.z); //get location of current wall
            if (wl.myori == Wall.UP)
            {
                //find up connection 
                if (drawablepath.Contains(poscheck))
                {
                    try
                    {
                        if (drawablepath.Contains(new Vector2Int((int)wl.myref.transform.position.x, (int)wl.myref.transform.position.z + 1))) //if data also contains -1 on z remove 
                        {
                            Destroy(wl.myref);
                            Debug.Log("removed wall up");
                        }
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
            if (wl.myori == Wall.DOWN)
            {
                //find down connection 
                if (drawablepath.Contains(new Vector2Int((int)wl.myref.transform.position.x, (int)wl.myref.transform.position.z)))
                {
                    try
                    {
                        if (drawablepath.Contains(new Vector2Int((int)wl.myref.transform.position.x, (int)wl.myref.transform.position.z - 1))) //if data also contains -1 on z remove 
                        {
                            Destroy(wl.myref);
                            Debug.Log("removed wall up");
                        }
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
            if (wl.myori == Wall.LEFT)
            {
                //find left connection 
                if (drawablepath.Contains(new Vector2Int((int)wl.myref.transform.position.x, (int)wl.myref.transform.position.z)))
                {
                    ////remove
                    //GameObject vis = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
                    //vis.transform.position = wl.myref.transform.position;
                    try
                    {
                        if (drawablepath.Contains(new Vector2Int((int)wl.myref.transform.position.x - 1, (int)wl.myref.transform.position.z))) //if data also contains -1 on x remove 
                        {
                            Destroy(wl.myref);
                            Debug.Log("removed wall up");
                        }
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
            if (wl.myori == Wall.RIGHT)
            {
                //find right connection 
                if (drawablepath.Contains(new Vector2Int((int)wl.myref.transform.position.x, (int)wl.myref.transform.position.z)))
                {
                    try
                    {
                        if (drawablepath.Contains(new Vector2Int((int)wl.myref.transform.position.x + 1, (int)wl.myref.transform.position.z))) //if data also contains +1 on x remove 
                        {
                            Destroy(wl.myref);
                            Debug.Log("removed wall up");
                        }
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
        }
    }
    void Roomcolor(List<Room> rooms)
    {
        foreach (Room r in rooms) //for every room
        {
            Color rcoler = allowedcolores[Random.Range(0, allowedcolores.Count)]; //new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            for (int i = 0; i < r.mypositions.Count; i++) //for all known positions in the room
            {
                if (i == 0) // first cell 
                {
                    GameObject fcell = instanced[r.mypositions[i]];
                    fcell.GetComponent<Renderer>().material.color = allowedcolores[0];
                }
                else if (i == r.mypositions.Count - 1) // last cell 
                {
                    GameObject lcell = instanced[r.mypositions[i]];
                    lcell.GetComponent<Renderer>().material.color = allowedcolores[0];
                }
                else
                {
                    GameObject roomcell = instanced[r.mypositions[i]]; //find gameobject based on postion
                    roomcell.GetComponent<Renderer>().material.color = allowedcolores[Random.Range(1, allowedcolores.Count)];
                }
            }
        }
    }
    public Cell[,] dungeontocell()
    {
        Cell[,] data = new Cell[GridWidth,GridHeight];
        data.Initialize();
        for (int x = 0; x < GridWidth; x++) //generates grid with full walls : Size = width,height
        {
            for (int y = 0; y < GridHeight; y++)
            {
                data[x, y] = new Cell();
                data[x, y].gridPosition = new Vector2Int(x, y);
                data[x, y].walls = walls[x].myori | walls[y].myori;//Wall.DOWN | Wall.LEFT | Wall.RIGHT | Wall.UP;
            }
        }
        return data;
    }
}
public class Wallobject
{
    public Wall myori;
    public GameObject myref;
    public Wallobject(Wall _Orientation, GameObject _Myobject)
    {
        myori = _Orientation;
        myref = _Myobject;
    }
}
public class Room
{
    public int Roomindex; ///roomlist position
    public List<Vector3Int> mypositions; ///positions of the room
    public int minX, maxX, minZ, maxZ; ///sizes
    public Room(int _minX, int _maxX, int _minZ, int _maxZ)
    {
        minX = _minX;
        maxX = _maxX;
        minZ = _minZ;
        maxZ = _maxZ;
        mypositions = new List<Vector3Int>();
    }
}
