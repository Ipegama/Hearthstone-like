using Gameplay;
using TriggerSystem.Data;
using UnityEngine;

namespace TriggerSystem
{
    [CreateAssetMenu(fileName ="Stats Buff",menuName ="Buffs/Stats Buff")]
    public class StatsBuff : Buff
    {
        private readonly StatsBuffData _buffData;

        public StatsBuff(StatsBuffData buffData)
        {
            _buffData = buffData;
        }

        public override void OnApply(Card card)
        {
            if (card is Creature creature)
            {
                if (_buffData.attack > 0) creature.ChangedAttack(_buffData.attack);
                if (_buffData.maxHealth > 0) creature.ChangedMaxHealth(_buffData.maxHealth);
            }
        }

        public override void OnRemove(Card card)
        {
            if (card is Creature creature)
            {
                if (_buffData.attack > 0) creature.ChangedAttack(-_buffData.attack);
                if (_buffData.maxHealth > 0) creature.ChangedMaxHealth(-_buffData.maxHealth);
            }
        }
    }
}
