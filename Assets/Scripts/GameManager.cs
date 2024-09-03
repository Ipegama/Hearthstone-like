using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using TriggerSystem;
using Gameplay.Interfaces;
using GameAnimations;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int startingMana;
    public int startingHealth;

    public Player[] players;
    public PlayerController[] playerControllers;

    private int _currentTurn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializePlayers();
    }

    private void InitializePlayers()
    {
        foreach (var player in players)
        {
            player.Initialize(startingMana,startingHealth);
        }

        foreach (var player in players)
        {
            for(var i = 0; i < 5; i++)
            {
                player.DrawCard();
            }
        }

        _currentTurn = 0;
        TurnStarted();
    }

    public void EndTurn()
    {
        TurnEnded();
        _currentTurn++;
        TurnStarted();
    }

    private void TurnStarted()
    {
        AnimationsQueue.Instance.StartQueue();
        Events.Players.TurnStarted?.Invoke(GetCurrentPlayerTurn());
        Events.Resolve?.Invoke();
        AnimationsQueue.Instance.EndQueue();
    }

    private void TurnEnded()
    {
        throw new NotImplementedException();
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
