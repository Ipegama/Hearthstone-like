using Gameplay.Interfaces;
using System;
using System.Diagnostics;

namespace TriggerSystem
{
    [Serializable]
    public class TargetFilter
    {
        public bool creature;
        public bool player;
        public bool spell;

        public bool ally;
        public bool enemy;

        public bool excludeSelf;
        public bool Match(ITargetable source, ITargetable target)
        {
            if (source == null) return false;
            if (target == null) return false;
 
            if (excludeSelf && source == target) return false;

            bool isCreature = target.IsCreature();
            bool isSpell = target.IsSpell();
            bool isPlayer = target.IsPlayer();

            bool matchesType = (creature && isCreature) || (spell && isSpell) || (player && isPlayer);
            if (!matchesType) return false;

            bool isAlly = source.GetOwner() == target.GetOwner();
            bool isEnemy = source.GetOwner() != target.GetOwner();

            bool matchesAlly = ally && isAlly;
            bool matchesEnemy = enemy && isEnemy;

            if (!matchesAlly && !matchesEnemy) return false;

            return true;
        }

        public bool HasTarget()
        {
            return creature || player || spell || ally || enemy;
        }
    }
}