using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator :AbstractDungeonGenerator
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;



    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters,startPosition);
      

        tileMapvisualizer.PaintFloorTiles(floorPositions);

        WallGeneration.CreateWalls(floorPositions, tileMapvisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO Parameters,Vector2Int position)
    {

        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < Parameters.iterations; i++)
        {
            var path = ProveduralGenerationAigorithms.SimoleRandomWalk(currentPosition, Parameters.walklength);
            floorPositions.UnionWith(path);
            if (Parameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }

   


}
