using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridoriFirstDugeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;

    //PCG Data


    protected override void RunProceduralGeneration()
    {
        CorridoriFirstDugeonGeneration();
    }

    private void CorridoriFirstDugeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialPoomPositions = new HashSet<Vector2Int>();

        GreateCorridors(floorPositions, potentialPoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialPoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tileMapvisualizer.PaintFloorTiles(floorPositions);
        WallGeneration.CreateWalls(floorPositions, tileMapvisualizer);
    }

    private void CreateRoomAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var directions in ProveduralGenerationAigorithms.Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + directions))
                    neighboursCount++;
            }
            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialPoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt( potentialPoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = potentialPoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void GreateCorridors(HashSet<Vector2Int> floorPosition,HashSet<Vector2Int> potentialPoomPositions)
    {
        var currentPosition = startPosition;
        potentialPoomPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProveduralGenerationAigorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialPoomPositions.Add(currentPosition);
            floorPosition.UnionWith(corridor);
        }
    }
}
