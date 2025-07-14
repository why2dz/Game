using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProveduralGenerationAigorithms
{
    //
    public static HashSet<Vector2Int> SimoleRandomWalk(Vector2Int startPositon, int walkLength)
    {
        HashSet<Vector2Int> Path = new HashSet<Vector2Int>();

        Path.Add(startPositon);

        var previousposition = startPositon;

        for (int i = 0; i < walkLength; i++)
        {
            var newPositon = previousposition + Direction2D.GetRandomCardinalDirection();
            Path.Add(newPositon);
            previousposition = newPositon;
        }

        return Path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        corridor.Add(startPosition);
        var currentPosition = startPosition;

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }

    ///二叉空间分割算法
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>(); // 创建队列来保存分割的空间
        List<BoundsInt> roomsList = new List<BoundsInt>(); // 创建列表来保存最终的房间
        roomsQueue.Enqueue(spaceToSplit); // 将初始空间加入队列中

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue(); // 取出队列中的一个空间

            if (room.size.y >= minHeight && room.size.x >= minWidth) // 如果空间的宽度和高度都大于等于最小值
            {
                if (Random.value < 0.5f) // 随机选择垂直或水平分割
                {
                    if (room.size.y >= minHeight * 2) // 如果空间的高度大于等于最小高度的两倍，则进行水平分割
                    {
                        SplitHorizontally(minHeight, roomsQueue, room); // 水平分割空间
                    }
                    else if (room.size.x >= minWidth * 2) // 如果空间的宽度大于等于最小宽度的两倍，则进行垂直分割
                    {
                        SplitVertically(minWidth, roomsQueue, room); // 垂直分割空间
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) // 如果空间的宽度和高度都大于等于最小值，则将其添加到房间列表中
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2) // 如果空间的宽度大于等于最小宽度的两倍，则进行垂直分割
                    {
                        SplitVertically(minWidth, roomsQueue, room); // 垂直分割空间
                    }
                    else if (room.size.y >= minHeight * 2) // 如果空间的高度大于等于最小高度的两倍，则进行水平分割
                    {
                        SplitHorizontally(minHeight, roomsQueue, room); // 水平分割空间
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) // 如果空间的宽度和高度都大于等于最小值，则将其添加到房间列表中
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }

        return roomsList; // 返回最终的房间列表
    }

    // 垂直分割空间
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x); // 随机选择分割点的x坐标
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1); // 添加分割后的两个新空间到队列中
        roomsQueue.Enqueue(room2);
    }

    // 水平分割空间
    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y); // 随机选择分割点的y坐标
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1); // 添加分割后的两个新空间到队列中
        roomsQueue.Enqueue(room2);
    }

    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
        {
            new Vector2Int(0,1),
            new Vector2Int(1,0),
            new Vector2Int(0,-1),
            new Vector2Int(-1,0)
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirectionsList[UnityEngine.Random.Range(0, cardinalDirectionsList.Count)];
        }
    }

}
