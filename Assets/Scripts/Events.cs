using Gameplay;
using Gameplay.Interfaces;
using System;
using System.Collections.Generic;
using TriggerSystem;
using TriggerSystem.Data;
using UnityEditor;
using UnityEngine;

public static class Events
{
    public static Action Resolve;

    public static class Actions
    {
        public static Action<ProjectileActionData,Card,Transform> Projectile;
    }
    public static class Zones
    {
        public static Action<Zone, List<Card>, Card> CardAdded;
        public static Action<Zone, List<Card>, Card> CardRemoved;
    }
    public static class Cards
    {
        public static Action<Card> Played;
        public static Action<Card> Drawn;
        public static Action<Card> Triggered;
        public static Action<Card> Created;
    }
    public static class Creatures
    {
        public static Action<ITargetable, int, int, int> Damaged;
        public static Action<ITargetable, int, int, int> Healed;
        public static Action<Card, int, int, int> MaxHealthChanged;
        public static Action<Card, int, int> AttackChanged;
        public static Action<Card, ITargetable> Attack;
    }
    public static class Players
    {
        public static Action<Player, int, int, int> MaxManaChanged;
        public static Action<Player, int, int, int> ManaChanged;
        public static Action<Player> TurnStarted;
        public static Action<Player> TurnEnded;
    }
}
