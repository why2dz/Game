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

    ///����ռ�ָ��㷨
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>(); // ��������������ָ�Ŀռ�
        List<BoundsInt> roomsList = new List<BoundsInt>(); // �����б����������յķ���
        roomsQueue.Enqueue(spaceToSplit); // ����ʼ�ռ���������

        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue(); // ȡ�������е�һ���ռ�

            if (room.size.y >= minHeight && room.size.x >= minWidth) // ����ռ�Ŀ�Ⱥ͸߶ȶ����ڵ�����Сֵ
            {
                if (Random.value < 0.5f) // ���ѡ��ֱ��ˮƽ�ָ�
                {
                    if (room.size.y >= minHeight * 2) // ����ռ�ĸ߶ȴ��ڵ�����С�߶ȵ������������ˮƽ�ָ�
                    {
                        SplitHorizontally(minHeight, roomsQueue, room); // ˮƽ�ָ�ռ�
                    }
                    else if (room.size.x >= minWidth * 2) // ����ռ�Ŀ�ȴ��ڵ�����С��ȵ�����������д�ֱ�ָ�
                    {
                        SplitVertically(minWidth, roomsQueue, room); // ��ֱ�ָ�ռ�
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) // ����ռ�Ŀ�Ⱥ͸߶ȶ����ڵ�����Сֵ��������ӵ������б���
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2) // ����ռ�Ŀ�ȴ��ڵ�����С��ȵ�����������д�ֱ�ָ�
                    {
                        SplitVertically(minWidth, roomsQueue, room); // ��ֱ�ָ�ռ�
                    }
                    else if (room.size.y >= minHeight * 2) // ����ռ�ĸ߶ȴ��ڵ�����С�߶ȵ������������ˮƽ�ָ�
                    {
                        SplitHorizontally(minHeight, roomsQueue, room); // ˮƽ�ָ�ռ�
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) // ����ռ�Ŀ�Ⱥ͸߶ȶ����ڵ�����Сֵ��������ӵ������б���
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }

        return roomsList; // �������յķ����б�
    }

    // ��ֱ�ָ�ռ�
    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x); // ���ѡ��ָ���x����
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1); // ��ӷָ��������¿ռ䵽������
        roomsQueue.Enqueue(room2);
    }

    // ˮƽ�ָ�ռ�
    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y); // ���ѡ��ָ���y����
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1); // ��ӷָ��������¿ռ䵽������
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
