using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementType
{
    OpenSpace, // 空地
    NearWall // 靠近墙壁
}


[CreateAssetMenu(fileName = "Item_", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [Header("物品图片")]
    public Sprite sprite;
    [Header("物品大小")]
    public Vector2Int size;
    [Header("放置类型枚举")]
    public PlacementType placementType;
    [Header("是否添加偏移量,可以有效防止物品阻塞路径")]
    public bool addOffset;
}


[Serializable]
public class ItemDataInfo
{
    public ItemSO itemData;
    public int minQuantity;//最小数量
    public int maxQuantity;//最大数量
}
