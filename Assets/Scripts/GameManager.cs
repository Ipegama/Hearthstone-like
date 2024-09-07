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
        AnimationsQueue.Instance.StartQueue();

        var player = GetCurrentPlayerTurn();
        EventManager.Instance.TurnEnded.Raise(new ActionContext
        {
            TriggerEntity = player
        });
        Events.Players.TurnEnded?.Invoke(player);

        Events.Resolve?.Invoke(player);
        AnimationsQueue.Instance.EndQueue();
    }

    private void Update()
    {
        var player = GetCurrentControllerTurn();
        if(player != null)
        {
            player.UpdateControls();
        }
    }
    private PlayerController GetCurrentControllerTurn()
    {
        return playerControllers[_currentTurn%2];
    }
    private Player GetCurrentPlayerTurn()
    {
        return players[_currentTurn%2];
    }
    public Player GetEnemyOf(Player player)
    {
        if(player == players[0])
            return players[1];
        return players[0];
    }
    public List<ITargetable> GetAllEntities(Card card, TargetFilter filter)
    {
        var result = new List<ITargetable>();
        foreach(var player in players)
        {
            result.AddRange(player.GetAllTargets(card, filter));
        }
        return result;
    }
}
