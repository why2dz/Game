using System;
using System.Collections.Generic;
using UnityEngine;
public class Graph
{

    private List<Vector2Int> graph; // 图的节点列表

    public Graph(IEnumerable<Vector2Int> vertices)
    {
        graph = new List<Vector2Int>(vertices);
    }

    // 四个方向邻居偏移量列表
    private static List<Vector2Int> neighbours4Directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // 上
        new Vector2Int(1, 0), // 右
        new Vector2Int(0, -1), // 下
        new Vector2Int(-1, 0) // 左
    };

    // 八个方向邻居偏移量列表
    private static List<Vector2Int> neighbours8Directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // 上
        new Vector2Int(1, 0), // 右
        new Vector2Int(0, -1), // 下
        new Vector2Int(-1, 0), // 左
        new Vector2Int(1, 1), // 右上
        new Vector2Int(1, -1), // 右下
        new Vector2Int(-1, 1), // 左上
        new Vector2Int(-1, -1) // 左下
    };



    // 获取起始位置的四个方向邻居节点
    public List<Vector2Int> GetNeighbours4Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbours4Directions);
    }

    // 获取起始位置的八个方向邻居节点
    public List<Vector2Int> GetNeighbours8Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbours8Directions);
    }

    // 获取指定位置的邻居节点
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

