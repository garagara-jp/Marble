using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Title,
    Prepare,
    Stage,
    Result,
    Failure,
    Test
}

/// <summary>
/// ゲーム全体の進行を管理するGameManager
/// シングルトンパターンで作成playerAttackManager
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameState CurrentGameState { get { return currentGameState; } }
    private GameState currentGameState;

    private void Awake()
    {
        // シングルトンを確立
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // 外からこのメソッドを利用して状態を変更
    public void SetCurrentState(GameState state)
    {
        currentGameState = state;
        Debug.Log("CurrentGameState is " + CurrentGameState);
    }
}
