using Gameplay;
using System;
using System.Collections.Generic;

public static class Events
{
    public static class Actions 
    {
        //public static Action<ProjectileActionData, ActionContext> Projectile;
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
    }
    public static class Creatures 
    {
        public static Action<Card, int, int, int> Damaged;
        public static Action<Card> Death;
    }
}
