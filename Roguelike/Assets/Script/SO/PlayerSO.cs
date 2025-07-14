using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_", menuName = "Scriptable Objects/Player/Player_")]
public class PlayerSO : ScriptableObject

{
    [Header("人物")]
    public string playerCharacterName;
    [Header("预制体")]
    public GameObject playerPrefab;
    [Header("血量")]
    public int playerHealthAmount;

    //嗯。。
    public RuntimeAnimatorController animator;

    [Header("人物地图图")]
    public Sprite playerMiniMapIcon;
    [Header("人物手图")]
    public Sprite playerHandSprite;
}
