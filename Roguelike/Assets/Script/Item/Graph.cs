using System;
using System.Collections.Generic;
using UnityEngine;
public class Graph
{

    private List<Vector2Int> graph; // ͼ�Ľڵ��б�

    public Graph(IEnumerable<Vector2Int> vertices)
    {
        graph = new List<Vector2Int>(vertices);
    }

    // �ĸ������ھ�ƫ�����б�
    private static List<Vector2Int> neighbours4Directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // ��
        new Vector2Int(1, 0), // ��
        new Vector2Int(0, -1), // ��
        new Vector2Int(-1, 0) // ��
    };

    // �˸������ھ�ƫ�����б�
    private static List<Vector2Int> neighbours8Directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // ��
        new Vector2Int(1, 0), // ��
        new Vector2Int(0, -1), // ��
        new Vector2Int(-1, 0), // ��
        new Vector2Int(1, 1), // ����
        new Vector2Int(1, -1), // ����
        new Vector2Int(-1, 1), // ����
        new Vector2Int(-1, -1) // ����
    };



    // ��ȡ��ʼλ�õ��ĸ������ھӽڵ�
    public List<Vector2Int> GetNeighbours4Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbours4Directions);
    }

    // ��ȡ��ʼλ�õİ˸������ھӽڵ�
    public List<Vector2Int> GetNeighbours8Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbours8Directions);
    }

    // ��ȡָ��λ�õ��ھӽڵ�
    private List<Vector2Int> GetNeighbours(Vector2Int startPosition, List<Vector2Int> neighboursOffsetList)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        foreach (var neighbourDirection in neighboursOffsetList)
        {
            Vector2Int potentialNeighbour = startPosition + neighbourDirection;
            if (graph.Contains(potentialNeighbour))
            {
                neighbours.Add(potentialNeighbour);
            }
        }
        return neighbours;
    }
}

