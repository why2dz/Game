using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapvisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap flootTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallsTile;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
    
        PaintTiles(floorPositions,flootTilemap,floorTile);
    }


    public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
    {
        //foreach (var position in wallPositions)
        //{
        //    Debug.Log(position);
        //}
        PaintTiles(wallPositions, wallTilemap, wallsTile);
      
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        flootTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
