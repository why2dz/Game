using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGeneration 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapvisualizer tileMapvisualizer)
    {
        HashSet<Vector2Int> basicWallPositions = FindWallsInDirections(floorPositions, ProveduralGenerationAigorithms.Direction2D.cardinalDirectionsList);
        tileMapvisualizer.PaintWallTiles(basicWallPositions);
        

    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
       
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position  in floorPositions)
        {
            foreach (var direction in directionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition);
            }

        }
        return wallPositions;
    }
}
