using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDugeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField, Header("最小房间宽度和高度")]
    private int minRoomWidth = 10, minRoomHeight = 10;

    [SerializeField, Header("地牢宽度和高度")]
    private int dungeonWidth = 70, dungeonHeight = 70;

    [SerializeField, Header("偏移量")]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;
    protected override void RunProceduralGeneration()
    {
        CreateRooms(); // 创建房间
    }
    private void CreateRooms()
    {
        var roomsList = ProveduralGenerationAigorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight); // 使用二叉空间分割算法创建房间列表
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // 用于保存地板坐标的集合
        //floor = CreateSimpleRooms(roomsList); // 创建简单房间

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);// 创建随机房间
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);// 创建简单房间
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>(); // 存储所有房间中心坐标的列表
        foreach (var room in roomsList) // 遍历所有房间
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center)); // 将房间中心坐标转换为Vector2Int类型后添加到列表中
        }
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters); // 连接所有房间，得到走廊的坐标集合
        floor.UnionWith(corridors); // 将走廊坐标集合和地板坐标集合合并

        tileMapvisualizer.PaintFloorTiles(floor); // 绘制地板砖块
        WallGeneration.CreateWalls(floor, tileMapvisualizer); // 创建墙壁
    }

    // 连接所有房间并返回地板坐标集合
    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)]; // 随机选择一个房间中心作为当前房间
        roomCenters.Remove(currentRoomCenter); // 从房间中心列表中移除当前房间中心
        while (roomCenters.Count > 0) // 当还有未连接的房间时循环
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters); // 找到距离当前房间中心最近的房间中心
            roomCenters.Remove(closest); // 从房间中心列表中移除最近的房间中心
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest); // 创建当前房间中心和最近房间中心之间的连接通道
            currentRoomCenter = closest; // 将最近房间中心设置为当前房间中心
            corridors.UnionWith(newCorridor); // 将新创建的通道添加到总通道集合中
        }
        return corridors; // 返回所有通道的地板坐标集合
    }

    // 寻找当前房间中心到最近房间的路径上的点
    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero; // 最近的点的坐标
        float distance = float.MaxValue; // 初始距离设为最大值
        foreach (var position in roomCenters) // 遍历所有的房间中心
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter); // 计算当前点与当前房间中心之间的距离
            if (currentDistance < distance) // 如果当前距离比之前记录的最小距离小
            {
                distance = currentDistance; // 更新最小距离
                closest = position; // 更新最近的点的坐标
            }
        }
        return closest; // 返回最近的点的坐标
    }

    // 创建连接两个房间的走廊
    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>(); // 存储走廊坐标的集合
        var position = currentRoomCenter; // 初始位置设为当前房间中心
        corridor.Add(position); // 将初始位置添加到走廊坐标集合中
        while (position.y != destination.y) // 沿着y轴移动直到到达目标位置的y坐标
        {
            if (destination.y > position.y) // 如果目标位置的y坐标大于当前位置的y坐标
            {
                position += Vector2Int.up; // 向上移动一格
            }
            else if (destination.y < position.y) // 如果目标位置的y坐标小于当前位置的y坐标
            {
                position += Vector2Int.down; // 向下移动一格
            }
            corridor.Add(position); // 将新位置添加到走廊坐标集合中
        }
        while (position.x != destination.x) // 沿着x轴移动直到到达目标位置的x坐标
        {
            if (destination.x > position.x) // 如果目标位置的x坐标大于当前位置的x坐标
            {
                position += Vector2Int.right; // 向右移动一格
            }
            else if (destination.x < position.x) // 如果目标位置的x坐标小于当前位置的x坐标
            {
                position += Vector2Int.left; // 向左移动一格
            }
            corridor.Add(position); // 将新位置添加到走廊坐标集合中
        }
        return corridor; // 返回走廊坐标的集合
    }


    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // 用于保存地板坐标的集合
        foreach (var room in roomsList) // 遍历房间列表
        {
            for (int col = offset; col < room.size.x - offset; col++) // 遍历列
            {
                for (int row = offset; row < room.size.y - offset; row++) // 遍历行
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row); // 计算地板坐标
                    floor.Add(position); // 添加地板坐标到集合中
                }
            }
        }
        return floor; // 返回地板集合
    }


    /// <summary>
    /// 创建随机房间
    /// </summary>
    /// <param name="roomsList"></param>
    /// <returns></returns>
    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>(); // 存储地板坐标的集合
        for (int i = 0; i < roomsList.Count; i++) // 遍历所有房间
        {
            var roomBounds = roomsList[i]; // 获取当前房间的边界
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y)); // 计算当前房间的中心坐标
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter); // 使用随机步行算法获取当前房间的地板坐标集合
            foreach (var position in roomFloor) // 遍历当前房间的地板坐标集合
            {
                // 如果坐标在房间边界加上偏移量的范围内，将其添加到地板坐标集合中
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor; // 返回地板坐标的集合
    }

}

