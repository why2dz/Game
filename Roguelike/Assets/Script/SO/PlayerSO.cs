using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_", menuName = "Scriptable Objects/Player/Player_")]
public class PlayerSO : ScriptableObject

{
    [Header("����")]
    public string playerCharacterName;
    [Header("Ԥ����")]
    public GameObject playerPrefab;
    [Header("Ѫ��")]
    public int playerHealthAmount;

    //�š���
    public RuntimeAnimatorController animator;

    [Header("�����ͼͼ")]
    public Sprite playerMiniMapIcon;
    [Header("������ͼ")]
    public Sprite playerHandSprite;
}
