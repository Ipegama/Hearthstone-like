using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player[] players;
    public GameBoard board;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializePlayers();
        InitializeBoard();
    }
    private void InitializePlayers()
    {
        foreach (var player in players)
        {
            player.Initialize();
        }
    }

    private void InitializeBoard()
    {
        board.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            players[0].DrawCard();
        }
    }
}
