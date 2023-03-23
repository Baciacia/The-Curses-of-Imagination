using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo{
    public string name;
    public int xCoord, yCoord;
}

public class roomGenerator : MonoBehaviour
{

    public static roomGenerator instance;
    string currentWorldName = "Parter";
    RoomInfo currentLoadRoomData;
    Room currRoom;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    bool isLoadingRoom = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadRoom("Start", 0, 0);
        LoadRoom("Gol", 1, 0);
        LoadRoom("Gol", -1, 0);
        LoadRoom("Gol", 0, 1);
        LoadRoom("Gol", 0, -1);
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
        {
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));

    }
    public void LoadRoom( string name, int x, int y)
    {
        if(DoesRoomExist(x, y) == true)
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.xCoord = x;
        newRoomData.yCoord = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom( Room room)
    {
        
        room.transform.position = new Vector3(
            currentLoadRoomData.xCoord * room.width,
            currentLoadRoomData.yCoord * room.height,
            0
        );

        room.x = currentLoadRoomData.xCoord;
        room.y = currentLoadRoomData.yCoord;
        room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.x + ", " + room.y;
        room.transform.parent = transform;

        isLoadingRoom = false;

        loadedRooms.Add(room);
        

    }


    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y) != null;
    }
    // Update is called once per frame
}
