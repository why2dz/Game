using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDugeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField, Header("��С�����Ⱥ͸߶�")]
    private int minRoomWidth = 10, minRoomHeight = 10;

    [SerializeField, Header("���ο�Ⱥ͸߶�")]
    private int dungeonWidth = 70, dungeonHeight = 70;

    [SerializeField, Header("ƫ����")]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;
    protected override void RunProceduralGeneration()
    {
        CreateRooms(); // ��������
    }
    private void CreateRooms()
    {
        var roomsList = ProveduralGenerationAigorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight); // ʹ�ö���ռ�ָ��㷨���������б�
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // ���ڱ���ذ�����ļ���
        //floor = CreateSimpleRooms(roomsList); // �����򵥷���

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);// �����������
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);// �����򵥷���
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>(); // �洢���з�������������б�
        foreach (var room in roomsList) // �������з���
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center)); // ��������������ת��ΪVector2Int���ͺ���ӵ��б���
        }
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters); // �������з��䣬�õ����ȵ����꼯��
        floor.UnionWith(corridors); // ���������꼯�Ϻ͵ذ����꼯�Ϻϲ�

        tileMapvisualizer.PaintFloorTiles(floor); // ���Ƶذ�ש��
        WallGeneration.CreateWalls(floor, tileMapvisualizer); // ����ǽ��
    }

    // �������з��䲢���صذ����꼯��
    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)]; // ���ѡ��һ������������Ϊ��ǰ����
        roomCenters.Remove(currentRoomCenter); // �ӷ��������б����Ƴ���ǰ��������
        while (roomCenters.Count > 0) // ������δ���ӵķ���ʱѭ��
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters); // �ҵ����뵱ǰ������������ķ�������
            roomCenters.Remove(closest); // �ӷ��������б����Ƴ�����ķ�������
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest); // ������ǰ�������ĺ������������֮�������ͨ��
            currentRoomCenter = closest; // �����������������Ϊ��ǰ��������
            corridors.UnionWith(newCorridor); // ���´�����ͨ����ӵ���ͨ��������
        }
        return corridors; // ��������ͨ���ĵذ����꼯��
    }

    // Ѱ�ҵ�ǰ�������ĵ���������·���ϵĵ�
    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero; // ����ĵ������
        float distance = float.MaxValue; // ��ʼ������Ϊ���ֵ
        foreach (var position in roomCenters) // �������еķ�������
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter); // ���㵱ǰ���뵱ǰ��������֮��ľ���
            if (currentDistance < distance) // �����ǰ�����֮ǰ��¼����С����С
            {
                distance = currentDistance; // ������С����
                closest = position; // ��������ĵ������
            }
        }
        return closest; // ��������ĵ������
    }

    // ���������������������
    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>(); // �洢��������ļ���
        var position = currentRoomCenter; // ��ʼλ����Ϊ��ǰ��������
        corridor.Add(position); // ����ʼλ����ӵ��������꼯����
        while (position.y != destination.y) // ����y���ƶ�ֱ������Ŀ��λ�õ�y����
        {
            if (destination.y > position.y) // ���Ŀ��λ�õ�y������ڵ�ǰλ�õ�y����
            {
                position += Vector2Int.up; // �����ƶ�һ��
            }
            else if (destination.y < position.y) // ���Ŀ��λ�õ�y����С�ڵ�ǰλ�õ�y����
            {
                position += Vector2Int.down; // �����ƶ�һ��
            }
            corridor.Add(position); // ����λ����ӵ��������꼯����
        }
        while (position.x != destination.x) // ����x���ƶ�ֱ������Ŀ��λ�õ�x����
        {
            if (destination.x > position.x) // ���Ŀ��λ�õ�x������ڵ�ǰλ�õ�x����
            {
                position += Vector2Int.right; // �����ƶ�һ��
            }
            else if (destination.x < position.x) // ���Ŀ��λ�õ�x����С�ڵ�ǰλ�õ�x����
            {
                position += Vector2Int.left; // �����ƶ�һ��
            }
            corridor.Add(position); // ����λ����ӵ��������꼯����
        }
        return corridor; // ������������ļ���
    }


    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // ���ڱ���ذ�����ļ���
        foreach (var room in roomsList) // ���������б�
        {
            for (int col = offset; col < room.size.x - offset; col++) // ������
            {
                for (int row = offset; row < room.size.y - offset; row++) // ������
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row); // ����ذ�����
                    floor.Add(position); // ��ӵذ����굽������
                }
            }
        }
        return floor; // ���صذ弯��
    }


    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="roomsList"></param>
    /// <returns></returns>
    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // �洢�ذ�����ļ���
        for (int i = 0; i < roomsList.Count; i++) // �������з���
        {
            var roomBounds = roomsList[i]; // ��ȡ��ǰ����ı߽�
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y)); // ���㵱ǰ�������������
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter); // ʹ����������㷨��ȡ��ǰ����ĵذ����꼯��
            foreach (var position in roomFloor) // ������ǰ����ĵذ����꼯��
            {
                // ��������ڷ���߽����ƫ�����ķ�Χ�ڣ�������ӵ��ذ����꼯����
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor; // ���صذ�����ļ���
    }

}

