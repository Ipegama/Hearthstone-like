using System;
using TriggerSystem.Data;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public GameEventData CreaturePlayed;
    public GameEventData CreatureDamaged;
    public GameEventData CreatureHealed;
    public GameEventData CreatureDeath;
    public GameEventData TurnStarted;
    public GameEventData TurnEnded;
    public GameEventData SpellPlayed;
    public GameEventData CardDrawn;
    private void Awake()
    {
        Instance = this;
    }
}
