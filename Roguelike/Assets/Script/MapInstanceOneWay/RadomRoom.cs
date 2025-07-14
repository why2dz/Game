using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadomRoom : MonoBehaviour
{
    public enum E_RoomPoint
    {
        Up,
        Down,
        Right,
        Left
    }

    [Header("房间信息")]
    public GameObject BaseRoom;
    public int RoomNum;
    public Color StartRoom;
    public Color EndRoom;
    private GameObject endRoom;
    private int fina = 0;

    private GameObject temporaryObj;

    [Header("位置信息")]
    public Transform PointTransform;
    public int Xoffice;
    public int Yoffice;

    public LayerMask layerMask;
    private E_RoomPoint _Room;
    private List<Room> Rooms = new List<Room>();

    private List<GameObject> Finally = new List<GameObject>();
    private List<GameObject> BeforeF = new List<GameObject>();
    private List<GameObject> OneDoor = new List<GameObject>();

    [Header("墙壁信息")]
   string doorNam = "Wall_";
    
    
   
    //private List<GameObject> walls = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < RoomNum - 1; i++)
        {
            Rooms.Add(Instantiate(BaseRoom, PointTransform.position, PointTransform.rotation).GetComponent<Room>());
            MoveRoom();
        }
        Rooms[0].GetComponent<SpriteRenderer>().color = StartRoom;
        endRoom = Rooms[0].gameObject;

        foreach (var room in Rooms)
        {
            SetDool(room, room.transform.position);
        }

        FindRoom();

        endRoom.GetComponent<SpriteRenderer>().color = EndRoom;


    }
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void MoveRoom()
    {
        do
        {
            _Room = (E_RoomPoint)Random.Range(0, 4);
            switch (_Room)
            {
                case E_RoomPoint.Up:
                    PointTransform.position += new Vector3(0, Yoffice, 0);
                    break;
                case E_RoomPoint.Down:
                    PointTransform.position += new Vector3(0, -Yoffice, 0);
                    break;
                case E_RoomPoint.Right:
                    PointTransform.position += new Vector3(Xoffice, 0, 0);
                    break;
                case E_RoomPoint.Left:
                    PointTransform.position += new Vector3(-Xoffice, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(PointTransform.position, 0.2f, layerMask));
    }

    void SetDool(Room room, Vector3 RoomPoint)
    {
        room.doolU = Physics2D.OverlapCircle(RoomPoint + new Vector3(0, Yoffice, 0), 0.2f, layerMask);
        room.doolD = Physics2D.OverlapCircle(RoomPoint + new Vector3(0, -Yoffice, 0), 0.2f, layerMask);
        room.doolR = Physics2D.OverlapCircle(RoomPoint + new Vector3(Xoffice, 0, 0), 0.2f, layerMask);
        room.doolL = Physics2D.OverlapCircle(RoomPoint + new Vector3(-Xoffice, 0, 0), 0.2f, layerMask);

        room.UpRoom(Xoffice, Yoffice);
        //利用Resources文件的aip
        temporaryObj = Resources.Load<GameObject>("Prefabs/Maps/Walls/" + GetWallType(room, doorNam));

        Instantiate(temporaryObj, room.transform);


        //使用反射(失败....)

        //var wallPrefabName = GetWallType(room);
        //var wallPrefab = wallType.GetType().GetField(wallPrefabName).GetValue(wallType) as GameObject;
        //print(wallType.GetType());
        //print(wallType.GetType().GetField(wallPrefabName));
        //print(wallType.GetType().GetField(wallPrefabName).GetValue(wallType));
        //Instantiate(wallPrefab, wallPrefab.transform);
    }

    void FindRoom()
    {
        foreach (var room in Rooms)
        {
            if (room.stratP > fina)
                fina = room.stratP;
        }

        foreach (var room in Rooms)
        {
            if (room.stratP == fina)
            {
                Finally.Add(room.gameObject);
            }
            if (room.stratP == fina - 1)
            {
                BeforeF.Add(room.gameObject);
            }
        }

        int i;
        for (i = 0; i < Finally.Count; i++)
        {
            if (Finally[i].GetComponent<Room>().doorcount == 1)
                OneDoor.Add(Finally[i]);
        }
        for (i = 0; i < BeforeF.Count; i++)
        {
            if (BeforeF[i].GetComponent<Room>().doorcount == 1)
                OneDoor.Add(BeforeF[i]);
        }

        if (OneDoor.Count != 0)
            endRoom = OneDoor[Random.Range(0, OneDoor.Count)];
        else
            endRoom = Finally[Random.Range(0, Finally.Count)];

    }

    public string GetWallType(Room room,string doorNam)
    {
        doorNam += (room.doolU ? 1 : 0).ToString();
        doorNam += (room.doolD ? 1 : 0).ToString();
        doorNam += (room.doolL ? 1 : 0).ToString();
        doorNam += (room.doolR ? 1 : 0).ToString();
        print(doorNam);
        return doorNam;
    }
        
}
