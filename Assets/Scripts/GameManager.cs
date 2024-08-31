using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using TriggerSystem;
using Gameplay.Interfaces;

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

    public List<ITargetable> GetAllEntities(ActionContext thisCard, TargetFilter filter)
    {
        throw new NotImplementedException();
    }

    public Player GetEnemyOf(Player owner)
    {
        return owner;
    }
}
