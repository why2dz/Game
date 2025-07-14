using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPlacementHelper
{
    // �洢  ÿ��  ��Ʒ�������� ��  ���п���λ��
    Dictionary<PlacementType, HashSet<Vector2Int>> tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();
    // ������ ���������ȵĵذ�  ����
    HashSet<Vector2Int> roomFloorNoCorridor;

    /// <summary>
    /// ���ڸ�����Ʒ���õİ����࣬���ݷ���ذ���Ϣ�Ͳ��������ȵķ���ذ���Ϣ���г�ʼ����
    /// </summary>
    /// <param name="roomFloor">�����������ڵķ���ذ�λ�ü���</param>
    /// <param name="roomFloorNoCorridor">���������ȵķ���ذ�λ�ü���</param>
    public ItemPlacementHelper(HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorNoCorridor)
    {
        // ���ݷ���ذ�λ�ü��Ϲ���ͼ
        Graph graph = new Graph(roomFloor);

        // ��ʼ�����������ȵķ���ذ�λ�ü���
        this.roomFloorNoCorridor = roomFloorNoCorridor;

        // ����ÿ������ذ�λ��
        foreach (var position in roomFloorNoCorridor)
        {
            // ��ȡ��ǰλ�õ�8��������ھ�����
            int neighboursCount8Dir = graph.GetNeighbours8Directions(position).Count;

            // �����ھ������жϷ�������
            PlacementType type = neighboursCount8Dir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace;

            // ����÷������Ͳ����ֵ��У������һ���µķ�������
            if (tileByType.ContainsKey(type) == false)
            {
                tileByType[type] = new HashSet<Vector2Int>();
            }

            // ���ڿ�ǽ��λ�ã������4��������ھӣ���������λ��
            if (type == PlacementType.NearWall && graph.GetNeighbours4Directions(position).Count == 4)
            {
                continue;
            }

            // ��λ����ӵ���Ӧ�������͵ļ�����
            tileByType[type].Add(position);
        }
    }


    /// <summary>
    /// ���ݷ������͡���������������Ʒ�����С��ȡ��Ʒ����λ�á�
    /// </summary>
    /// <param name="placementType">��������</param>
    /// <param name="iterationsMax">����������</param>
    /// <param name="itemAreaSize">��Ʒ�����С</param>
    /// <returns>��Ʒ����λ�õĶ�ά����������Ҳ�������λ���򷵻�null</returns>
    public Vector2? GetItemPlacementPosition(PlacementType placementType, int iterationsMax, Vector2Int itemAreaSize, bool addOffset)
    {
        int itemArea = itemAreaSize.x * itemAreaSize.y;
        // ���ָ���������͵Ŀ���λ������С����Ʒ����Ĵ�С�����޷����ã�����null
        if (tileByType[placementType].Count < itemArea)
        {
            return null;
        }

        int iteration = 0;
        while (iteration < iterationsMax)
        {
            iteration++;

            // ���ѡ��һ��λ��
            int index = UnityEngine.Random.Range(0, tileByType[placementType].Count);
            // if(tileByType[placementType] == null) return null; 
            var position = tileByType[placementType].ElementAtOrDefault(index);
            if (position == null)
            {
                continue; // ������û��ָ��������λ��
            }
            // Vector2Int position = tileByType[placementType].ElementAt(index);

            // �����Ʒ�����С����1�����Է��ýϴ����Ʒ
            if (itemArea > 1)
            {
                var (result, placementPositions) = PlaceBigItem(position, itemAreaSize, addOffset);
                if (result == false)
                {
                    continue; // ����ʧ�ܣ�������һ�ε���
                }
                // �ӷ������ͺ��ڽ�ǽ�ڵ�λ�ü������ų��ѷ��õ�λ��
                tileByType[placementType].ExceptWith(placementPositions);
                tileByType[PlacementType.NearWall].ExceptWith(placementPositions);
            }
            else
            {
                // �Ƴ�����λ��
                tileByType[placementType].Remove(position);
            }
            return position; // ���سɹ����õ�λ��
        }
        return null; // �ﵽ������������δ�ҵ�����λ�ã�����null
    }


    /// <summary>
    /// ���ýϴ���Ʒ�����ط����Ƿ�ɹ��Լ����õ�λ���б�
    /// </summary>
    /// <param name="originPosition">��ʼλ��</param>
    /// <param name="size">��Ʒ�ߴ�</param>
    /// <param name="addOffset">�Ƿ����ƫ����</param>
    /// <returns>�����Ƿ�ɹ��Լ����õ�λ���б�</returns>
    private (bool, List<Vector2Int>) PlaceBigItem(Vector2Int originPosition, Vector2Int size, bool addOffset)
    {
        // ��ʼ�����õ�λ���б���������ʼλ��
        List<Vector2Int> positions = new List<Vector2Int>() { originPosition };

        // ����߽�ֵ
        int maxX = addOffset ? size.x + 1 : size.x;
        int maxY = addOffset ? size.y + 1 : size.y;
        int minX = addOffset ? -1 : 0;
        int minY = addOffset ? -1 : 0;

        // ����ÿ��λ��
        for (int row = minX; row <= maxX; row++)
        {
            for (int col = minY; col <= maxY; col++)
            {
                // ������ʼλ��
                if (col == 0 && row == 0)
                {
                    continue;
                }

                // ������λ��
                Vector2Int newPosToCheck = new Vector2Int(originPosition.x + row, originPosition.y + col);

                // �����λ���Ƿ����
                if (roomFloorNoCorridor.Contains(newPosToCheck) == false)
                {
                    return (false, positions); // ����ʧ�ܣ�����ʧ��״̬���ѷ��õ�λ���б�
                }
                positions.Add(newPosToCheck); // ����λ�ü����ѷ��õ�λ���б�
            }
        }

        return (true, positions); // ���óɹ������سɹ�״̬���ѷ��õ�λ���б�
    }
}

