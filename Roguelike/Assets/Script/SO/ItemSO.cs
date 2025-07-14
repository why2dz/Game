using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementType
{
    OpenSpace, // �յ�
    NearWall // ����ǽ��
}


[CreateAssetMenu(fileName = "Item_", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [Header("��ƷͼƬ")]
    public Sprite sprite;
    [Header("��Ʒ��С")]
    public Vector2Int size;
    [Header("��������ö��")]
    public PlacementType placementType;
    [Header("�Ƿ����ƫ����,������Ч��ֹ��Ʒ����·��")]
    public bool addOffset;
}


[Serializable]
public class ItemDataInfo
{
    public ItemSO itemData;
    public int minQuantity;//��С����
    public int maxQuantity;//�������
}
