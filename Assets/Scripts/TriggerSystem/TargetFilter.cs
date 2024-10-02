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
            if (target == null) return false;
            if (source == null) return false;

            if (excludeSelf && source == target) return false;

            if (!creature && target.IsCreature()) return false;
            if (!spell && target.IsSpell()) return false;
            if (!player && target.IsPlayer()) return false;

            if (!ally && source.GetOwner() == target.GetOwner()) return false;
            if (!enemy && source.GetOwner() != target.GetOwner()) return false;

            return true;
        }


        public bool HasTarget()
        {
            return creature || player || spell || ally || enemy;
        }
    }
}