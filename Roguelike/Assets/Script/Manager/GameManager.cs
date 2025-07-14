using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManger : Singleton<GameManger>
{
    #region 地牢水平
    [Space(10)]
    [Header("地牢水平")]
    #endregion
    #region 地牢水平
    [Tooltip("....")]
    #endregion
    [SerializeField]
    private List<DungeonLevelSO> dungeonLevels;
    [SerializeField]
    private int currentDungeonLevelListIndex = 0;

    public E_GameState gamestate;
    // Start is called before the first frame update
    private void Start()
    {
        gamestate = E_GameState.gameStarted;


    }

    // Update is called once per frame
    private void Update()
    {
        HandleGameState();
    }

    private void HandleGameState()
    {
        switch (gamestate)
        {
            case E_GameState.gameStarted:
                playDungeonLevel(currentDungeonLevelListIndex);
                gamestate = E_GameState.playingLevel;
                break;
            case E_GameState.playingLevel:
                break;
            case E_GameState.engagingEnemies:
                break;
            case E_GameState.bossStage:
                break;
            case E_GameState.engagingBoss:
                break;
            case E_GameState.levelCompleted:
                break;
            case E_GameState.gameWon:
                break;
            case E_GameState.gameLost:
                break;
            case E_GameState.gamePaused:
                break;
            case E_GameState.dungeonOverviewMap:
                break;
            case E_GameState.restartGame:
                break;
            default:
                break;
        }
    }

    private void playDungeonLevel(int currentDungeonLevelListIndex)
    {
        
    }
}
